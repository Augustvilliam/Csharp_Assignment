using Microsoft.Extensions.DependencyInjection;
using Business.Interfaces;
using Business.Services;



var serviceProvider = new ServiceCollection()
    .AddSingleton<IUserService, UserService>()
    .AddSingleton<UserManagementService>()
    .AddSingleton<IFileService, fileService>()
    .BuildServiceProvider();

var userManagementService = serviceProvider.GetRequiredService<UserManagementService>();
userManagementService.ShowMenu();







//Hela detta kodstycket generarat av ChatGPT .4o då jag var lat.