﻿using System.Buffers.Binary;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace Hiwapardaz.SepehrBarin.Common.Extensions
{
    public static class SecurityOperations
    {
        public static async Task<string> GetSha256Hash(string input)
        {
            using var hashAlgorithm = SHA256.Create();
            var byteValue = Encoding.UTF8.GetBytes(input);
            using (var stream = new MemoryStream(byteValue))
            {
                var byteHash = await hashAlgorithm.ComputeHashAsync(stream);
                return Convert.ToBase64String(byteHash);
            }
        }
        public static async Task<string> GetSha512Hash(string input)
        {
            using var hashAlgorithm = SHA512.Create();
            var byteValue = Encoding.UTF8.GetBytes(input);
            using (var stream = new MemoryStream(byteValue))
            {
                var byteHash = await hashAlgorithm.ComputeHashAsync(stream);
                return Convert.ToBase64String(byteHash);
            }
        }
        public static async Task<string> GenerateObjectHash(this object @object)
        {
            if (@object == null)
            {
                return string.Empty;
            }
            var jsonData = JsonSerializer.Serialize(@object);
            return await GetSha256Hash(jsonData);
        }
        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(plainTextBytes);
        }
        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = Convert.FromBase64String(base64EncodedData);
            return Encoding.UTF8.GetString(base64EncodedBytes);
        }

        public static Guid CreateCryptographicallySecureGuid()
        {
            RandomNumberGenerator _rand = RandomNumberGenerator.Create();
            var bytes = new byte[16];
            _rand.GetBytes(bytes);
            return new Guid(bytes);
        }
        public static string EncryptAesGcm(string plain)
        {
            byte[] plainBytes = Encoding.UTF8.GetBytes(plain);
            int nonceSize = AesGcm.NonceByteSizes.MaxSize;
            int tagSize = AesGcm.TagByteSizes.MaxSize;
            int cipherSize = plainBytes.Length;
            int encryptedDataLength = 4 + nonceSize + 4 + tagSize + cipherSize;
            Span<byte> encryptedData = encryptedDataLength < 1024
                                     ? stackalloc byte[encryptedDataLength]
                                     : new byte[encryptedDataLength].AsSpan();

            // Copy parameters
            BinaryPrimitives.WriteInt32LittleEndian(encryptedData.Slice(0, 4), nonceSize);
            BinaryPrimitives.WriteInt32LittleEndian(encryptedData.Slice(4 + nonceSize, 4), tagSize);
            var nonce = encryptedData.Slice(4, nonceSize);
            var tag = encryptedData.Slice(4 + nonceSize + 4, tagSize);
            var cipherBytes = encryptedData.Slice(4 + nonceSize + 4 + tagSize, cipherSize);

            // Generate secure nonce
            RandomNumberGenerator.Fill(nonce);

            // Encrypt
            using var aes = new AesGcm(GetKey());
            aes.Encrypt(nonce, plainBytes.AsSpan(), cipherBytes, tag);

            // Encode for transmission
            return Convert.ToBase64String(encryptedData);
        }
        public static string DecryptAesGcm(string cipher)
        {
            Span<byte> encryptedData = Convert.FromBase64String(cipher).AsSpan();

            // Extract parameter sizes
            int nonceSize = BinaryPrimitives.ReadInt32LittleEndian(encryptedData.Slice(0, 4));
            int tagSize = BinaryPrimitives.ReadInt32LittleEndian(encryptedData.Slice(4 + nonceSize, 4));
            int cipherSize = encryptedData.Length - 4 - nonceSize - 4 - tagSize;

            // Extract parameters
            var nonce = encryptedData.Slice(4, nonceSize);
            var tag = encryptedData.Slice(4 + nonceSize + 4, tagSize);
            var cipherBytes = encryptedData.Slice(4 + nonceSize + 4 + tagSize, cipherSize);

            // Decrypt
            Span<byte> plainBytes = cipherSize < 1024
                                  ? stackalloc byte[cipherSize]
                                  : new byte[cipherSize];
            using var aes = new AesGcm(GetKey());
            aes.Decrypt(nonce, cipherBytes, tag, plainBytes);

            // Convert plain bytes back into string
            return Encoding.UTF8.GetString(plainBytes);
        }
        private static byte[] GetKey()
        {
            var charArray = "3fd00454580de44ea216d8b7b234267a";
            return Encoding.GetEncoding("UTF-8").GetBytes(charArray);
        }
    }
}
