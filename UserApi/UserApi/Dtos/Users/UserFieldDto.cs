namespace UserApi.Dtos.Users;

public class UserFieldDto
{
    public int Id { get; set; }
    public int FieldId { get; set; }
    public FieldDto? Field { get; set; }
    public string? FieldValue { get; set; }
}
