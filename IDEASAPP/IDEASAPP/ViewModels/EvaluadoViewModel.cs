using IDEASAPP.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace IDEASAPP.ViewModels
{
	public class EvaluadoViewModel : BaseViewModel
	{
		public ObservableCollection<Aporte> Aportes { get; }

		public Command LoadAportesCommand { get; }
		public EvaluadoViewModel()
		{
			Aportes = new ObservableCollection<Aporte>();
			LoadAportesCommand = new Command(async () => await LoadAportes());
		}
		async Task LoadAportes()
		{

			ObservableCollection<Aporte> aux = new ObservableCollection<Aporte>();
			ObservableCollection<AportesPersona> aportesPersona = new ObservableCollection<AportesPersona>();



			aportesPersona = await GetAportesPersona(Convert.ToInt32(Application.Current.Properties["idUsuario"]));

			foreach (AportesPersona aportePersona in aportesPersona)
			{
				Aporte aporte = await GetAporte(aportePersona.CAporte);
				aux.Add(aporte);
			}

			Aportes.Clear();


			foreach (var item in aux.OrderByDescending(x => x.FechaAporte))
			{
				Calificacion calificacion = await GetCalificacion(item.CCalificacion);
				CategoriaAporte categoriaAporte = await GetCategoriaAporte(item.CCategoriaAporte);
				Estado estado = await GetEstado(item.CEstado);
				TipoAporte tipoAporte = await GetTipoAporte(item.CTipoAporte);

				NegocioMiembro negocio = await GetNegocioMiembro(item.CNegocio);
				item.SourceFoto = ImageSource.FromStream(() => new MemoryStream(negocio.DFoto));
				item.NombreEmpresa = negocio.DNombreComercial;
				item.VistaCalificacion = calificacion.DDescripcion;
				item.VistaCategoriaAporte = categoriaAporte.DCategoriaAporte;
				item.VistaEstado = estado.DEstado;
				item.VistaTipoAporte = tipoAporte.DTipoAporte;

				Aportes.Add(item);

			}


		}

		async Task<NegocioMiembro> GetNegocioMiembro(int idPersona)
		{

			var request = new HttpRequestMessage();
			request.RequestUri = new Uri("https://felino.vitalit.co.cr/api/api/NegocioMiembro/" + idPersona);
			request.Method = HttpMethod.Get;

			var client = new HttpClient();
			HttpResponseMessage response = await client.SendAsync(request);

			string content = await response.Content.ReadAsStringAsync();
			var resultado = JsonConvert.DeserializeObject<NegocioMiembro>(content);

			return resultado;

		}
		async Task<Calificacion> GetCalificacion(int id)
		{

			var request = new HttpRequestMessage();
			request.RequestUri = new Uri("https://felino.vitalit.co.cr/api/api/Calificacion/" + id);
			request.Method = HttpMethod.Get;

			var client = new HttpClient();
			HttpResponseMessage response = await client.SendAsync(request);

			string content = await response.Content.ReadAsStringAsync();
			var resultado = JsonConvert.DeserializeObject<Calificacion>(content);

			return resultado;

		}
		async Task<CategoriaAporte> GetCategoriaAporte(int id)
		{

			var request = new HttpRequestMessage();
			request.RequestUri = new Uri("https://felino.vitalit.co.cr/api/api/CategoriaAporte/" + id);
			request.Method = HttpMethod.Get;

			var client = new HttpClient();
			HttpResponseMessage response = await client.SendAsync(request);

			string content = await response.Content.ReadAsStringAsync();
			var resultado = JsonConvert.DeserializeObject<CategoriaAporte>(content);

			return resultado;

		}
		async Task<Estado> GetEstado(int id)
		{

			var request = new HttpRequestMessage();
			request.RequestUri = new Uri("https://felino.vitalit.co.cr/api/api/Estado/" + id);
			request.Method = HttpMethod.Get;

			var client = new HttpClient();
			HttpResponseMessage response = await client.SendAsync(request);

			string content = await response.Content.ReadAsStringAsync();
			var resultado = JsonConvert.DeserializeObject<Estado>(content);

			return resultado;

		}
		async Task<TipoAporte> GetTipoAporte(int id)
		{

			var request = new HttpRequestMessage();
			request.RequestUri = new Uri("https://felino.vitalit.co.cr/api/api/TipoAporte/" + id);
			request.Method = HttpMethod.Get;

			var client = new HttpClient();
			HttpResponseMessage response = await client.SendAsync(request);

			string content = await response.Content.ReadAsStringAsync();
			var resultado = JsonConvert.DeserializeObject<TipoAporte>(content);

			return resultado;

		}
		async Task<ObservableCollection<AportesPersona>> GetAportesPersona(int idPersona)
		{
			ObservableCollection<AportesPersona> aportesPersona = new ObservableCollection<AportesPersona>();
			var request = new HttpRequestMessage();
			request.RequestUri = new Uri("https://felino.vitalit.co.cr/api/api/AportesPersona");
			request.Method = HttpMethod.Get;

			var client = new HttpClient();
			HttpResponseMessage response = await client.SendAsync(request);

			string content = await response.Content.ReadAsStringAsync();
			var resultado = JsonConvert.DeserializeObject<ObservableCollection<AportesPersona>>(content);

			foreach (var item in resultado)
			{
				if (item.CPersona == idPersona)
				{
					aportesPersona.Add(item);
				}
			}

			return aportesPersona;

		}
		async Task<Aporte> GetAporte(int idAporte)
		{

			var request = new HttpRequestMessage();
			request.RequestUri = new Uri("https://felino.vitalit.co.cr/api/api/Aporte/" + idAporte);
			request.Method = HttpMethod.Get;

			var client = new HttpClient();
			HttpResponseMessage response = await client.SendAsync(request);

			string content = await response.Content.ReadAsStringAsync();
			var resultado = JsonConvert.DeserializeObject<Aporte>(content);

			return resultado;

		}

	}
}
