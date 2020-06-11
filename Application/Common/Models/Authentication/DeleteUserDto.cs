using System.ComponentModel.DataAnnotations;

namespace Application.Common.Models.Authentication
{
    public class DeleteUserDto
    {
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}