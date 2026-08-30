using Spifel.Application.Extentions;
using Spifel.Application.Generator;
using Spifel.Application.Security;
using Spifel.Application.Services.Implementations.Features.Register;
using Spifel.Application.Services.Implementations.Features.Update;
using Spifel.Domain.Models.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace Spifel.Application.Mapper
{
    public static class Mapper
    {
        public static User ToUser(this RegisterDto dto)
        {
            return new User()
            {
                UserName = dto.UserName.FixUserName(),
                Email = dto.Email.FixEmail(),
                Password = PasswordHelper.HashPassword(dto.Password),
                Avatar = "NoPhoto.jpg",
                CreateDate = DateTime.Now,
                EmailActiveCode = UniqName.Generate(),
                IsActive = true,
                IsEmailActive = false
            };
        }


        //public static User ToUser(this UpdateDto dto, User user)
        //{
        //    // ایا متود رو out کنم؟
        //    user.UserName = dto.UserName.FixUserName();
        //    user.Email = dto.Email.FixEmail();
        //    user.Password = PasswordHelper.HashPassword(dto);
        //    user.Avatar = dto.Avatar;
        //    return user;
        //    //{
        //    //    UserName = dto.UserName.FixUserName(),
        //    //    Email = dto.Email.FixEmail(),
        //    //    Password = PasswordHelper.HashPassword(dto.Password),
        //    //    Avatar = dto.Avatar,

        //    //}
        //}

    }
}
