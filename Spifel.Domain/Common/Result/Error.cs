using System;
using System.Collections.Generic;
using System.Text;

namespace Spifel.Domain.Common.Result
{
    public sealed class Error
    {
        private Error(
            string code,
            string description,
            ErrorType type,
            string? propertyName = null)
        {
            Code = code;
            Description = description;
            Type = type;
            PropertyName = propertyName;
        }

        public string Code { get; }
        public string Description { get; }
        public ErrorType Type { get; }
        public string? PropertyName { get; }

        // ===== Factory Methods =====

        public static Error Validation(string code, string description, string propertyName)
            => new(code, description, ErrorType.Validation, propertyName);

        public static Error NotFound(string code, string description)
            => new(code, description, ErrorType.NotFound);

        public static Error Conflict(string code, string description)
            => new(code, description, ErrorType.Conflict);

        public static Error Unauthorized(string code, string description)
            => new(code, description, ErrorType.Unauthorized);

        public static Error Forbidden(string code, string description)
            => new(code, description, ErrorType.Forbidden);

        public static Error Failure(string code, string description)
            => new(code, description, ErrorType.Failure);

        public static Error NotActive(string code, string description)
            => new(code, description, ErrorType.NotActive);
    }
}
