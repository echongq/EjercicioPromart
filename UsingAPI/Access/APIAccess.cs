using UsingAPI.Entity;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace UsingAPI.Access
{
    public static class APIAccess
    {
        public static async Task<result> getApi(string url)
        {
            var geto = new result();
            using (var http = new HttpClient())
            {
                var response = await http.GetStringAsync(url);
                geto = JsonConvert.DeserializeObject<result>(response);
            }
            return geto;
        }
        public static async Task<object> postApi(string url, Employee emp)
        {
            using (var http = new HttpClient())
            {
                var stringContent = new StringContent(JsonConvert.SerializeObject(emp), Encoding.UTF8, "application/json");

                var response = await http.PostAsync(url, stringContent);
                var geto = response.StatusCode.ToString();
                return geto;

            }
        }
    }
}
