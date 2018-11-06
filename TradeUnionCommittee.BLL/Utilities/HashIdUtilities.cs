using HashidsNet;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.Exceptions;
using TradeUnionCommittee.Common.ActualResults;
using TradeUnionCommittee.Common.Enums;

namespace TradeUnionCommittee.BLL.Utilities
{
    public class HashIdUtilitiesSetting
    {
        public string Salt { get; set; }
        public int MinHashLenght { get; set; }
        public string Alphabet { get; set; }
        public string Seps { get; set; }
        public bool UseGuidFormat { get; set; }
    }

    public interface IHashIdUtilities
    {
        long DecryptLong(string cipherText, Enums.Services service);
        string EncryptLong(long plainLong, Enums.Services service);
        Task<ActualResult<long>> CheckDecryptWithId(string hashId, Enums.Services service);
    }

    internal sealed class HashIdUtilities : IHashIdUtilities
    {
        private readonly string _salt;
        private readonly int _minHashLenght;
        private readonly string _alphabet;
        private readonly string _seps;
        private readonly bool _useGuidFormat;

        public HashIdUtilities(HashIdUtilitiesSetting setting)
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

        private Hashids ObjectHashids(Enums.Services service)
        {
            return new Hashids($"{_salt} {AdditionalSalt(service)}", _minHashLenght, _alphabet, _seps);
        }

        public string EncryptLong(long plainLong, Enums.Services service)
        {
            return _useGuidFormat ? GuidFormat(ObjectHashids(service).EncodeLong(plainLong), true) : ObjectHashids(service).EncodeLong(plainLong);
        }

        public long DecryptLong(string cipherText, Enums.Services service)
        {
            if (string.IsNullOrEmpty(cipherText) || string.IsNullOrWhiteSpace(cipherText))
            {
                return 0;
            }
            var result = ObjectHashids(service).DecodeLong(_useGuidFormat ? GuidFormat(cipherText, false) : cipherText);

            if (result.Length == 1)
            {
                return result[0];
            }
            throw new DecryptHashIdException($"{service}");
        }

        public async Task<ActualResult<long>> CheckDecryptWithId(string hashId, Enums.Services service)
        {
            return await Task.Run(() => CheckDecrypt(hashId, service, out long id) ? new ActualResult<long> { Result = id } : new ActualResult<long>(Errors.InvalidId));
        }

        private bool CheckDecrypt(string cipherText, Enums.Services service, out long id)
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

        private string GuidFormat(string hash, bool addOrRemoveMinus)
        {
            return addOrRemoveMinus ? hash.Insert(8, "-").Insert(13, "-").Insert(18, "-").Insert(23, "-") : hash.Replace("-", string.Empty);
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
                case Enums.Services.Qualification:
                    return "QualificationService";
                case Enums.Services.Education:
                    return "EducationService";
                default:
                    return string.Empty;
            }
        }
    }
}