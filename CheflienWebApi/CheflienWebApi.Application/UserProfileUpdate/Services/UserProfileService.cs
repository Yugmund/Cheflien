using CheflienWebApi.Application.UserProfileUpdate.DTOs;
using CheflienWebApi.Application.UserProfileUpdate.Interfaces;

namespace CheflienWebApi.Application.UserProfileUpdate.Services;

public class UserProfileService(IUserRepository userRepository) : IUserProfileService
{
    public async Task<UserProfileDto> GetUserProfileAsync(string userId)
    {
        var user = await userRepository.GetByIdAsync(userId);
        if (user == null)
            throw new KeyNotFoundException($"User with ID {userId} not found.");

        var userAlergies = await userRepository.GetUserAlergiesAsync(userId);

        return new UserProfileDto
        {
            Id = user.Id,
            UserName = user.UserName,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            Alergies = userAlergies.Select(a => new AlergieDto
            {
                Id = a.Id,
                Name = a.Name,
                Description = a.Description
            }).ToList()
        };
    }

    public async Task<UserProfileDto> UpdateUserProfileAsync(string userId, UpdateUserProfileDto updateDto)
    {
        var user = await userRepository.GetByIdAsync(userId);
        if (user == null)
            throw new KeyNotFoundException($"User with ID {userId} not found.");

        // Update basic user information
        user.UserName = updateDto.UserName;
        user.Email = updateDto.Email;
        user.PhoneNumber = updateDto.PhoneNumber;

        await userRepository.UpdateAsync(user);
        await userRepository.UpdateUserAlergiesAsync(userId, updateDto.AlergieIds);

        return await GetUserProfileAsync(userId);
    }
} 