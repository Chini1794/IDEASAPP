using IDEASAPP.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace IDEASAPP.Services
{
	public class PromocionDataStore : IDataStore<Promocion>
	{
		Task<bool> IDataStore<Promocion>.AddItemAsync(Promocion item)
		{
			throw new NotImplementedException();
		}

		Task<bool> IDataStore<Promocion>.DeleteItemAsync(string id)
		{
			throw new NotImplementedException();
		}

		Task<Promocion> IDataStore<Promocion>.GetItemAsync(int id)
		{
			throw new NotImplementedException();
		}

		async Task<IEnumerable<Promocion>> IDataStore<Promocion>.GetItemsAsync()
		{
			var request = new HttpRequestMessage();
			request.RequestUri = new Uri("https://felino.vitalit.co.cr/api/api/Promocion");
			request.Method = HttpMethod.Get;

			var client = new HttpClient();
			HttpResponseMessage response = await client.SendAsync(request);

			string content = await response.Content.ReadAsStringAsync();
			var resultado = JsonConvert.DeserializeObject<ObservableCollection<Promocion>>(content);

			return await Task.FromResult(resultado);
		}

		Task<bool> IDataStore<Promocion>.UpdateItemAsync(Promocion item)
		{
			throw new NotImplementedException();
		}
	}
}
