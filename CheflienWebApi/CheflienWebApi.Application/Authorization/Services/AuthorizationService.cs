using BookingWebApi.Application.Authorization.Wrappers;
using CheflienWebApi.Application.Authorization.Dtos;
using CheflienWebApi.Application.Authorization.Errors;
using CheflienWebApi.Application.Authorization.Jwts;
using CheflienWebApi.Application.Authorization.Wrappers;
using CheflienWebApi.Domain.Entities;
using FluentResults;
using Mapster;
using Microsoft.AspNetCore.Identity;

namespace CheflienWebApi.Application.Authorization.Services;

public interface IAuthorizationService
{
    Task<Result<string>> RegisterUser(RegisterDto registerDto);
    Task<Result<string>> LoginUser(LoginDto loginDto);
}

public class AuthorizationService(
        IUserManagerWrapper _userManager, 
        IRoleManagerWrapper _roleManager,
        IJwtTokenGenerator _jwtTokenGenerator
    ) : IAuthorizationService
{
    public async Task<Result<string>> RegisterUser(RegisterDto registerDto)
    {
        var existingUser = await _userManager.FindByEmailAsync(registerDto.Email);
        if (existingUser != null)
            return Result.Fail(new UserAlreadyExistsError());
        
        var role = await _roleManager.FindByNameAsync(registerDto.Role);
        if (role == null)
            return Result.Fail(new RoleNotFoundError());
        
        var user = registerDto.Adapt<User>();

        user.UserRoles =
        [
            new IdentityUserRole<string>
            {
                UserId = user.Id,
                RoleId = role.Id
            }
        ];
        
        var result = await _userManager.CreateAsync(user, registerDto.Password);
        if (!result.Succeeded)
        {
            var errors = result.Errors.Select(e => e.Description).ToList();
            return Result.Fail<string>(string.Join(", ", errors));
        }
        
        return Result.Ok(user.Id);
    }

    public async Task<Result<string>> LoginUser(LoginDto loginDto)
    {
        var user = await _userManager.FindByEmailAsync(loginDto.Email);
        
        if (user == null)
        {
            return Result.Fail(new UserDoesNotExistError());
        }

        var roles = await _userManager.GetRolesAsync(user);

        var result = await _userManager.CheckPasswordAsync(user, loginDto.Password);
        if (result)
        {
            var token = _jwtTokenGenerator.GenerateToken(user, roles);
            return Result.Ok(token);
        }

        return Result.Fail(new InvalidCredentialsError());
    }
}