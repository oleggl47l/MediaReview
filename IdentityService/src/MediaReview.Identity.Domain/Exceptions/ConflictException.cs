namespace MediaReview.Identity.Domain.Exceptions;

public class ConflictException(string message) : Exception(message);