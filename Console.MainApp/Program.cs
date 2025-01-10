using Microsoft.Extensions.DependencyInjection;
using Business.Interfaces;
using Business.Services;
using Business.Factory;
using Business.Helper;



var serviceProvider = new ServiceCollection() //laddar in allt med DI, första 3 raderna generarades med chatGPT, resten är tilllagd för hand. 
    .AddSingleton<IUserService, UserService>()
    .AddSingleton<UserManagementService>()
    .AddSingleton<IFileService, FileService>()
    .AddSingleton<IUserFactory, UserFactory>()
    .AddTransient<IUserValidation, UserValidation>()

    .BuildServiceProvider();

var userManagementService = serviceProvider.GetRequiredService<UserManagementService>();
userManagementService.ShowMenu();







