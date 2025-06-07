using CheflienWebApi.Domain.Entities;

namespace CheflienWebApi.Application.Alergies.Interfaces;

public interface IAlergieRepository
{
    Task<IList<Alergie>> GetAllAsync();
} 