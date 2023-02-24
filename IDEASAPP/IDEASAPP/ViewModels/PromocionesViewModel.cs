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
    public class PromocionesViewModel : BaseViewModel
	{
		public ObservableCollection<Promocion> Promociones { get; } 
		public Command LoadPromocionesCommand { get; }
		public Command LinkCommand { get; }
		public PromocionesViewModel()
        {
			Promociones = new ObservableCollection<Promocion>();
			LinkCommand = new Command(OnLinkTapped);
			LoadPromocionesCommand = new Command(async () => await LoadPromociones());
		}
		async Task LoadPromociones()
		{

			ObservableCollection<NegocioMiembro> aux = new ObservableCollection<NegocioMiembro>();
			var request = new HttpRequestMessage();
			request.RequestUri = new Uri("https://felino.vitalit.co.cr/api/api/Promocion");
			request.Method = HttpMethod.Get;

			var client = new HttpClient();
			HttpResponseMessage response = await client.SendAsync(request);
			Promociones.Clear();
			string content = await response.Content.ReadAsStringAsync();
			var resultado = JsonConvert.DeserializeObject<ObservableCollection<Promocion>>(content).OrderByDescending(x => x.DCreacion);

			foreach (var item in resultado)
			{
				if (item.DEstadoActiva)
				{
					NegocioMiembro negocio = await GetNegocioMiembro(item.CNegocio);
					item.SourceFoto = ImageSource.FromStream(() => new MemoryStream(item.DFoto));
					item.NombreEmpresa = negocio.DNombreComercial;
					
					Promociones.Add(item);
				}
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
