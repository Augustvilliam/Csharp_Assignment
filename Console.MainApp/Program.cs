

using Business.Services;

var userService = new UserService();
var fileService = new FileService();

var userManagementService = new UserManagementService(userService);
var importExportService = new ImportExportService(fileService);

var mainMenuService = new MainMenuService(userManagementService, importExportService);

mainMenuService.ShowMenu();

//Hela detta kodstycket generarat av ChatGPT .4o då jag var lat.