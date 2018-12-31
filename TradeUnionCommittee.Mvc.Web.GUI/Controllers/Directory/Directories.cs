using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using TradeUnionCommittee.BLL.DTO;
using TradeUnionCommittee.BLL.Interfaces.Account;
using TradeUnionCommittee.BLL.Interfaces.Directory;

namespace TradeUnionCommittee.Mvc.Web.GUI.Controllers.Directory
{
    public class Directories : IDirectories
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


        public Directories(IAccountService accountService,
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
            return roles.IsValid ? new SelectList(roles.Result, "Name", "Name") : null;
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
            return subdivision.IsValid ? new SelectList(subdivision.Result, "HashIdMain", "Name") : null;
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

        public async Task<IEnumerable<SelectListItem>> GetTreeSubdivisions(string hashIdSelectedValue = null)
        {
            var treeSubdivisions = await _subdivisionsService.GetTreeSubdivisions();
            var subdivision = treeSubdivisions.ToList();
            var group = subdivision.Select(m => new SelectListGroup { Name = m.GroupName }).ToList();
            var items = new List<SelectListItem>();
            var j = 0;
            for (var i = 0; i < subdivision.Count; i++)
            {
                for (; j < group.Count;)
                {
                    items.AddRange(subdivision.ElementAt(i).Subdivisions.Select(subdivisionDto => new SelectListItem
                    {
                        Value = subdivisionDto.HashIdMain,
                        Text = subdivisionDto.Name,
                        Group = group[j],
                        Selected = subdivisionDto.HashIdMain == hashIdSelectedValue
                    }));
                    break;
                }
                j++;
            }
            return items;
        }

        public async Task<SelectList> GetPosition(string hashIdSelectedValue = null)
        {
            var position = await _positionService.GetAllAsync();
            return position.IsValid ? new SelectList(position.Result, "HashId", "Name", hashIdSelectedValue) : null;
        }

        public async Task<SelectList> GetDormitory(string hashIdSelectedValue = null)
        {
            var dormitory = await _dormitoryService.GetAllAsync();
            return dormitory.IsValid ? new SelectList(dormitory.Result, "HashId", "NumberDormitory", hashIdSelectedValue) : null;
        }

        public async Task<SelectList> GetDepartmental(string hashIdSelectedValue = null)
        {
            var departmental = await _departmentalService.GetAllShortcut();
            return departmental.IsValid ? new SelectList(departmental.Result, "Key", "Value", hashIdSelectedValue) : null;
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

        public async Task<SelectList> GetSocialActivity(string hashIdSelectedValue = null)
        {
            var socialActivity = await _socialActivity.GetAllAsync();
            return socialActivity.IsValid ? new SelectList(socialActivity.Result, "HashId", "Name", hashIdSelectedValue) : null;
        }

        public async Task<SelectList> GetPrivilegies(string hashIdSelectedValue = null)
        {
            var privilegies = await _privilegesService.GetAllAsync();
            return privilegies.IsValid ? new SelectList(privilegies.Result, "HashId", "Name", hashIdSelectedValue) : null;
        }

        public async Task<SelectList> GetHobby(string hashIdSelectedValue = null)
        {
            var hobby = await _hobbyService.GetAllAsync();
            return hobby.IsValid ? new SelectList(hobby.Result, "HashId", "Name", hashIdSelectedValue) : null;
        }

        public async Task<SelectList> GetTravel()
        {
            var travel = await _travelService.GetAllAsync();
            return travel.IsValid ? new SelectList(travel.Result, "HashId", "Name") : null;
        }

        public async Task<SelectList> GetWellness()
        {
            var wellness = await _wellnessService.GetAllAsync();
            return wellness.IsValid ? new SelectList(wellness.Result, "HashId", "Name") : null;
        }

        public async Task<SelectList> GetTour()
        {
            var tour = await _tourService.GetAllAsync();
            return tour.IsValid ? new SelectList(tour.Result, "HashId", "Name") : null;
        }

        public async Task<SelectList> GetCultural()
        {
            var cultural = await _culturalService.GetAllAsync();
            return cultural.IsValid ? new SelectList(cultural.Result, "HashId", "Name") : null;
        }

        public async Task<SelectList> GetActivities()
        {
            var activities = await _activitiesService.GetAllAsync();
            return activities.IsValid ? new SelectList(activities.Result, "HashId", "Name") : null;
        }

        public async Task<SelectList> GetAward()
        {
            var award = await _awardService.GetAllAsync();
            return award.IsValid ? new SelectList(award.Result, "HashId", "Name") : null;
        }

        public async Task<SelectList> GetMaterialAid()
        {
            var materialAid = await _materialAidService.GetAllAsync();
            return materialAid.IsValid ? new SelectList(materialAid.Result, "HashId", "Name") : null;
        }
    }

    public interface IDirectories
    {
        Task<SelectList> GetRoles();
        Task<SelectList> GetLevelEducation();
        Task<SelectList> GetStudy();
        Task<SelectList> GetMainSubdivision();
        Task<List<SubdivisionDTO>> GetSubordinateSubdivisions(string hashId);
        Task<IEnumerable<SelectListItem>> GetTreeSubdivisions(string hashIdSelectedValue = null);
        Task<SelectList> GetPosition(string hashIdSelectedValue = null);
        Task<SelectList> GetDormitory(string hashIdSelectedValue = null);
        Task<SelectList> GetDepartmental(string hashIdSelectedValue = null);
        Task<SelectList> GetScientificTitle();
        Task<SelectList> GetAcademicDegree();
        Task<SelectList> GetSocialActivity(string hashIdSelectedValue = null);
        Task<SelectList> GetPrivilegies(string hashIdSelectedValue = null);
        Task<SelectList> GetHobby(string hashIdSelectedValue = null);
        Task<SelectList> GetTravel();
        Task<SelectList> GetWellness();
        Task<SelectList> GetTour();
        Task<SelectList> GetCultural();
        Task<SelectList> GetActivities();
        Task<SelectList> GetAward();
        Task<SelectList> GetMaterialAid();
    }
}