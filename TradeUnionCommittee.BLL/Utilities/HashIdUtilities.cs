using HashidsNet;

namespace TradeUnionCommittee.BLL.Utilities
{
    public interface IHashIdUtilities
    {
        long DecryptLong(string cipherText, Enums.Services service);
        string EncryptLong(long plainLong, Enums.Services service);
        bool CheckDecrypt(string cipherText, Enums.Services service, out long id);
    }

    internal sealed class HashIdUtilities : IHashIdUtilities
    {
        private const string Salt = "Development Salt";
        private const int MinHashLenght = 5;
        private const string Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
        private const string Seps = "TradeUnionCommittee.BLL.Assembly.Development.Seps";

        private Hashids ObjectHashids(Enums.Services service)
        {
            return new Hashids($"{Salt} {AdditionalSalt(service)}", MinHashLenght, Alphabet, Seps);
        }

        public string EncryptLong(long plainLong, Enums.Services service)
        {
            return ObjectHashids(service).EncodeLong(plainLong);
        }

        public long DecryptLong(string cipherText, Enums.Services service)
        {
            if (string.IsNullOrEmpty(cipherText) || string.IsNullOrWhiteSpace(cipherText))
            {
                return 0;
            }
            var decod = ObjectHashids(service).DecodeLong(cipherText);
            return decod[0];
        }

        public bool CheckDecrypt(string cipherText, Enums.Services service, out long id)
        {
            id = 0;
            if (string.IsNullOrEmpty(cipherText) || string.IsNullOrWhiteSpace(cipherText))
            {
                return false;
            }
            var result = DecryptLong(cipherText, service);
            if (result == 0) return false;
            id = result;
            return true;
        }

        private string AdditionalSalt(Enums.Services service)
        {
            switch (service)
            {
                case Enums.Services.Position:
                    return "PositionService";
                case Enums.Services.SocialActivity:
                    return "SocialActivityService";
                case Enums.Services.Privileges:
                    return "PrivilegesService";
                case Enums.Services.Award:
                    return "AwardService";
                case Enums.Services.MaterialAid:
                    return "MaterialAidService";
                case Enums.Services.Hobby:
                    return "HobbyService";
                case Enums.Services.Travel:
                    return "TravelService";
                case Enums.Services.Wellness:
                    return "WellnessService";
                case Enums.Services.Tour:
                    return "TourService";
                case Enums.Services.Activities:
                    return "ActivitiesService";
                case Enums.Services.Cultural:
                    return "CulturalService";
                case Enums.Services.Subdivision:
                    return "SubdivisionService";
                case Enums.Services.Departmental:
                    return "DepartmentalService";
                case Enums.Services.Dormitory:
                    return "DormitoryService";
                case Enums.Services.Employee:
                    return "EmployeeService";
                default:
                    return string.Empty;
            }
        }
    }
}