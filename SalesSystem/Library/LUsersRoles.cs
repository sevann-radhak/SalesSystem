using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesSystem.Library
{
    public class LUsersRoles
    {
        public List<SelectListItem> GetRoles(RoleManager<IdentityRole> roleManager)
        {
            List<SelectListItem> _selectList = new List<SelectListItem>();
            roleManager.Roles.OrderBy(role => role.Name).ToList().ForEach(role =>
            _selectList.Add(new SelectListItem { Value = role.Id, Text = role.Name }));

            return _selectList;
        }
    }
}
