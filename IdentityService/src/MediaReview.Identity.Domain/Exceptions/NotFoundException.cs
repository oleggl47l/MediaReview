namespace MediaReview.Identity.Domain.Exceptions;

public class NotFoundException(string message) : Exception(message);