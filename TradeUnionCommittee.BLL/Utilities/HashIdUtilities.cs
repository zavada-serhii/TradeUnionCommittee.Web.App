using HashidsNet;

namespace TradeUnionCommittee.BLL.Utilities
{
    public class HashIdUtilitiesSetting
    {
        public string Salt { get; set; }
        public int MinHashLenght { get; set; }
        public string Alphabet { get; set; }
        public string Seps { get; set; }
    }

    public interface IHashIdUtilities
    {
        long DecryptLong(string cipherText, Enums.Services service);
        string EncryptLong(long plainLong, Enums.Services service);
        bool CheckDecrypt(string cipherText, Enums.Services service, out long id);
    }

    internal sealed class HashIdUtilities : IHashIdUtilities
    {
        private readonly string _salt;
        private readonly int _minHashLenght;
        private readonly string _alphabet;
        private readonly string _seps;

        public HashIdUtilities(HashIdUtilitiesSetting setting)
        {
            _salt = setting.Salt;
            _minHashLenght = setting.MinHashLenght;
            _alphabet = setting.Alphabet;
            _seps = setting.Seps;
        }

        private Hashids ObjectHashids(Enums.Services service)
        {
            return new Hashids($"{_salt} {AdditionalSalt(service)}", _minHashLenght, _alphabet, _seps);
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