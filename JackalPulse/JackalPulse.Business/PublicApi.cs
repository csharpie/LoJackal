using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace JackalPulse.Business
{
    public class PublicApi
    {
        private readonly HttpClient _client;

        public PublicApi(string uri)
        {
            var handler = new HttpClientHandler();
            handler.UseDefaultCredentials = true;

            _client = new HttpClient(handler, true)
            {
                BaseAddress = new Uri(uri)
            };
        }

        public bool Save(string confirmOnlineRoute)
        {
            var response = _client.PostAsJsonAsync(confirmOnlineRoute + Environment.MachineName, new { }).Result;
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public double GetTimerInterval(string confirmTimerIntervalRoute)
        {
            var response = _client.GetStringAsync(confirmTimerIntervalRoute).Result;
            double interval = 0;
            Double.TryParse(response.ToString(), out interval);
            return interval;
        }
    }
}
