using HashidsNet;
using System;

namespace TradeUnionCommittee.Encryption
{
    public class CryptoUtilities : ICryptoUtilities
    {
        private string Salt { get; } = "Development Salt";
        private const int MinHashLenght = 5;
        private const string Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";

        public string EncryptLong(long plainLong, EnumCryptoUtilities crypto)
        {
            var hashids = new Hashids(Salt + AdditionalSalt(crypto), MinHashLenght, Alphabet);
            return hashids.EncodeLong(plainLong);
        }

        public long DecryptLong(string cipherText, EnumCryptoUtilities crypto)
        {
            var hashids = new Hashids(Salt + AdditionalSalt(crypto), MinHashLenght, Alphabet);
            var decod = hashids.DecodeLong(cipherText);
            return decod[0];
        }

        public int DecryptInt(string cipherText, EnumCryptoUtilities crypto)
        {
            var hashids = new Hashids(Salt + AdditionalSalt(crypto), MinHashLenght, Alphabet);
            var decod = hashids.DecodeLong(cipherText);
            return Convert.ToInt32(decod[0]);
        }

        private string AdditionalSalt(EnumCryptoUtilities crypto)
        {
            switch (crypto)
            {
                case EnumCryptoUtilities.AccountService:
                    return "AccountService";
                case EnumCryptoUtilities.PositionService:
                    return "PositionService";
                default:
                    return string.Empty;
            }
        }
    }

    public interface ICryptoUtilities
    {
        int DecryptInt(string cipherText,EnumCryptoUtilities crypto);
        long DecryptLong(string cipherText,EnumCryptoUtilities crypto);
        string EncryptLong(long plainLong, EnumCryptoUtilities crypto);
    }

    public enum EnumCryptoUtilities
    {
        AccountService = 1,
        PositionService = 2
    }
}