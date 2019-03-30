using HashidsNet;
using System;
using System.Text;
using TradeUnionCommittee.BLL.Exceptions;

namespace TradeUnionCommittee.BLL.Configurations
{
    public class HashIdConfigurationSetting
    {
        public string Salt { get; set; }
        public int MinHashLenght { get; set; }
        public string Alphabet { get; set; }
        public bool UseGuidFormat { get; set; }
    }

    internal sealed class HashIdConfiguration
    {
        private readonly bool _useGuidFormat;
        private readonly Hashids _hashId;

        public HashIdConfiguration(HashIdConfigurationSetting setting)
        {
            if (setting.UseGuidFormat)
            {
                setting.MinHashLenght = 32;
                setting.Alphabet = setting.Alphabet.Replace("-", string.Empty).ToLower();
            }
            _useGuidFormat = setting.UseGuidFormat;
            _hashId = new Hashids(setting.Salt, setting.MinHashLenght, setting.Alphabet);
        }

        public long DecryptLong(string cipherText)
        {
            if (string.IsNullOrEmpty(cipherText) || string.IsNullOrWhiteSpace(cipherText))
            {
                throw new DecryptHashIdException();
            }

            var result = _hashId.DecodeLong(_useGuidFormat ? GuidFormat(cipherText, HashIdOperation.Decrypt) : cipherText);

            if (result.Length == 1)
            {
                return result[0];
            }
            throw new DecryptHashIdException();
        }

        public string EncryptLong(long plainLong)
        {
            if (_useGuidFormat)
            {
                return GuidFormat(_hashId.EncodeLong(plainLong), HashIdOperation.Encrypt);
            }
            return _hashId.EncodeLong(plainLong);
        }

        private string GuidFormat(string hash, HashIdOperation hashId)
        {
            var builder = new StringBuilder(hash);
            switch (hashId)
            {
                case HashIdOperation.Encrypt:
                    return builder.Insert(8, "-").Insert(13, "-").Insert(18, "-").Insert(23, "-").ToString();
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