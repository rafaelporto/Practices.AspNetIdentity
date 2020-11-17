using System.Collections.Generic;
using System.Linq;

namespace Identity.Core
{
	public readonly struct Result<T>
	{
		public bool IsSuccess => true;
		public bool IsFailure => !IsSuccess;
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
