using password_validator_xunit.Models;

namespace password_validator_xunit.Utils;

/// <summary>
/// Interface for a password validator.
/// </summary>
public interface IPasswordValidator
{
    /// <summary>
    /// Initialize a password validator.
    /// </summary>
    /// <param name="minLength">The minimum length of the password.</param>
    /// <param name="maxLength">The maximum length of the password.</param>
    /// <param name="mustContainDigits">Whether the password must contain at least one digit.</param>
    /// <param name="mustContainCapitalLetters">Whether the password must contain at least one capital letter.</param>
    /// <exception cref="IndexOutOfRangeException">Thrown if minLength is less than or equal to zero, or if maxLength is greater than 255.</exception>
    /// <exception cref="ArgumentException">Thrown if minLength is greater than maxLength.</exception>
    void Initialize(int minLength, int maxLength, bool mustContainDigits, bool mustContainCapitalLetters);

    /// <summary>
    /// Validates a provided password.
    /// </summary>
    /// <param name="password">The password to validate.</param>
    /// <returns>A validator result.</returns>
    ValidationResult Validate(string password);
}

/// <summary>
/// The result of validating a password.
/// </summary>
public class ValidationResult
{
    /// <summary>
    /// Returns true only if a password matches all the rules.
    /// </summary>
    public bool IsCorrect { get; set; }

    /// <summary>
    /// If IsCorrect is true then this property returns an empty array. Otherwise it returns all found errors.
    /// Errors cannot be duplicated. This property must not be null.
    /// </summary>
    public ValidationErrorEnum[] Errors { get; set; }
}
