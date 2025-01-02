﻿using Business.Interfaces;
using Business.Services;
using Microsoft.Extensions.Logging;

namespace MobileApp;
//OBS ENDAST DESCTOP. Ansvarar inte för hur det fungerar eller ser ut i övriga miljöer.
public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });
        builder.Services.AddSingleton<IUserService, UserService>();
        builder.Services.AddSingleton<IFileService, FileService>();
        builder.Services.AddSingleton<UserManagementService>();



#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
