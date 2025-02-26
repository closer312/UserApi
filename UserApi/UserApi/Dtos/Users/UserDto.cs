using UserApi.Dtos.Competences;

namespace UserApi.Dtos.Users;

public class UserDto
{
    public int Id { get; set; }
    public ICollection<CompetenceDto> Competences { get; set; } = new List<CompetenceDto>();
    public List<UserFieldDto> UserFields { get; set; } = new List<UserFieldDto>();
}
