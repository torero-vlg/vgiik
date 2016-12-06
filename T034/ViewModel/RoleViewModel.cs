using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace T034.ViewModel
{
    public class RoleViewModel
    {
        public RoleViewModel()
        {
            WebPermissions = new List<WebPermissionViewModel>();
        }

        [Display(Name = "Идентификатор")]
        public int Id { get; set; }
        
        [Display(Name = "Название")]
        public string Name { get; set; }
        
        [Display(Name = "Код")]
        public string Code { get; set; }
        
        public bool Selected { get; set; }

        public List<WebPermissionViewModel> WebPermissions { get; set; }
    }
}