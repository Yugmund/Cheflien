using FluentResults;

namespace CheflienWebApi.Application.Authorization.Errors;

public class InvalidCredentialsError () : Error("Invalid email or password");