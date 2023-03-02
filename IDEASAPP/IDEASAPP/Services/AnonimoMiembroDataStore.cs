using IDEASAPP.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace IDEASAPP.Services
{
	public class AnonimoMiembroDataStore : IDataStore<AnonimoMiembro>
	{
		async Task<bool> IDataStore<AnonimoMiembro>.AddItemAsync(AnonimoMiembro item)
		{
			Uri requestUri = new Uri("https://felino.vitalit.co.cr/api/api/AnonimoMiembro");
			var client = new HttpClient();
			var json = JsonConvert.SerializeObject(item);
			var contentJson = new StringContent(json, Encoding.UTF8, "application/json");

			var response = await client.PostAsync(requestUri, contentJson);

			return response.StatusCode == HttpStatusCode.OK;
		}

		Task<bool> IDataStore<AnonimoMiembro>.DeleteItemAsync(string id)
		{
			throw new NotImplementedException();
		}

		Task<AnonimoMiembro> IDataStore<AnonimoMiembro>.GetItemAsync(int id)
		{
			throw new NotImplementedException();
		}

		async Task<IEnumerable<AnonimoMiembro>> IDataStore<AnonimoMiembro>.GetItemsAsync()
		{
			var request = new HttpRequestMessage();
			request.RequestUri = new Uri("https://felino.vitalit.co.cr/api/api/AnonimoMiembro");
			request.Method = HttpMethod.Get;
			var client = new HttpClient();
			HttpResponseMessage response = await client.SendAsync(request);

			string content = await response.Content.ReadAsStringAsync();
			var resultado = JsonConvert.DeserializeObject<IEnumerable<AnonimoMiembro>>(content);

			return await Task.FromResult(resultado);
		}

		Task<bool> IDataStore<AnonimoMiembro>.UpdateItemAsync(AnonimoMiembro item)
		{
			throw new NotImplementedException();
		}
	}
}
