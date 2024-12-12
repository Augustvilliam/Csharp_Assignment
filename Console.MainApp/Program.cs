using Business.Interfaces;
using Business.Services;

IFileService fileService = new FileService();
IUserService userService = new UserService(fileService);
var importExportService = new ImportExportService(userService, fileService);
var userManagementService = new UserManagementService(userService);
var mainMenuService = new MainMenuService(userManagementService, importExportService);

// Visa huvudmenyn
mainMenuService.ShowMenu();




//Hela detta kodstycket generarat av ChatGPT .4o då jag var lat.