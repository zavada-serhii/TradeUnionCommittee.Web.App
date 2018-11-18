using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using TradeUnionCommittee.BLL.DTO;

namespace TradeUnionCommittee.Mvc.Web.GUI.Configuration.DropDownLists
{
    public interface IDropDownList
    {
        Task<SelectList> GetRoles();
        Task<SelectList> GetLevelEducation();
        Task<SelectList> GetStudy();
        Task<SelectList> GetMainSubdivision();
        Task<List<SubdivisionDTO>> GetSubordinateSubdivisions(string hashId);
        Task<SelectList> GetPosition();
        Task<SelectList> GetDormitory();
        Task<SelectList> GetDepartmental();
        Task<SelectList> GetScientificTitle();
        Task<SelectList> GetAcademicDegree();
        Task<SelectList> GetSocialActivity();
        Task<SelectList> GetPrivilegies();
        Task<SelectList> GetHobby();
        Task<SelectList> GetTravel();
        Task<SelectList> GetWellness();
        Task<SelectList> GetTour();
        Task<SelectList> GetCultural();
        Task<SelectList> GetActivities();
        Task<SelectList> GetAward();
        Task<SelectList> GetMaterialAid();
    }
}
