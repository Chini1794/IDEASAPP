using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace IDEASAPP.ViewModels
{
    public class EvaluadoViewModel : BaseViewModel
	{

		public Command LinkCommand { get; }
		public EvaluadoViewModel() {

			LinkCommand = new Command(OnLinkTapped);
		}
		private void OnLinkTapped(object obj)
		{
			if (obj is Frame sl)
			{
				if (sl.BackgroundColor == Color.FromHex("#9EA1A3"))
				{
					sl.BackgroundColor = Color.White;
				}
				else
				{
					sl.BackgroundColor = Color.FromHex("#9EA1A3");
				}
			}
		}

	}
}
