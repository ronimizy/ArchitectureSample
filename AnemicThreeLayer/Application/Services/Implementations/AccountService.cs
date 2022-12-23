using System.Security.Cryptography;
using System.Text;
using Application.Dto;
using Application.Extensions;
using Application.Mapping;
using DataAccess;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.Implementations;

internal sealed class AccountService : IAccountService
{
    private readonly DatabaseContext _context;

    public AccountService(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<AccountDto> CreateAccount(string name, string password)
    {
        ArgumentNullException.ThrowIfNull(name, nameof(name));
        ArgumentNullException.ThrowIfNull(password, nameof(password));

        string passwordHash = GetPasswordHash(password);

        var account = new Account(Guid.NewGuid(), name, passwordHash);
        _context.Accounts.Add(account);
        await _context.SaveChangesAsync();

        return account.AsDto();
    }

    public async Task<AccountDto> FindAccount(string name, string password)
    {
        ArgumentNullException.ThrowIfNull(name, nameof(name));
        ArgumentNullException.ThrowIfNull(password, nameof(password));

        string passwordHash = GetPasswordHash(password);

        Account account = await _context.Accounts
            .SingleOrDefaultAsync(x => x.Name == name && x.PasswordHash == passwordHash);

        return account?.AsDto();
    }

    public async Task<AccountDto> GrantStudentCreation(Guid accountId)
    {
        Account account = await _context.Accounts.GetEntityAsync(accountId, CancellationToken.None);
        account.AllowStudentCreation = true;
        await _context.SaveChangesAsync();

        return account.AsDto();
    }

    public async Task<AccountDto> GrantGroupCreation(Guid accountId)
    {
        Account account = await _context.Accounts.GetEntityAsync(accountId, CancellationToken.None);
        account.AllowGroupCreation = true;
        await _context.SaveChangesAsync();

        return account.AsDto();
    }

    private static string GetPasswordHash(string password)
    {
        using var hashingAlgorithm = SHA256.Create();
        byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
        return BitConverter.ToString(hashingAlgorithm.ComputeHash(passwordBytes));
    }
}