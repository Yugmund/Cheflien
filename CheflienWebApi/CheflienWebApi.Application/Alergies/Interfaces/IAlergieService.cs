using CheflienWebApi.Application.Alergies.DTOs;

namespace CheflienWebApi.Application.Alergies.Interfaces;

public interface IAlergieService
{
    Task<IList<AlergieDto>> GetAllAsync();
} 