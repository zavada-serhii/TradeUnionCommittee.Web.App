using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.DTO;
using TradeUnionCommittee.BLL.Interfaces.Account;
using TradeUnionCommittee.BLL.Interfaces.Directory;

namespace TradeUnionCommittee.Web.GUI.AdditionalSettings.DropDownLists
{
    public class DropDownList : IDropDownList
    {
        private readonly IAccountService _accountService;
        private readonly IEducationService _educationService;
        private readonly ISubdivisionsService _subdivisionsService;
        private readonly IPositionService _positionService;
        private readonly IDormitoryService _dormitoryService;
        private readonly IDepartmentalService _departmentalService;
        private readonly IQualificationService _scientificService;
        private readonly ISocialActivityService _socialActivity;
        private readonly IPrivilegesService _privilegesService;
        private readonly IHobbyService _hobbyService;
        private readonly ITravelService _travelService;
        private readonly IWellnessService _wellnessService;
        private readonly ITourService _tourService;
        private readonly ICulturalService _culturalService;
        private readonly IActivitiesService _activitiesService;
        private readonly IAwardService _awardService;
        private readonly IMaterialAidService _materialAidService;


        public DropDownList(IAccountService accountService,
                            IEducationService educationService,
                            ISubdivisionsService subdivisionsService,
                            IPositionService positionService,
                            IDormitoryService dormitoryService,
                            IDepartmentalService departmentalService,
                            IQualificationService scientificService,
                            ISocialActivityService socialActivity,
                            IPrivilegesService privilegesService,
                            IHobbyService hobbyService,
                            ITravelService travelService,
                            IWellnessService wellnessService,
                            ITourService tourService,
                            ICulturalService culturalService,
                            IActivitiesService activitiesService,
                            IAwardService awardService,
                            IMaterialAidService materialAidService)
        {
            _accountService = accountService;
            _educationService = educationService;
            _subdivisionsService = subdivisionsService;
            _positionService = positionService;
            _dormitoryService = dormitoryService;
            _departmentalService = departmentalService;
            _scientificService = scientificService;
            _socialActivity = socialActivity;
            _privilegesService = privilegesService;
            _hobbyService = hobbyService;
            _travelService = travelService;
            _wellnessService = wellnessService;
            _tourService = tourService;
            _culturalService = culturalService;
            _activitiesService = activitiesService;
            _awardService = awardService;
            _materialAidService = materialAidService;
        }

        public async Task<SelectList> GetRoles()
        {
            var roles = await _accountService.GetRoles();
            return roles.IsValid ? new SelectList(roles.Result, "HashId", "Name") : null;
        }

        public async Task<SelectList> GetLevelEducation()
        {
            var levelEducation = await _educationService.GetAllLevelEducationAsync();
            return levelEducation.IsValid ? new SelectList(levelEducation.Result) : null;
        }

        public async Task<SelectList> GetStudy()
        {
            var nameInstitution = await _educationService.GetAllNameInstitutionAsync();
            return nameInstitution.IsValid ? new SelectList(nameInstitution.Result) : null;
        }

        public async Task<SelectList> GetMainSubdivision()
        {
            var subdivision = await _subdivisionsService.GetAllAsync();
            return subdivision.IsValid ? new SelectList(subdivision.Result, "HashId", "Name") : null;
        }

        public async Task<List<SubdivisionDTO>> GetSubordinateSubdivisions(string hashId)
        {
            var subordinateSubdivision = await _subdivisionsService.GetSubordinateSubdivisions(hashId);
            List<SubdivisionDTO> listSubordinateSubdivision = null;
            if (subordinateSubdivision.IsValid && subordinateSubdivision.Result.Any())
            {
                listSubordinateSubdivision = new List<SubdivisionDTO>();
                listSubordinateSubdivision.AddRange(subordinateSubdivision.Result);
            }
            return listSubordinateSubdivision;
        }

        public async Task<SelectList> GetPosition()
        {
            var position = await _positionService.GetAllAsync();
            return position.IsValid ? new SelectList(position.Result, "Id", "Name") : null;
        }

        public async Task<SelectList> GetDormitory()
        {
            var dormitory = await _dormitoryService.GetAllAsync();
            return dormitory.IsValid ? new SelectList(dormitory.Result, "Id", "NumberDormitory") : null;
        }

        public async Task<SelectList> GetDepartmental()
        {
            var departmental = await _departmentalService.GetAllShortcut();
            return departmental.IsValid ? new SelectList(departmental.Result, "Key", "Value") : null;
        }

        public async Task<SelectList> GetScientificTitle()
        {
            var scientificTitle = await _scientificService.GetAllScientificTitleAsync();
            return scientificTitle.IsValid ? new SelectList(scientificTitle.Result) : null;
        }

        public async Task<SelectList> GetAcademicDegree()
        {
            var academicDegree = await _scientificService.GetAllScientificDegreeAsync();
            return academicDegree.IsValid ? new SelectList(academicDegree.Result) : null;
        }

        public async Task<SelectList> GetSocialActivity()
        {
            var socialActivity = await _socialActivity.GetAllAsync();
            return socialActivity.IsValid ? new SelectList(socialActivity.Result, "Id", "Name") : null;
        }

        public async Task<SelectList> GetPrivilegies()
        {
            var privilegies = await _privilegesService.GetAllAsync();
            return privilegies.IsValid ? new SelectList(privilegies.Result, "Id", "Name") : null;
        }

        public async Task<SelectList> GetHobby()
        {
            var hobby = await _hobbyService.GetAllAsync();
            return hobby.IsValid ? new SelectList(hobby.Result, "Id", "Name") : null;
        }

        public async Task<SelectList> GetTravel()
        {
            var travel = await _travelService.GetAllAsync();
            return travel.IsValid ? new SelectList(travel.Result, "Id", "Name") : null;
        }

        public async Task<SelectList> GetWellness()
        {
            var wellness = await _wellnessService.GetAllAsync();
            return wellness.IsValid ? new SelectList(wellness.Result, "Id", "Name") : null;
        }

        public async Task<SelectList> GetTour()
        {
            var tour = await _tourService.GetAllAsync();
            return tour.IsValid ? new SelectList(tour.Result, "Id", "Name") : null;
        }

        public async Task<SelectList> GetCultural()
        {
            var cultural = await _culturalService.GetAllAsync();
            return cultural.IsValid ? new SelectList(cultural.Result, "Id", "Name") : null;
        }

        public async Task<SelectList> GetActivities()
        {
            var activities = await _activitiesService.GetAllAsync();
            return activities.IsValid ? new SelectList(activities.Result, "Id", "Name") : null;
        }

        public async Task<SelectList> GetAward()
        {
            var award = await _awardService.GetAllAsync();
            return award.IsValid ? new SelectList(award.Result, "Id", "Name") : null;
        }

        public async Task<SelectList> GetMaterialAid()
        {
            var materialAid = await _materialAidService.GetAllAsync();
            return materialAid.IsValid ? new SelectList(materialAid.Result, "Id", "Name") : null;
        }
    }
}