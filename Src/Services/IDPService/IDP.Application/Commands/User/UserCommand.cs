using MediatR;
using System.ComponentModel.DataAnnotations;

namespace IDP.Application.Commands.User
{
    public record UserCommand:IRequest<bool>
    {
        [Required(ErrorMessage ="name is required")]
        [MinLength(4)]
        [MaxLength(20)]
        public string FullName { get; set; }

        public string NationalCode { get; set; }
        public string Email { get; set; }

     }
}
