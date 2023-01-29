using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ColoShopEcommerce.WebApp.Models.ViewModel
{
    public class ManageUserRolesVM
    {
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public bool Selected { get; set; }
    }
}