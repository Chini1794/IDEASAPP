using IDEASAPP.Models;
using IDEASAPP.ViewModels;
using IDEASAPP.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IDEASAPP.Views
{
    public partial class ComentariosPage : ContentPage
    {
        ComentariosViewModel _viewModel;

        public ComentariosPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new ComentariosViewModel();
			_viewModel.LoadAportesCommand.Execute(this);
		}


    }
}