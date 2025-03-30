using System;
using System.Net.Mail;
using System.Net;
using Microsoft.Maui.Controls;
using System.Text;
using SQLite;
using Practica_NavegacionEntrePage.Models;
using Practica_NavegacionEntrePage.Services;

namespace Practica_NavegacionEntrePage.Views
{
    public partial class ForgotPasswordPage : ContentPage
    {
        public ForgotPasswordPage()
        {
            InitializeComponent();
        }

        private async void Enviar_Clicked(object sender, EventArgs e)
        {
            string email = EmailEntry.Text?.Trim();

            if (string.IsNullOrEmpty(email))
            {
                await DisplayAlert("Error", "Por favor ingrese su correo electrónico.", "OK");
                return;
            }

            // Comprobar si el usuario existe usando el servicio de base de datos
            DatabaseService databaseService = new DatabaseService();
            var usuario = await databaseService.GetUserByEmail(email);

            if (usuario == null)
            {
                await DisplayAlert("Error", "No existe un usuario con este correo electrónico.", "OK");
                return;
            }

            // Obtener la contraseña actual del usuario
            string contrasenaActual = usuario.Password;

            // Enviar la contraseña por correo
            bool correoEnviado = EnviarCorreo(email, contrasenaActual);

            if (correoEnviado)
            {
                await DisplayAlert("Recuperación", "Se ha enviado su contraseña actual a su correo electrónico.", "OK");
                await Navigation.PopToRootAsync(); // Redirigir a la página de inicio
            }
            else
            {
                await DisplayAlert("Error", "No se pudo enviar el correo. Inténtelo de nuevo.", "OK");
            }
        }

        private bool EnviarCorreo(string destinatario, string contrasena)
        {
            try
            {
                string remitente = "proyectospruebas10@gmail.com";
                string clave = "qzyj cels rsqb axsm";
                string smtpServer = "smtp.gmail.com";
                int smtpPort = 587;

                MailMessage mensaje = new MailMessage();
                mensaje.From = new MailAddress(remitente);
                mensaje.To.Add(destinatario);
                mensaje.Subject = "Recuperación de Contraseña";
                mensaje.Body = $"Su contraseña actual es: {contrasena}\nPor favor, manténgala en un lugar seguro.";
                mensaje.IsBodyHtml = false;

                SmtpClient smtp = new SmtpClient(smtpServer, smtpPort);
                smtp.Credentials = new NetworkCredential(remitente, clave);
                smtp.EnableSsl = true;

                smtp.Send(mensaje);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al enviar correo: {ex.Message}");
                return false;
            }
        }
    }
}
