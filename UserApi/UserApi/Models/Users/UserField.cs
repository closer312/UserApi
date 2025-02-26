namespace UserApi.Models.Users;

public class UserField : BaseEntity
{
    public long UserId { get; set; }
    public User? User { get; set; }
    public long FieldId { get; set; }
    public Field? Field { get; set; }
    public string? FieldValue { get; set; }
}
