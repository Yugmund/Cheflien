using CheflienWebApi.Application.Alergies.Interfaces;
using CheflienWebApi.Domain.Entities;
using CheflienWebApi.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace CheflienWebApi.Infrastructure.Repositories;

public class AlergieRepository(ApplicationDbContext context) : IAlergieRepository
{
    public async Task<IList<Alergie>> GetAllAsync()
    {
        return await context.Alergies.ToListAsync();
    }
} 