using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AMA.AdminClient.Properties;
using Flurl.Http;

namespace AMA.AdminClient
{
    public class ApiService
    {
        /// <summary>
        /// Full route to the resource (controller/action/etc)
        /// </summary>
        public string Route { get; }
        public string ApiUrl = $"{Resources.ApiUrl}";

        public static string Token { get; set; }
        public static int UserId { get; set; }
        public static string Permission { get; set; }

        public ApiService(string route)
        {
            Route = route;
        }

        public async Task<T> Get<T>(object searchRequest = null, string action = null)
        {
            var query = string.Empty;
            if (searchRequest != null)
            {
                query = await searchRequest?.ToQueryString();
            }

            string url = $"{ApiUrl}/{Route}";

            if (!string.IsNullOrEmpty(action))
            {
                url += $"/{action}";
            }

            var list = await $"{url}?{query}"
                .WithHeader("Accept", "application/json")
                .WithOAuthBearerToken(Token)
                .GetJsonAsync<T>();

            return list;
        }

        public async Task<T> GetById<T>(object id)
        {
            var url = $"{ApiUrl}/{Route}/{id}";

            return await url.WithOAuthBearerToken(Token).GetJsonAsync<T>();
        }

        public async Task<T> Post<T>(object data = null, string action = null)
        {
            var url = $"{ApiUrl}/{Route}";

            if (!string.IsNullOrEmpty(action))
            {
                url += $"/{action}";
            }

            try
            {
                return await url
                    .WithHeader("Accept", "application/json")
                    .WithOAuthBearerToken(Token)
                    .PostJsonAsync(data)
                    .ReceiveJson<T>();
            }
            catch (FlurlHttpException ex)
            {
                if (action != "login")
                {
                    var stringBuilder = new StringBuilder(ex.Message);
                    MessageBox.Show(stringBuilder.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return default(T);
            }
        }

        public async Task<T> Update<T>(int id, object request)
        {
            try
            {
                var url = $"{ApiUrl}/{Route}/{id}";

                return await url
                    .WithHeader("Accept", "application/json")
                    .WithOAuthBearerToken(Token)
                    .PutJsonAsync(request)
                    .ReceiveJson<T>();
            }
            catch (FlurlHttpException ex)
            {
                var stringBuilder = new StringBuilder(ex.Message);
                MessageBox.Show(stringBuilder.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return default(T);
            }
        }

        public async Task<T> Delete<T>(object data = null, string action = null)
        {
            try
            {
                var query = string.Empty;
                if (data != null)
                {
                    query = await data?.ToQueryString();
                }

                var url = $"{ApiUrl}/{Route}";

                if (!string.IsNullOrEmpty(action))
                {
                    url += $"/{action}";
                }

                return await $"{url}?{query}"
                    .WithHeader("Accept", "application/json")
                    .WithOAuthBearerToken(Token)
                    .DeleteAsync()
                    .ReceiveJson<T>();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return default;
            }
        }

        public async Task<T> Delete<T>(int id)
        {
            try
            {
                var url = $"{ApiUrl}/{Route}/{id}";

                return await url
                    .WithHeader("Accept", "application/json")
                    .WithOAuthBearerToken(Token)
                    .DeleteAsync()
                    .ReceiveJson<T>();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return default;
            }
        }

        public async Task GetExcelFile(string filename, string action = null)
        {
            var query = string.Empty;

            string url = $"{ApiUrl}/{Route}";

            if (!string.IsNullOrEmpty(action))
            {
                url += $"/{action}";
            }

            var response = await $"{url}?{query}"
                .WithHeader("Accept", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                .WithOAuthBearerToken(Token)
                .GetBytesAsync();


            string file = $@"C:\Reports\{filename}";
            string path = Path.GetDirectoryName(file);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            if (!File.Exists(file))
            {
                using (var fs = new FileStream(file, FileMode.Create, FileAccess.Write))
                {
                    await fs.WriteAsync(response, 0, response.Length);
                    MessageBox.Show($"Excel report generate at {file}");
                }
            }
        }
    }
}