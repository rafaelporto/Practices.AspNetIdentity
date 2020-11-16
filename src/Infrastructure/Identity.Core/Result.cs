using System.Collections.Generic;
using System.Linq;

namespace Identity.Core
{
	public record Result<T>
	{
		public bool IsSuccess => true;
		public bool IsFailure => !IsSuccess;
		public T Value { get; set; }
		private List<string> _errors;

		public IReadOnlyList<string> Errors => _errors;


		private Result(T value) => Value = value;
		private Result(IEnumerable<string> errors) => _errors = errors.ToList();
		private Result(string error) => _errors = new List<string>() { error };

		public static Result<T> Ok(T value) => new Result<T>(value);
		public static Result<T> Fail(IEnumerable<string> errors) => new Result<T>(errors);
		public static Result<T> Fail(string error) => new Result<T>(error);

		public override string ToString() =>
			$"IsSuccess: {IsSuccess}, Value: " + Value.ToString();
	}
}
