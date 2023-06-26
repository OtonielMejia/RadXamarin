using RADXamarin.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;


namespace RADXamarin
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            LlenarDatos();
        }

        private async void btnRegistar_Clicked(object sender, EventArgs e)
        {
            if (validarDatos())
            {
                Contactos contacto = new Contactos
                {
                    Nombres = txtNombres.Text,
                    Apellidos = txtApellidos.Text,
                    Edad = Convert.ToInt32(txtEdad.Text), 
                    Pais = CombxPais.SelectedItem.ToString(),
                    Nota = txtNota.Text,

                    
                };
                await App.SQLiteDB.SaveContactoAsync(contacto);
                await DisplayAlert("Registro", "Nuevo contacto agregado", "OK");
                LlenarDatos();
                BorrarCasillas();
            }
            else
            {
                await DisplayAlert("Advertencia", "Ingresar todos los datos", "OK");
            }
        }

        public  async void LlenarDatos()
        {
            var contacList = await App.SQLiteDB.GetContactoAsync();
            if (contacList != null)
            {
                listContactos.ItemsSource = contacList;
            }
        }

        public void BorrarCasillas()
        {
            txtIdContaco.Text = "";
            txtNombres.Text = "";
            txtApellidos.Text = "";
            txtEdad.Text = "";
            CombxPais.SelectedItem = "Pais";
            txtNota.Text = "";
            btnRegistar.IsVisible = true;
            txtIdContaco.IsVisible = false;
            btnActualizar.IsVisible = false;
            btnEliminar.IsVisible = false;

        }


        public bool validarDatos() {
            bool respuesta;
            if (string.IsNullOrEmpty(txtNombres.Text))
            {
                respuesta = false;
            }
            else if (string.IsNullOrEmpty(txtApellidos.Text))
            {
                respuesta = false;
            }
            else if (string.IsNullOrEmpty(txtEdad.Text))
            {
                respuesta = false;
            }
            else if (string.IsNullOrEmpty(txtNota.Text))
            {
                respuesta = false;
            }
            else
            {
                respuesta = true;
            }
            return respuesta;
        }

        private async void btnActualizar_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtIdContaco.Text))
            {
                Contactos contactos = new Contactos() {
                    IdContacto = Convert.ToInt32(txtIdContaco.Text),
                    Nombres = txtNombres.Text,
                    Apellidos = txtApellidos.Text,
                    Edad = Convert.ToInt32(txtEdad.Text),
                    Pais = CombxPais.ToString(),
                };
                await App.SQLiteDB.SaveContactoAsync(contactos);
                await DisplayAlert("Registro", "Contacto Actualizado", "OK");
                LlenarDatos();
                BorrarCasillas();
            }
        }

        private async void listContactos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var obj = (Contactos)e.SelectedItem;
            btnRegistar.IsVisible = false;
            txtIdContaco.IsVisible = true;
            btnActualizar.IsVisible = true;
            btnEliminar.IsVisible = true;

            if(!string.IsNullOrEmpty(obj.IdContacto.ToString()))
            {
                var Contacto = await App.SQLiteDB.GetContactoByIdAsync(obj.IdContacto);
                if (Contacto != null) {
                    txtIdContaco.Text = Contacto.IdContacto.ToString();
                    txtNombres.Text = Contacto.Nombres.ToString();
                    txtApellidos.Text = Contacto.Apellidos.ToString();
                    txtEdad.Text = Contacto.Edad.ToString();
                    CombxPais.SelectedItem = Contacto.Pais.ToString();
                    txtNota.Text = Contacto.Nota.ToString();
                }
            }
        }

        private async void btnEliminar_Clicked(object sender, EventArgs e)
        {
            var contacto = await App.SQLiteDB.GetContactoByIdAsync(Convert.ToInt32(txtIdContaco.Text));
            if (contacto != null)
            {
                await App.SQLiteDB.DeleteContactoAsync(contacto);
                await DisplayAlert("registro","Se elimino de manera exitosa","OK");
                BorrarCasillas();
                LlenarDatos();
            }
        }
    }
    
}
