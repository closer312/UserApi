using UserApi.Dtos.Competences;

namespace UserApi.Services.Competences;

public interface ICompetenceService
{
    Task<long> AddCompetenceAsync(string name, string description);
    Task<List<CompetenceDto>> GetCompetencesAsync();
    Task<CompetenceDto> GetCompetenceAsync(int id);
    Task UpdateCompetenceAsync(CompetenceDto competenceDto);
    Task DeleteCompetenceAsync(int id);
}
