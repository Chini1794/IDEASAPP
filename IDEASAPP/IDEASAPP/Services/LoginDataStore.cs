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
	public class LoginDataStore : IDataStore<Login>
	{
		async Task<bool> IDataStore<Login>.AddItemAsync(Login item)
		{
			Uri requestUri = new Uri("https://felino.vitalit.co.cr/api/api/Login");
			var client = new HttpClient();
			var json = JsonConvert.SerializeObject(item);
			var contentJson = new StringContent(json, Encoding.UTF8, "application/json");

			var response = await client.PostAsync(requestUri, contentJson);

			return response.StatusCode == HttpStatusCode.OK;


		}

		Task<bool> IDataStore<Login>.DeleteItemAsync(string id)
		{
			throw new NotImplementedException();
		}

		Task<Login> IDataStore<Login>.GetItemAsync(int id)
		{
			throw new NotImplementedException();
		}

		Task<IEnumerable<Login>> IDataStore<Login>.GetItemsAsync()
		{
			throw new NotImplementedException();
		}

		Task<bool> IDataStore<Login>.UpdateItemAsync(Login item)
		{
			throw new NotImplementedException();
		}
	}
}
