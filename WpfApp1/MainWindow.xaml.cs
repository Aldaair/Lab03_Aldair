using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using ConsoleApp1;

namespace WpfApp1
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            List<Trabajador> trabajadoresList = ListarEmpleadosListaObjetos();
            dataGrid.ItemsSource = trabajadoresList;
        }

        private List<Trabajador> ListarEmpleadosListaObjetos()
        {
            List<Trabajador> trabajadores = new List<Trabajador>();

            // Llama a la función ListarEmpleadosListaObjetos desde el proyecto ConsoleApp1
            trabajadores = Program.ListarEmpleadosListaObjetos();

            return trabajadores;
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string searchText = searchTextBox.Text.Trim();

            if (!string.IsNullOrEmpty(searchText))
            {
                var searchResults = ((List<Trabajador>)dataGrid.ItemsSource).Where(student =>
                    student.Nombre.Contains(searchText) ||
                    student.Apellido.Contains(searchText)).ToList();

                dataGrid.ItemsSource = searchResults;
            }
            else
            {
                // Vuelve a mostrar la lista completa de trabajadores
                dataGrid.ItemsSource = ListarEmpleadosListaObjetos();
            }
        }
    }
}
