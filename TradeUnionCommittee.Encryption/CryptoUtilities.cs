using HashidsNet;

namespace TradeUnionCommittee.Encryption
{
    public class CryptoUtilities : ICryptoUtilities
    {
        private const string Salt = "Development Salt ";
        private const int MinHashLenght = 5;
        private const string Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
        private const string Seps = "TradeUnionCommittee.Encryption.Assembly.Development.Seps";

        private Hashids ObjectHashids(EnumCryptoUtilities crypto)
        {
            return new Hashids(Salt + AdditionalSalt(crypto), MinHashLenght, Alphabet, Seps);
        }

        public string EncryptLong(long plainLong, EnumCryptoUtilities crypto)
        {
            return ObjectHashids(crypto).EncodeLong(plainLong);
        }

        public long DecryptLong(string cipherText, EnumCryptoUtilities crypto)
        {
            if (string.IsNullOrEmpty(cipherText) || string.IsNullOrWhiteSpace(cipherText))
            {
                return 0;
            }
            var decod = ObjectHashids(crypto).DecodeLong(cipherText);
            return decod[0];
        }

        public bool CheckDecrypt(string cipherText, EnumCryptoUtilities crypto, out long id)
        {
            id = 0;
            if (string.IsNullOrEmpty(cipherText) || string.IsNullOrWhiteSpace(cipherText))
            {
                return false;
            }
            var result = DecryptLong(cipherText, crypto);
            if (result == 0) return false;
            id = result;
            return true;
        }

        private string AdditionalSalt(EnumCryptoUtilities crypto)
        {
            switch (crypto)
            {
                case EnumCryptoUtilities.Account:
                    return "AccountService";
                case EnumCryptoUtilities.Role:
                    return "RoleService";
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
                case EnumCryptoUtilities.Employee:
                    return "EmployeeService";
                default:
                    return string.Empty;
            }
        }
    }

    public interface ICryptoUtilities
    {
        long DecryptLong(string cipherText,EnumCryptoUtilities crypto);
        string EncryptLong(long plainLong, EnumCryptoUtilities crypto);
        bool CheckDecrypt(string cipherText, EnumCryptoUtilities crypto, out long id);
    }

    public enum EnumCryptoUtilities
    {
        Account = 1,
        Role = 2,
        Position = 3,
        SocialActivity = 4,
        Privileges = 5,
        Award = 6,
        MaterialAid = 7,
        Hobby = 8,
        Travel = 9,
        Wellness = 10,
        Tour = 11,
        Activities = 12,
        Cultural = 13,
        Subdivision = 14,
        Departmental = 15,
        Dormitory = 16,
        Employee = 17
    }
}