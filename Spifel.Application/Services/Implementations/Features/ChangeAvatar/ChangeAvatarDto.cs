using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Spifel.Application.Services.Implementations.Features.ChangeAvatar
{
    public class ChangeAvatarDto
    {
        public int UserId { get; set; }
        public string Avatar { get; set; }
        public IFormFile AvatarFile { get; set; }

    }
}
