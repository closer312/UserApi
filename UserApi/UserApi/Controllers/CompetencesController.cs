using Microsoft.AspNetCore.Mvc;
using UserApi.Dtos.Competences;
using UserApi.Services.Competences;

namespace UserApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CompetencesController : ControllerBase
{
    private readonly ICompetenceService _competenceService;
    public CompetencesController(ICompetenceService competenceService)
    {
        _competenceService = competenceService;
    }
    [HttpGet("get-competences")]
    public async Task<List<CompetenceDto>> GetCompetences()
    {
        var result = await _competenceService.GetCompetencesAsync();
        return result;
    }
    [HttpGet("get-competence/{id}")]
    public async Task<CompetenceDto> GetCompetence(int id)
    {
        var result = await _competenceService.GetCompetenceAsync(id);
        return result;
    }
    [HttpPost("create-competence")]
    public async Task<IActionResult> CreateCompetence(string name, string description)
    {
        var result = await _competenceService.AddCompetenceAsync(name, description);
        return Ok(result);
    }
    [HttpPut("update-competence")]
    public async Task<IActionResult> UpdateCompetence(CompetenceDto competenceDto)
    {
        await _competenceService.UpdateCompetenceAsync(competenceDto);
        return Ok();
    }
    [HttpDelete("delete-competence/{id}")]
    public async Task<IActionResult> DeleteCompetence(int id)
    {
        await _competenceService.DeleteCompetenceAsync(id);
        return Ok();
    }

}
