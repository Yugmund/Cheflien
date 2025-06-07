using CheflienWebApi.Application.Alergies.DTOs;
using CheflienWebApi.Application.Alergies.Interfaces;

namespace CheflienWebApi.Application.Alergies.Services;

public class AlergieService(IAlergieRepository alergieRepository) : IAlergieService
{
    public async Task<IList<AlergieDto>> GetAllAsync()
    {
        var alergies = await alergieRepository.GetAllAsync();
        return alergies.Select(a => new AlergieDto
        {
            Id = a.Id,
            Name = a.Name,
            Description = a.Description
        }).ToList();
    }
} 