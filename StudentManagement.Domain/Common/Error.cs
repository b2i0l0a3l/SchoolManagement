namespace StudentManagement.Domain.Common
{
    public enum ErrorType
    {
        NotFound,
        Validation,
        General
    }

    public record Error(string Id, ErrorType Type, string Description);

    public static class Errors
    {
        public static Error UserNotFoundError { get; } = new("UserNotFound", ErrorType.NotFound, "User Not Found.");
        public static Error EmailAlreadyExistsError { get; } = new("EmailFoundError", ErrorType.General, "Email already exists.");
       
    }
}
