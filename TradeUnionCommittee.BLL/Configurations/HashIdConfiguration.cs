using HashidsNet;
using System.Linq;
using TradeUnionCommittee.BLL.Exceptions;

namespace TradeUnionCommittee.BLL.Configurations
{
    public class HashIdConfigurationSetting
    {
        public string Salt { get; set; }
        public int MinHashLenght { get; set; }
        public string Alphabet { get; set; }
        public string Seps { get; set; }
        public bool UseGuidFormat { get; set; }
    }

    public interface IHashIdConfiguration
    {
        long DecryptLong(string cipherText);
        string EncryptLong(long plainLong);
    }

    internal sealed class HashIdConfiguration : IHashIdConfiguration
    {
        private readonly string _salt;
        private readonly int _minHashLenght;
        private readonly string _alphabet;
        private readonly string _seps;
        private readonly bool _useGuidFormat;

        public HashIdConfiguration(HashIdConfigurationSetting setting)
        {
            if (setting.UseGuidFormat)
            {
                _minHashLenght = 32;
                _alphabet = setting.Alphabet.Replace("-", string.Empty).ToLower();
            }
            else
            {
                _minHashLenght = setting.MinHashLenght;
                _alphabet = setting.Alphabet;
            }

            _salt = setting.Salt;
            _seps = setting.Seps;
            _useGuidFormat = setting.UseGuidFormat;
        }

        private Hashids ObjectHashids()
        {
            return new Hashids($"{_salt}-Service|{_salt.Reverse()}", _minHashLenght, _alphabet, _seps);
        }

        public string EncryptLong(long plainLong)
        {
            return _useGuidFormat ? GuidFormat(ObjectHashids().EncodeLong(plainLong), true) : ObjectHashids().EncodeLong(plainLong);
        }

        public long DecryptLong(string cipherText)
        {
            if (string.IsNullOrEmpty(cipherText) || string.IsNullOrWhiteSpace(cipherText))
            {
                return 0;
            }
            var result = ObjectHashids().DecodeLong(_useGuidFormat ? GuidFormat(cipherText, false) : cipherText);

            if (result.Length == 1)
            {
                return result[0];
            }
            throw new DecryptHashIdException();
        }

        private string GuidFormat(string hash, bool addOrRemoveMinus)
        {
            return addOrRemoveMinus ? hash.Insert(8, "-").Insert(13, "-").Insert(18, "-").Insert(23, "-") : hash.Replace("-", string.Empty);
        }
    }
}