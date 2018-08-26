using HashidsNet;

namespace TradeUnionCommittee.Encryption
{
    public class CryptoUtilities : ICryptoUtilities
    {
        private const string Salt = "Development Salt ";
        private const int MinHashLenght = 5;
        private const string Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";

        public string EncryptLong(long plainLong, EnumCryptoUtilities crypto)
        {
            var hashids = new Hashids(Salt + AdditionalSalt(crypto), MinHashLenght, Alphabet);
            return hashids.EncodeLong(plainLong);
        }

        public bool CheckDecrypt(string cipherText, EnumCryptoUtilities crypto)
        {
            var hashids = new Hashids(Salt + AdditionalSalt(crypto), MinHashLenght, Alphabet);
            return hashids.DecodeLong(cipherText).Length > 0;
        }

        public long DecryptLong(string cipherText, EnumCryptoUtilities crypto)
        {
            if (cipherText == null)
            {
                return 0;
            }
            var hashids = new Hashids(Salt + AdditionalSalt(crypto), MinHashLenght, Alphabet);
            var decod = hashids.DecodeLong(cipherText);
            return decod[0];
        }

        private string AdditionalSalt(EnumCryptoUtilities crypto)
        {
            switch (crypto)
            {
                case EnumCryptoUtilities.Account:
                    return "AccountService";
                case EnumCryptoUtilities.Position:
                    return "PositionService";
                case EnumCryptoUtilities.SocialActivity:
                    return "SocialActivityService";
                case EnumCryptoUtilities.Privileges:
                    return "PrivilegesService";
                case EnumCryptoUtilities.Award:
                    return "AwardService";
                case EnumCryptoUtilities.MaterialAid:
                    return "MaterialAidService";
                case EnumCryptoUtilities.Hobby:
                    return "HobbyService";
                case EnumCryptoUtilities.Travel:
                    return "TravelService";
                case EnumCryptoUtilities.Wellness:
                    return "WellnessService";
                case EnumCryptoUtilities.Tour:
                    return "TourService";
                case EnumCryptoUtilities.Activities:
                    return "ActivitiesService";
                case EnumCryptoUtilities.Cultural:
                    return "CulturalService";
                case EnumCryptoUtilities.Subdivision:
                    return "SubdivisionService";
                case EnumCryptoUtilities.Departmental:
                    return "DepartmentalService";
                case EnumCryptoUtilities.Dormitory:
                    return "DormitoryService";
                default:
                    return string.Empty;
            }
        }
    }

    public interface ICryptoUtilities
    {
        long DecryptLong(string cipherText,EnumCryptoUtilities crypto);
        string EncryptLong(long plainLong, EnumCryptoUtilities crypto);
        bool CheckDecrypt(string cipherText, EnumCryptoUtilities crypto);
    }

    public enum EnumCryptoUtilities
    {
        Account = 1,
        Position = 2,
        SocialActivity = 3,
        Privileges = 4,
        Award = 5,
        MaterialAid = 6,
        Hobby = 7,
        Travel = 8,
        Wellness = 9,
        Tour = 10,
        Activities = 11,
        Cultural = 12,
        Subdivision = 13,
        Departmental = 14,
        Dormitory = 15
    }
}