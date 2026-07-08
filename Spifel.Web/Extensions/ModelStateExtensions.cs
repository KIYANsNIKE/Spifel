using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Spifel.Domain.Common.Result;

namespace Spifel.Web.Extensions
{
    public static class ModelStateExtensions
    {
        public static void AddFluentValidationErrors(
            this ModelStateDictionary modelState,
            ValidationResult validation)
        {
            modelState.Clear();
            foreach (var error in validation.Errors)
            {
                modelState.AddModelError(
                    error.PropertyName ?? string.Empty,
                    error.ErrorMessage);
            }
        }

        public static void AddResultErrors(
            this ModelStateDictionary modelState,
            IReadOnlyList<Error> errors)
        {
            modelState.Clear();
            foreach (var error in errors)
            {
                modelState.AddModelError(
                    error.PropertyName ?? string.Empty,
                    error.Description);
            }
        }
    }
}
