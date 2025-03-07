using Microsoft.Maui.Controls;
using Practica_NavegacionEntrePage.ViewModels;

namespace Practica_NavegacionEntrePage.Views
{
    public partial class RegisterPage : ContentPage
    {
        public RegisterViewModel ViewModel { get; set; }

        public RegisterPage()
        {
            InitializeComponent();
            ViewModel = new RegisterViewModel();
            BindingContext = ViewModel;
        }

        private void OnRegisterClicked(object sender, EventArgs e)
        {
            // Llamar a la lógica del ViewModel para registrar al usuario
            ViewModel.Register();
        }
    }
}
