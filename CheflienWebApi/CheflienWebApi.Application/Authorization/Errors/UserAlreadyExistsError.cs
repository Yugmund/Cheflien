using FluentResults;

namespace CheflienWebApi.Application.Authorization.Errors;

public class UserAlreadyExistsError() : Error("User with this email already exists");