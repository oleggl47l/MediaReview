namespace MediaReview.Identity.Domain.Exceptions;

public class ValidationException(IEnumerable<string> errors) : Exception("One or more validation errors occurred.");