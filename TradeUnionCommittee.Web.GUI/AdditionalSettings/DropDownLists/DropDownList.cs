using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.Interfaces.Account;
using TradeUnionCommittee.BLL.Interfaces.Directory;

namespace TradeUnionCommittee.Web.GUI.DropDownLists
{
    public class DropDownList : IDropDownList
    {
        private readonly IAccountService _accountService;
        private readonly ISubdivisionsService _subdivisionsService;
        private readonly IPositionService _positionService;
        private readonly IDormitoryService _dormitoryService;
        private readonly IDepartmentalService _departmentalService;


        public DropDownList(IAccountService accountService,
                            ISubdivisionsService subdivisionsService,
                            IPositionService positionService,
                            IDormitoryService dormitoryService,
                            IDepartmentalService departmentalService)
        {
            _accountService = accountService;
            _subdivisionsService = subdivisionsService;
            _positionService = positionService;
            _dormitoryService = dormitoryService;
            _departmentalService = departmentalService;
        }

        public async Task<SelectList> GetRoles()
        {
            var roles = await _accountService.GetRoles();
            return roles.IsValid ? new SelectList(roles.Result, "Id", "Name") : null;
        }

        public Task<SelectList> GetLevelEducation()
        {
            throw new NotImplementedException();
        }

        public Task<SelectList> GetStudy()
        {
            throw new NotImplementedException();
        }

        public async Task<SelectList> GetMainSubdivision()
        {
            var subdivision = await _subdivisionsService.GetAllAsync();
            return subdivision.IsValid ? new SelectList(subdivision.Result, "Id", "DeptName") : null;
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

        public Task<SelectList> GetScientificTitle()
        {
            throw new NotImplementedException();
        }

        public Task<SelectList> GetAcademicDegree()
        {
            throw new NotImplementedException();
        }

        public Task<SelectList> GetSocialActivity()
        {
            throw new NotImplementedException();
        }

        public Task<SelectList> GetPrivilegies()
        {
            throw new NotImplementedException();
        }

        public Task<SelectList> GetHobby()
        {
            throw new NotImplementedException();
        }

        public Task<SelectList> GetTravel()
        {
            throw new NotImplementedException();
        }

        public Task<SelectList> GetWellness()
        {
            throw new NotImplementedException();
        }

        public Task<SelectList> GetTour()
        {
            throw new NotImplementedException();
        }

        public Task<SelectList> GetCultural()
        {
            throw new NotImplementedException();
        }

        public Task<SelectList> GetActivities()
        {
            throw new NotImplementedException();
        }
    }
}