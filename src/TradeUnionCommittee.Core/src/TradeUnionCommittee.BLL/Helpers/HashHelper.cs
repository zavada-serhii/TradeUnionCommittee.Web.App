using HashidsNet;
using System;
using System.Text;
using TradeUnionCommittee.BLL.Configurations;
using TradeUnionCommittee.BLL.Exceptions;

namespace TradeUnionCommittee.BLL.Helpers
{
    internal sealed class HashHelper
    {
        private static bool _isInitialized;
        private static bool _useGuidFormat;
        private static Hashids _hashIds;

        public static void Configure(HashIdConfiguration setting)
        {
            if (setting != null && !_isInitialized)
            {
                int minHashLenght = setting.UseGuidFormat
                    ? 32
                    : setting.MinHashLenght;

                string alphabet = setting.UseGuidFormat
                    ? setting.Alphabet.Replace("-", string.Empty).ToLower()
                    : setting.Alphabet;

                _useGuidFormat = setting.UseGuidFormat;
                _hashIds = new Hashids(setting.Salt, minHashLenght, alphabet);

                _isInitialized = true;
            }
        }

        public static long DecryptLong(string cipherText)
        {
            if (string.IsNullOrEmpty(cipherText) || string.IsNullOrWhiteSpace(cipherText))
            {
                return 0;
            }

            var result = _hashIds.DecodeLong(_useGuidFormat ? GuidFormat(cipherText, HashIdOperation.Decrypt) : cipherText);

            if (result.Length == 1)
            {
                return result[0];
            }
            throw new DecryptHashIdException();
        }

        public static string EncryptLong(long plainLong)
        {
            if (_useGuidFormat)
            {
                return GuidFormat(_hashIds.EncodeLong(plainLong), HashIdOperation.Encrypt);
            }
            return _hashIds.EncodeLong(plainLong);
        }

        private static string GuidFormat(string hash, HashIdOperation hashId)
        {
            var builder = new StringBuilder(hash);
            switch (hashId)
            {
                case HashIdOperation.Encrypt:
                    return builder.Insert(8, '-').Insert(13, '-').Insert(18, '-').Insert(23, '-').ToString();
                case HashIdOperation.Decrypt:
                    return builder.Replace("-", string.Empty).ToString();
                default:
                    throw new ArgumentOutOfRangeException(nameof(hashId), hashId, null);
            }
        }

        private enum HashIdOperation
        {
            Encrypt,
            Decrypt
        }
    }
}
