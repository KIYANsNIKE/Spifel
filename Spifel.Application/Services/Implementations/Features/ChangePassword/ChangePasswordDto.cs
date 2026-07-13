using System;
using System.Collections.Generic;
using System.Text;

namespace Spifel.Application.Services.Implementations.Features.ChangePassword
{
    public class ChangePasswordDto
    {
        public int UserId { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
