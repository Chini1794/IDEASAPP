using IDEASAPP.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace IDEASAPP.Services
{
	public class AportesPersonaDataStore : IDataStore<AportesPersona>
	{
		async Task<bool> IDataStore<AportesPersona>.AddItemAsync(AportesPersona item)
		{
			Uri requestUri = new Uri("https://felino.vitalit.co.cr/api/api/AportesPersona");
			var client = new HttpClient();
			var json = JsonConvert.SerializeObject(item);
			var contentJson = new StringContent(json, Encoding.UTF8, "application/json");

			var response = await client.PostAsync(requestUri, contentJson);

			return response.StatusCode == HttpStatusCode.OK;
		}

		Task<bool> IDataStore<AportesPersona>.DeleteItemAsync(string id)
		{
			throw new NotImplementedException();
		}

		Task<AportesPersona> IDataStore<AportesPersona>.GetItemAsync(int id)
		{
			throw new NotImplementedException();
		}

		async Task<IEnumerable<AportesPersona>> IDataStore<AportesPersona>.GetItemsAsync()
		{
			var request = new HttpRequestMessage();
			request.RequestUri = new Uri("https://felino.vitalit.co.cr/api/api/AportesPersona");
			request.Method = HttpMethod.Get;
			var client = new HttpClient();
			HttpResponseMessage response = await client.SendAsync(request);

			string content = await response.Content.ReadAsStringAsync();
			var resultado = JsonConvert.DeserializeObject<IEnumerable<AportesPersona>>(content);

			return await Task.FromResult(resultado);
		}

		Task<bool> IDataStore<AportesPersona>.UpdateItemAsync(AportesPersona item)
		{
			throw new NotImplementedException();
		}
	}
}
