using IDEASAPP.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace IDEASAPP.Services
{
	public class CalificacionDataStore : IDataStore<Calificacion>
	{
		Task<bool> IDataStore<Calificacion>.AddItemAsync(Calificacion item)
		{
			throw new NotImplementedException();
		}

		Task<bool> IDataStore<Calificacion>.DeleteItemAsync(string id)
		{
			throw new NotImplementedException();
		}

		async Task<Calificacion> IDataStore<Calificacion>.GetItemAsync(int id)
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

		Task<IEnumerable<Calificacion>> IDataStore<Calificacion>.GetItemsAsync()
		{
			throw new NotImplementedException();
		}

		Task<bool> IDataStore<Calificacion>.UpdateItemAsync(Calificacion item)
		{
			throw new NotImplementedException();
		}
	}
}
