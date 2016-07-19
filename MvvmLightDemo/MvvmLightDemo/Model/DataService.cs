using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace MvvmLightDemo.Model
{
    public class DataService : IDataService
    {
        //public void GetData(Action<DataItem, Exception> callback)
        //{
        //    // Use this to connect to the actual data service
        //    var item = new DataItem("Welcome to MVVM Light");
        //    callback(item, null);
        //}

        private const string ServiceUrl = "http://www.galasoft.ch/labs/json/JsonDemo.ashx";
        private readonly HttpClient _client;
        public DataService()
        {
            _client = new HttpClient();
        }
        public async Task<IList<Friend>> GetFriends()
        {
            return new List<Friend>();
            //var request = new HttpRequestMessage(HttpMethod.Post, new Uri(ServiceUrl));
            //var response = await _client.SendAsync(request);
            //var result = await response.Content.ReadAsStringAsync();
            //var serializer = new JsonSerializer();
            //var deserialized = serializer.Deserialize<FacebookResult>(result);
            //return deserialized.Friends;
        }
    }
}