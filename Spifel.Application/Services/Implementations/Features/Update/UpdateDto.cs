using System;
using System.Collections.Generic;
using System.Text;

namespace Spifel.Application.Services.Implementations.Features.Update
{
    public class UpdateDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string NationalCode { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string BirthDate { get; set; }
    }
}
