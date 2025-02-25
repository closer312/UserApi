using UserApi.Constants;
using UserApi.Enums;
using UserApi.Models.Users;

namespace UserApi.Data;

public class DataSeed
{
    public static void SeedData(AppPostgreSqlDbContext context)
    {
        if (!context.Fields.Any())
        {
            var fields = new List<Field>
            {
                new() { Name = FieldNamesConstants.UserName, FieldType = FieldType.Text.ToString() },
                new() { Name = FieldNamesConstants.Avatar, FieldType = FieldType.Id.ToString() },
                new() { Name = FieldNamesConstants.Gender, FieldType = FieldType.Text.ToString() },
                new() { Name = FieldNamesConstants.DateOfBirth, FieldType = FieldType.Text.ToString() },
                new() { Name = FieldNamesConstants.TimeOfBirth, FieldType = FieldType.Text.ToString() },
                new() { Name = FieldNamesConstants.PlaceOfBirth, FieldType = FieldType.Text.ToString() },
                new() { Name = FieldNamesConstants.CurrentLocation, FieldType = FieldType.Text.ToString() },
            };
            context.Fields.AddRange(fields);
            context.SaveChanges();
        }
    }
}
