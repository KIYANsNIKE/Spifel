using System;
using System.Collections.Generic;
using System.Text;

namespace Spifel.Domain.Common.Result
{
    public class Result
    {
        protected Result(bool isSuccess, List<Error>? errors = null)
        {
            IsSuccess = isSuccess;
            Errors = errors ?? new List<Error>();
        }

        public bool IsSuccess { get; }
        public bool IsFailure => !IsSuccess;

        public IReadOnlyList<Error> Errors { get; }

        public static Result Success()
            => new(true);

        public static Result Failure(Error error)
            => new(false, new List<Error> { error });

        public static Result Failure(List<Error> errors)
            => new(false, errors);
    }
    public sealed class Result<T> : Result
    {
        private readonly T? _value;

        private Result(T value)
            : base(true)
        {
            _value = value;
        }

        private Result(List<Error> errors)
            : base(false, errors)
        {
            _value = default;
        }

        public T Value =>
            IsSuccess
                ? _value!
                : throw new InvalidOperationException(
                    "Cannot access Value when result is failure.");

        public static Result<T> Success(T value)
            => new(value);

        public static new Result<T> Failure(Error error)
            => new(new List<Error> { error });

        public static new Result<T> Failure(List<Error> errors)
            => new(errors);
    }
}
