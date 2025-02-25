using UserApi.Models.Users;

namespace UserApi.Models.Competences;

public class Competence : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public ICollection<User> users { get; set; }
}

