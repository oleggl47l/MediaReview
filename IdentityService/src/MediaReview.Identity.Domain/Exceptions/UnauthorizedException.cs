﻿namespace MediaReview.Identity.Domain.Exceptions;

public class UnauthorizedException(string message) : Exception(message);