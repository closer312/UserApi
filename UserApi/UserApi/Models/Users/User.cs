using UserApi.Models.Competences;

namespace UserApi.Models.Users;

public class User : BaseEntity
{
    public ICollection<Competence> Competences { get; set; } = new List<Competence>();
    public ICollection<UserField> UserFields { get; set; } = new List<UserField>();
}
