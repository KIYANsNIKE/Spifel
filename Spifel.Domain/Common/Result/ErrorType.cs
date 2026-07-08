using System;
using System.Collections.Generic;
using System.Text;

namespace Spifel.Domain.Common.Result
{
    public enum ErrorType
    {
        None = 0,
        Validation = 1,
        NotFound = 2,
        Conflict = 3,
        Unauthorized = 4,
        Forbidden = 5,
        Failure = 6,
        NotActive = 7,
    }
    public enum ErrorCode
    {
        None = 0,

        // Validation
        Required,
        InvalidEmail,
        InvalidUserName,
        UserNameTooShort,
        UserNameToolong,
        PasswordTooShort,
        PasswordWeak,
        PasswordMismatch,

        // Business
        UserAlreadyExists,
        EmailAlreadyExists,
        UserNotFound,
        UserNotActive,

        // System
        UnexpectedError
    }
}
