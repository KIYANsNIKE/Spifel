using FluentValidation.Results;
using Spifel.Domain.Common.Result;
namespace Spifel.Application.Shared.Validations
{
    public static class ValidationExtensions
    {
        public static Result ToResult(this ValidationResult validationResult)
        {
            if (validationResult.IsValid)
                return Result.Success();

            var errors = validationResult.Errors
                .Select(failure =>
                    Error.Validation(
                        code: string.IsNullOrWhiteSpace(failure.ErrorCode)
                            ? "ValidationError"
                            : failure.ErrorCode,
                        description: failure.ErrorMessage,
                        propertyName: failure.PropertyName))
                .ToList();

            return Result.Failure(errors);
        }
    }

}