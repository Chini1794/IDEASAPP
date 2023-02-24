using IDEASAPP.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace IDEASAPP.ViewModels
{
	[QueryProperty(nameof(EmpresaId), nameof(EmpresaId))]
	public class NuevoComentarioViewModel : BaseViewModel
    {
    
        private string description;


		private string empresaId;

		public NuevoComentarioViewModel()
        {
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            Description = Application.Current.Properties["idUsuario"].ToString();

			this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }

        private bool ValidateSave()
        {
            return  !String.IsNullOrWhiteSpace(description);
        }

        public string Description
        {
            get => description;
            set => SetProperty(ref description, value);
        }

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        private async void OnSave()
        {
            Item newItem = new Item()
            {
                Id = Guid.NewGuid().ToString(),
                Description = Description
            };

            await DataStore.AddItemAsync(newItem);

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

		public string EmpresaId
		{
			get
			{
				return empresaId;
			}
			set
			{
				empresaId = value;
				LoadEmpresaId(value);
			}
		}


		public async void LoadEmpresaId(string id)
		{
			var request = new HttpRequestMessage();
			request.RequestUri = new Uri("https://felino.vitalit.co.cr/api/api/NegocioMiembro/" + id);
			request.Method = HttpMethod.Get;

			var client = new HttpClient();
			HttpResponseMessage response = await client.SendAsync(request);
			string content = await response.Content.ReadAsStringAsync();
			var resultado = JsonConvert.DeserializeObject<NegocioMiembro>(content);
		}
	}
}
