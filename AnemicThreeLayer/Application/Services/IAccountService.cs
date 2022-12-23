using Application.Dto;

namespace Application.Services;

public interface IAccountService
{
    Task<AccountDto> CreateAccount(string name, string password);

    Task<AccountDto> FindAccount(string name, string password);

    Task<AccountDto> GrantStudentCreation(Guid accountId);

    Task<AccountDto> GrantGroupCreation(Guid accountId);
}