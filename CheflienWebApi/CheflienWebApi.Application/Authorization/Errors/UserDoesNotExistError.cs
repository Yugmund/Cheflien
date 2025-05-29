using FluentResults;

namespace CheflienWebApi.Application.Authorization.Errors;

public class UserDoesNotExistError() : Error("User with this email does not exist");
