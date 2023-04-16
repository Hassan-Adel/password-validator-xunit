using System.Reflection;
using password_validator_xunit.Models;
using password_validator_xunit.Utils;
using Xunit;
using Xunit.Sdk;

namespace password_validator_xunit.Tests.Utils;

// I don't recommend this approach it uses fewer lines/tests but at the ccost of readabiity and maintainability
public class PasswordValidatorCutomTests
{
    private readonly PasswordValidator validator;

    public PasswordValidatorCutomTests()
    {
        validator = new PasswordValidator();
    }

    [Theory]
    [CustomValidationData]
    public void Validate_ReturnsExpectedResult(string password, bool expectedResult, ValidationErrorEnum[] expectedErrors)
    {
        // Arrange
        validator.Initialize(6, 12, true, true);

        // Act
        var result = validator.Validate(password);

        // Assert
        Assert.Equal(expectedResult, result.IsCorrect);
        Assert.Equal(expectedErrors, result.Errors);
    }

    private class CustomValidationDataAttribute : DataAttribute
    {
        public override IEnumerable<object[]> GetData(MethodInfo testMethod)
        {
            yield return new object[] { "123456", false, new[] { ValidationErrorEnum.DoesNotContainCapitalLetters } };
            yield return new object[] { "ABCDEFGHIJK", false, new[] { ValidationErrorEnum.DoesNotContainDigits } };
            yield return new object[] { "123456789", false, new[] { ValidationErrorEnum.DoesNotContainCapitalLetters } };
            yield return new object[] { "Abcde", false, new[] { ValidationErrorEnum.IsTooShort, ValidationErrorEnum.DoesNotContainDigits } };
            yield return new object[] { "abcdefghijkl", false, new[] { ValidationErrorEnum.DoesNotContainDigits, ValidationErrorEnum.DoesNotContainCapitalLetters } };
            yield return new object[] { "ABCDEFGHIJKL", false, new[] { ValidationErrorEnum.DoesNotContainDigits } };
            yield return new object[] { "Abcd1234", true, Array.Empty<ValidationErrorEnum>() };
            yield return new object[] { "Abcde1234", true, Array.Empty<ValidationErrorEnum>() };
            yield return new object[] { "Abcd12345678", true, Array.Empty<ValidationErrorEnum>() };
            yield return new object[] { "Abcdef123456", true, Array.Empty<ValidationErrorEnum>() };
            yield return new object[] { "Abcdefghij12", true, Array.Empty<ValidationErrorEnum>() };
            yield return new object[] { "ABCDEFGHIJ123", false, new[] { ValidationErrorEnum.IsTooLong } };
        }
    }
}
