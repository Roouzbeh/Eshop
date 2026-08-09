using IDP.Domain.Entities.BaseEntities;

namespace IDP.Domain.Entities
{
    public class User:BaseEntity
    {
        //abstract
        public required string FullName { get; set; }
        public required string NationalCode { get; set; }
        public required string UserName { get; set; }
        public required string Password { get; set; }
        public required string Salt { get; set; }
        public string? Email { get; set; }
        public required string MobileNumber { get; set; }

    }
}
