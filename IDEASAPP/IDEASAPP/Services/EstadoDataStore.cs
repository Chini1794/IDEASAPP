using IDEASAPP.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace IDEASAPP.Services
{
	public class EstadoDataStore : IDataStore<Estado>
	{
		Task<bool> IDataStore<Estado>.AddItemAsync(Estado item)
		{
			throw new NotImplementedException();
		}

		Task<bool> IDataStore<Estado>.DeleteItemAsync(string id)
		{
			throw new NotImplementedException();
		}

		async Task<Estado> IDataStore<Estado>.GetItemAsync(int id)
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

		Task<IEnumerable<Estado>> IDataStore<Estado>.GetItemsAsync()
		{
			throw new NotImplementedException();
		}

		Task<bool> IDataStore<Estado>.UpdateItemAsync(Estado item)
		{
			throw new NotImplementedException();
		}
	}
}
