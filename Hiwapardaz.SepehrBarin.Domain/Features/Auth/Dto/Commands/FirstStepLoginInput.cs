﻿namespace Hiwapardaz.SepehrBarin.Domain.Features.Auth.Dto.Commands
{
    public class FirstStepLoginInput
    {
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? ClientDateTime { get; set; }
        public int AppVersion { get; set; }
        public string CaptchaText { get; set; } = null!;
        public string CaptchaToken { get; set; } = null!;
        public string CaptchaInputText { get; set; } = null!;
    }
}