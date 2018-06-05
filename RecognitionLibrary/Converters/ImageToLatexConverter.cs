using System;
using System.Drawing;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace RecognitionLibrary.Converters
{
    public static class ImageToLatexConverter
    {
        public static async Task<string> ConvertAsync(string path)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("app_id", "havendv_gmail_com");
                client.DefaultRequestHeaders.Add("app_key", "d58ccd58d9e5166a2394");

                var content = new StringContent($"{{ \"src\": \"data:image/jpeg;base64,{ToBase64(path)}\" }}");
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                var response = await client.PostAsync("https://api.mathpix.com/v3/latex", content);
                var text = await response.Content.ReadAsStringAsync();
                var answer = Answer.FromJson(text);

                if (string.IsNullOrWhiteSpace(answer.Latex))
                {
                    throw new Exception(answer.Error);
                }

                return answer.Latex;
            }
        }

        private static string ToBase64(string path)
        {
            using (var image = Image.FromFile(path))
            {
                using (var stream = new MemoryStream())
                {
                    image.Save(stream, image.RawFormat);
                    var bytes = stream.ToArray();

                    return Convert.ToBase64String(bytes);
                }
            }
        }
    }
}
