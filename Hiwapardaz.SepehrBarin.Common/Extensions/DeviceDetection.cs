﻿using Hiwapardaz.SepehrBarin.Common.Categories.UseragentLog;
using DeviceDetectorNET;
using DeviceDetectorNET.Parser;
using Microsoft.AspNetCore.Http;

namespace Hiwapardaz.SepehrBarin.Common.Extensions
{
    public static class DeviceDetection
    {
        //source  of this projcet at :https://github.com/totpero/DeviceDetector.NET
        public static LogInfo GetLogInfo(HttpRequest request, bool skipBotDetection=true)
        {           
            var (clientHints, dd) = GetClientHitnAndDD(request);
            return LogInfoFactory.Create(dd, clientHints, skipBotDetection);
        }        
        public static Tuple<bool, string> IsBot(HttpRequest request)
        {
            var userAgent = request.Headers["User-Agent"];
            var botParser = new BotParser();
            botParser.SetUserAgent(userAgent);

            // OPTIONAL: discard bot information. Parse() will then return true instead of information
            botParser.DiscardDetails = true;

            var result = botParser.Parse();

            if (result != null)
            {
                return new Tuple<bool,string>(false,string.Empty);
            }
            if(result is not null)
            {
                return new Tuple<bool, string>(true, result.ParserName);
            }
            return new Tuple<bool, string>(true,string.Empty);
        }

        public static async  Task<string> GetHash(HttpRequest request)
        {
            var userAgent = request.Headers["User-Agent"];
            string hash = await SecurityOperations.GetSha256Hash(userAgent);
            return hash;
        }

        private static (ClientHints,DeviceDetector) GetClientHitnAndDD(HttpRequest request)
        {
            DeviceDetector.SetVersionTruncation(VersionTruncation.VERSION_TRUNCATION_NONE);

            var userAgent = request.Headers["User-Agent"]; // change this to the useragent you want to parse
            var headers = request.Headers.ToDictionary(a => a.Key, a => a.Value.ToArray().FirstOrDefault());
            var clientHints = ClientHints.Factory(headers); // client hints are optional
            var dd = new DeviceDetector(userAgent, clientHints);

            return (clientHints, dd);
        }
    }
}
