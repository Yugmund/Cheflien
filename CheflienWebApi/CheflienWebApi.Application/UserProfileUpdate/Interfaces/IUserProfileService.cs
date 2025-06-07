using CheflienWebApi.Application.UserProfileUpdate.DTOs;

namespace CheflienWebApi.Application.UserProfileUpdate.Interfaces;

public interface IUserProfileService
{
    Task<UserProfileDto> GetUserProfileAsync(string userId);
    Task<UserProfileDto> UpdateUserProfileAsync(string userId, UpdateUserProfileDto updateDto);
} 