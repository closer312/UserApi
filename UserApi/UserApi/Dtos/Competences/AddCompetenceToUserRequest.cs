namespace UserApi.Dtos.Competences;

public class AddCompetenceToUserRequest
{
    public long UserId { get; set; }
    public long CompetenceId { get; set; }
}
