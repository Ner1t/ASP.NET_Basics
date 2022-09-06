using System.ComponentModel.DataAnnotations;

namespace UsersAndRewardsNET.Models.ViewModel
{
    public class UserEditViewModel
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }

    }
}
