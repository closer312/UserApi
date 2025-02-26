using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UserApi.Data;
using UserApi.Dtos.Competences;
using UserApi.Models.Competences;

namespace UserApi.Services.Competences.Impl;

public class CompetenceService : ICompetenceService
{
    private readonly AppPostgreSqlDbContext _context;
    private readonly IMapper _mapper;
    public CompetenceService(AppPostgreSqlDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }


    public async Task<long> AddCompetenceAsync(string name, string description)
    {
        var competence = new Competence
        {
            Name = name,
            Description = description
        };
        await _context.Competences.AddAsync(competence);
        await _context.SaveChangesAsync();

        return competence.Id;
    }

    public async Task AddCompetenceToUserAsync(AddCompetenceToUserRequest request)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == request.UserId);
        if (user == null)
            throw new Exception("Пользователь не найден в базе данных.");

        var competence = await _context.Competences.FirstOrDefaultAsync(x => x.Id == request.CompetenceId);
        if (competence == null)
            throw new Exception("Компетенция не найдена в базе данных.");

        user.Competences.Add(competence);
        await _context.SaveChangesAsync();

    }

    public async Task DeleteCompetenceAsync(int id)
    {
        var competence = await _context.Competences.FirstOrDefaultAsync(x => x.Id == id);
        if (competence == null)
            throw new Exception("Компетенция не найдена в базе данных.");

        _context.Competences.Remove(competence);
        await _context.SaveChangesAsync();
    }

    public async Task<CompetenceDto> GetCompetenceAsync(int id)
    {
        var competence = await _context.Competences.FirstOrDefaultAsync(x => x.Id == id);
        if (competence == null)
            throw new Exception("Компетенция не найдена в базе данных.");
        var result = _mapper.Map<CompetenceDto>(competence);
        return result;
    }

    public async Task<List<CompetenceDto>> GetCompetencesAsync()
    {
        var competences = await _context.Competences.ToListAsync();

        var result = _mapper.Map<List<CompetenceDto>>(competences);

        return result;
    }

    public async Task UpdateCompetenceAsync(CompetenceDto competenceDto)
    {
        var competence = await _context.Competences.FirstOrDefaultAsync(x => x.Id == competenceDto.Id);
        if (competence == null)
            throw new Exception("Компетенция не найдена в базе данных.");

        competence = _mapper.Map(competenceDto, competence);
        await _context.SaveChangesAsync();
    }
}
