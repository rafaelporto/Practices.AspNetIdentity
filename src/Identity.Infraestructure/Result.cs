using System.Collections.Generic;
using System.Linq;

namespace Identity.Infraestructure
{
	public readonly struct Result
	{
		public bool IsSuccess => IsFailure is false;
		public bool IsFailure => Errors?.Any() is true;
		public IReadOnlyList<string> Errors { get; init; }

		private Result(IEnumerable<string> errors = null) =>
			Errors = errors?.ToList() ?? default;

		public static Result Success() => new Result();
		public static Result Failure(IEnumerable<string> errors) => new Result(errors);
		public static Result Failure(string error) => new Result(new string[] { error });

		public static Result<T> Success<T>(T value) => Result<T>.Ok(value);
		public static Result<T> Failure<T>(IEnumerable<string> errors) => Result<T>.Fail(errors);
		public static Result<T> Failure<T>(string error) => Result<T>.Fail(error);
	}

	public readonly struct Result<T>
	{
		public bool IsSuccess => !IsFailure;
		public bool IsFailure => Errors is not null && Errors.Any();
		public T Value { get; init; }
		public IReadOnlyList<string> Errors { get; init; }

		private Result(T value) => (Value, Errors) = (value, new List<string>());
		private Result(IEnumerable<string> errors) => (Value, Errors) = ( default, errors.ToList());
		private Result(string error) => (Value, Errors) = (default, new List<string>() { error });

		public static Result<T> Ok(T value) => new Result<T>(value);
		public static Result<T> Fail(IEnumerable<string> errors) => new Result<T>(errors);
		public static Result<T> Fail(string error) => new Result<T>(error);

		public override string ToString() =>
			$"IsSuccess: {IsSuccess}, Value Type: {nameof(T)}, Value: {Value?.ToString()}";
	}
}
