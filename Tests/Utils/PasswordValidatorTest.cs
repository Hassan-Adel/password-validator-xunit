namespace password_validator_xunit.Tests.Utils;

using password_validator_xunit.Models;
using password_validator_xunit.Utils;
using Xunit;

public class PasswordValidatorTest
{
    private readonly IPasswordValidator validator;

    public PasswordValidatorTest()
    {
        validator = new PasswordValidator();
    }

    [Fact]
    public void Initialize_WithZeroMinLength_ThrowsIndexOutOfRangeException()
    {
        var passwordValidator = new PasswordValidator();

        Assert.Throws<IndexOutOfRangeException>(() =>
            passwordValidator.Initialize(0, 8, true, false));
    }

    [Fact]
    public void Initialize_WithMaxLengthGreaterThan255_ThrowsIndexOutOfRangeException()
    {
        var passwordValidator = new PasswordValidator();

        Assert.Throws<IndexOutOfRangeException>(() =>
            passwordValidator.Initialize(8, 256, true, false));
    }

    [Fact]
    public void Initialize_WithMinLengthGreaterThanMaxLength_ThrowsArgumentException()
    {
        var passwordValidator = new PasswordValidator();

        Assert.Throws<ArgumentException>(() =>
            passwordValidator.Initialize(8, 6, true, false));
    }

    [Fact]
    public void Validate_WhenPasswordIsNull_ReturnsIsTooShortError()
    {
        validator.Initialize(8, 20, true, true);

        var result = validator.Validate(null);

        Assert.Contains(ValidationErrorEnum.IsEmpty, result.Errors);
        Assert.False(result.IsCorrect);
    }

    [Fact]
    public void Validate_WhenPasswordIsEmpty_ReturnsIsEmptyError()
    {
        validator.Initialize(8, 20, true, true);

        var result = validator.Validate("");

        Assert.Contains(ValidationErrorEnum.IsEmpty, result.Errors);
        Assert.False(result.IsCorrect);
    }

    [Fact]
    public void Validate_WhenPasswordIsTooShort_ReturnsIsTooShortError()
    {
        validator.Initialize(8, 20, true, true);

        var result = validator.Validate("abcde");

        Assert.Contains(ValidationErrorEnum.IsTooShort, result.Errors);
        Assert.False(result.IsCorrect);
    }

    [Fact]
    public void Validate_WhenPasswordIsTooLong_ReturnsIsTooLongError()
    {
        validator.Initialize(8, 20, true, true);

        var result = validator.Validate("abcdefghijkmnopqrstuvwxyz1234567890");

        Assert.Contains(ValidationErrorEnum.IsTooLong, result.Errors);
        Assert.False(result.IsCorrect);
    }

    [Fact]
    public void Validate_WhenPasswordDoesNotContainDigitsAndCapitalLetters_ReturnsDoesNotContainDigitsAndCapitalLettersErrors()
    {
        validator.Initialize(8, 20, true, true);

        var result = validator.Validate("abcdefg");

        Assert.Contains(ValidationErrorEnum.DoesNotContainDigits, result.Errors);
        Assert.Contains(ValidationErrorEnum.DoesNotContainCapitalLetters, result.Errors);
        Assert.False(result.IsCorrect);
    }

    [Fact]
    public void Validate_WhenPasswordDoesNotContainDigits_ReturnsDoesNotContainDigitsError()
    {
        validator.Initialize(8, 20, true, true);

        var result = validator.Validate("Abcdefgh");

        Assert.Contains(ValidationErrorEnum.DoesNotContainDigits, result.Errors);
        Assert.False(result.IsCorrect);
    }

    [Fact]
    public void Validate_WhenPasswordDoesNotContainCapitalLetters_ReturnsDoesNotContainCapitalLettersError()
    {
        validator.Initialize(8, 20, true, true);

        var result = validator.Validate("abcdefg1");

        Assert.Contains(ValidationErrorEnum.DoesNotContainCapitalLetters, result.Errors);
        Assert.False(result.IsCorrect);
    }

    [Fact]
    public void Validate_WhenPasswordIsValid_ReturnsIsCorrect()
    {
        validator.Initialize(8, 20, true, true);

        var result = validator.Validate("Abcdefg1");

        Assert.True(result.IsCorrect);
        Assert.Empty(result.Errors);
    }
}
