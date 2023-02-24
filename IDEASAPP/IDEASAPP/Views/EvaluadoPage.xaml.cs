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
	public partial class EvaluadoPage : ContentPage
	{
		EvaluadoViewModel _viewModel;
		public EvaluadoPage ()
		{
			InitializeComponent ();
			BindingContext = _viewModel = new EvaluadoViewModel();
			_viewModel.LoadAportesCommand.Execute(this);
		}
	}
}