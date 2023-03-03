using IDEASAPP.Models;
using IDEASAPP.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IDEASAPP.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NuevoComentarioPage : ContentPage
    {

		NuevoComentarioViewModel _viewModel;
		public NuevoComentarioPage()
        {
            InitializeComponent();
			BindingContext = _viewModel = new NuevoComentarioViewModel();

			
		}

		void Categoria_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			var selectedOption = (sender as Picker).SelectedIndex;
			_viewModel.CategoriaAporte = Convert.ToString(selectedOption +1);
		}	
		void Tipo_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			var selectedOption = (sender as Picker).SelectedIndex;
			_viewModel.TipoAporte = Convert.ToString(selectedOption + 1);
		}
	}
}