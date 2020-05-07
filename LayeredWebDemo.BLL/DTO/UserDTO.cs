using LayeredWebDemo.DAL.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace LayeredWebDemo.BLL.DTO
{
    public class UserDTO
    {
        [Display(Name = "Id")]
        public string Id { get; set; }

        [Display(Name = "UserNAme")]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
       
        [Phone]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Required]
        [Display(Name = "Firs Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "About Me")]
        public string AboutMe { get; set; }

        [Display(Name = "Date Of Birth")]
        public DateTime DateOfBirth { get; set; }

        [Display(Name = "Gender")]
        public Gender Gender { get; set; }

        [Display(Name = "Address")]
        public string Address { get; set; }

    }
}