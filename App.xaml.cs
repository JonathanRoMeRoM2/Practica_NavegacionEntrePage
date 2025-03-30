using System;
using System.IO;
using SQLite;
using Practica_NavegacionEntrePage.Models;

namespace Practica_NavegacionEntrePage
{
    public partial class App : Application
    {
        public static string DatabasePath { get; set; } = string.Empty;

        public App()
        {
            InitializeComponent();

            // Inicializar la ruta de la base de datos
            string dbName = "usuarios.db3";
            string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            DatabasePath = Path.Combine(folderPath, dbName);

            // Opcional: crear la base de datos y tablas si no existen
            using (SQLiteConnection conn = new SQLiteConnection(DatabasePath))
            {
                conn.CreateTable<User>();

                // Si quieres verificar si hay usuarios y crear uno de ejemplo si no hay ninguno
                if (conn.Table<User>().Count() == 0)
                {
                    // Crear usuario de prueba (solo para desarrollo)
                    var usuarioPrueba = new User
                    {
                        Email = "jonathanromeo3@gmail.com",
                        Password = "contraseña123"
                        // Otros campos necesarios
                    };
                    conn.Insert(usuarioPrueba);
                }
            }

            MainPage = new NavigationPage(new MainPage());
        }
    }
}