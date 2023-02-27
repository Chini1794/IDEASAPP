using IDEASAPP.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace IDEASAPP.ViewModels
{
	[QueryProperty(nameof(EmpresaId), nameof(EmpresaId))]
	public class NuevoComentarioViewModel : BaseViewModel
    {
    
        private string description;

		public ICommand ChangeOptionCommand { get; set; }
		private string empresaId;

		public NuevoComentarioViewModel()
        {
            SaveCommand = new Command(GuardarAporte, ValidateSave);
            CancelCommand = new Command(OnCancel);
            Description = Application.Current.Properties["idUsuario"].ToString();
			ChangeOptionCommand = new Command<KeyValuePair<string, string>>(ChangeOption);
			this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }
		void ChangeOption(KeyValuePair<string, string> selectedOption)
		{
			Debug.WriteLine(selectedOption);
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

		private int categoriaAporte;

		public int CategoriaAporte
		{
			get => categoriaAporte;
			set => SetProperty(ref categoriaAporte, value);


		}			
		private int calificacion;

		public int Calificacion
		{
			get => calificacion;
			set => SetProperty(ref calificacion, value);


		}		
		private int tipoAporte;

		public int TipoAporte
		{
			get => tipoAporte;
			set => SetProperty(ref tipoAporte, value);
		}
		private async void GuardarAporte()
		{

			Aporte nuevoAporte = new Aporte
			{
				CNegocio = Convert.ToInt32(EmpresaId),
				FechaAporte = DateTime.Now,
				FechaRespuesta = DateTime.Now,
				CTipoAporte = TipoAporte,
				CCategoriaAporte = CategoriaAporte,
				DComentario = Description.ToString(),
				CCalificacion = Calificacion,
				DRespuesta ="",
				CEstado = 1,
			};

			Uri requestUri = new Uri("https://felino.vitalit.co.cr/api/api/Aporte");
			var client = new HttpClient();
			var json = JsonConvert.SerializeObject(nuevoAporte);
			var contentJson = new StringContent(json, Encoding.UTF8, "application/json");

			var response = await client.PostAsync(requestUri, contentJson);

			Aporte aporteAgregado = await GetLastAporte();
			if (response.StatusCode == HttpStatusCode.OK)
			{
				AportesPersona aportePersona = new AportesPersona();
				aportePersona.CPersona = (int)Application.Current.Properties["idUsuario"];
				aportePersona.CAporte = aporteAgregado.Id;
				HttpResponseMessage aux = await AgregarAportePersona(aportePersona);
				if (aux.StatusCode == HttpStatusCode.OK) {
					await Shell.Current.GoToAsync($"..");
				}
				else
				{

					await Application.Current.MainPage.DisplayAlert("Mensaje", "Error Inesperado", "OK");
				}


			}
			else
			{

				await Application.Current.MainPage.DisplayAlert("Mensaje", "Error Inesperado", "OK");
			}

		}
		async Task<Aporte> GetLastAporte()
		{
			ObservableCollection<Aporte> aportesPersona = new ObservableCollection<Aporte>();
			var request = new HttpRequestMessage();
			request.RequestUri = new Uri("https://felino.vitalit.co.cr/api/api/Aporte");
			request.Method = HttpMethod.Get;

			var client = new HttpClient();
			HttpResponseMessage response = await client.SendAsync(request);

			string content = await response.Content.ReadAsStringAsync();
			var resultado = JsonConvert.DeserializeObject<ObservableCollection<Aporte>>(content);


			return resultado.Last();

		}
		private async Task<HttpResponseMessage> AgregarAportePersona(AportesPersona aportePersona)
		{

			Uri requestUri = new Uri("https://felino.vitalit.co.cr/api/api/AportesPersona");
			var client = new HttpClient();
			var json = JsonConvert.SerializeObject(aportePersona);
			var contentJson = new StringContent(json, Encoding.UTF8, "application/json");

			var response = await client.PostAsync(requestUri, contentJson);

			return response;
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
