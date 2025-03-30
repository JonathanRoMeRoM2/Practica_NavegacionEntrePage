using Microsoft.Extensions.Logging;
using System.IO;

namespace Practica_NavegacionEntrePage;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();

        // Definir la ruta de la base de datos
        App.DatabasePath = Path.Combine(FileSystem.AppDataDirectory, "usuarios.db3");

        builder
            .UseMauiApp<App>() // Ahora App ya tiene DatabasePath configurado
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
