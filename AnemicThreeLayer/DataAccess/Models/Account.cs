namespace DataAccess.Models;

public class Account
{
    public Account(Guid id, string name, string passwordHash)
    {
        Id = id;
        Name = name;
        PasswordHash = passwordHash;
    }

    protected Account()
    {
    }

    public Guid Id { get; set; }

    public string Name { get; set; }

    public string PasswordHash { get; set; }

    public bool AllowGroupCreation { get; set; }

    public bool AllowStudentCreation { get; set; }
}