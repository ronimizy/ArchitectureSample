using Application.Constants;
using Application.Dto;
using DataAccess.Models;

namespace Application.Mapping;

public static class AccountMapping
{
    public static AccountDto AsDto(this Account account)
    {
        return new AccountDto(account.Id, account.Name, GetRole(account).ToString("G"));
    }

    private static AccountRole GetRole(Account account)
    {
        return (account.AllowGroupCreation, account.AllowStudentCreation) switch
        {
            (true, true) => AccountRole.SuperAdministrator,
            (true, false) => AccountRole.IsuAdministrator,
            (false, true) => AccountRole.FacultyAdministrator,
            _ => AccountRole.RegularAdministrator,
        };
    }
}