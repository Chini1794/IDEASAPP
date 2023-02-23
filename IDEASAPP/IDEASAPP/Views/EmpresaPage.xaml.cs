using IDEASAPP.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IDEASAPP.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EmpresaPage : ContentPage
	{
		EmpresaViewModel _viewModel;
		public EmpresaPage ()
		{
			InitializeComponent ();
			BindingContext = _viewModel = new EmpresaViewModel();
			_viewModel.LoadAportesCommand.Execute(this);
		}

	}
}