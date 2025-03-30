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
                await DisplayAlert("Error", "Por favor ingrese su correo electr�nico.", "OK");
                return;
            }

            // Comprobar si el usuario existe usando el servicio de base de datos
            DatabaseService databaseService = new DatabaseService();
            var usuario = await databaseService.GetUserByEmail(email);

            if (usuario == null)
            {
                await DisplayAlert("Error", "No existe un usuario con este correo electr�nico.", "OK");
                return;
            }

            // Obtener la contrase�a actual del usuario
            string contrasenaActual = usuario.Password;

            // Enviar la contrase�a por correo
            bool correoEnviado = EnviarCorreo(email, contrasenaActual);

            if (correoEnviado)
            {
                await DisplayAlert("Recuperaci�n", "Se ha enviado su contrase�a actual a su correo electr�nico.", "OK");
                await Navigation.PopToRootAsync(); // Redirigir a la p�gina de inicio
            }
            else
            {
                await DisplayAlert("Error", "No se pudo enviar el correo. Int�ntelo de nuevo.", "OK");
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
                mensaje.Subject = "Recuperaci�n de Contrase�a";
                mensaje.Body = $"Su contrase�a actual es: {contrasena}\nPor favor, mant�ngala en un lugar seguro.";
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
