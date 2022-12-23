using Application.Services;
using Application.Services.Implementations;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection collection)
    {
        collection.AddScoped<IStudentService, StudentService>();
        collection.AddScoped<IStudentGroupService, StudentGroupService>();
        collection.AddScoped<ITransferOrderService, TransferOrderService>();
        collection.AddScoped<IAccountService, AccountService>();

        return collection;
    }
}