
using FluentValidation.Results;

namespace ChitChat.Application.Exceptions;

public class ValidationException : Exception
{
    public IDictionary<string, string[]> Errors { get; }

    public ValidationException(IEnumerable<ValidationFailure> failures) : base(BuildMessage(failures))
    {
        Errors = failures
            .GroupBy(f => f.PropertyName)
            .ToDictionary(
                g => g.Key,
                g => g.Select(f => f.ErrorMessage).Distinct().ToArray()
            );
    }

    private static string BuildMessage(IEnumerable<ValidationFailure> failures)
    {
        var parts = failures.Select(f => $"{f.PropertyName}: {f.ErrorMessage}");
        return string.Join(" | ", parts);
    }
}
