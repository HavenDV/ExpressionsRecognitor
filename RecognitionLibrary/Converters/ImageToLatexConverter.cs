using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace RecognitionLibrary.Converters
{
    public static class ImageToLatexConverter
    {
        public static async Task<string> ConvertAsync(string path)
        {
            var cache = TryGetCache(path);
            if (!string.IsNullOrWhiteSpace(cache))
            {
                return cache;
            }

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

                SaveToCache(path, answer.Latex);

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


        #region Cache


        private static string GetCachePath()
        {
            return Path.Combine(Path.GetTempPath(), "recognition.txt");
        }

        private static Dictionary<string, string> GetCache()
        {
            var tempPath = GetCachePath();
            if (!File.Exists(tempPath))
            {
                return null;
            }

            var text = File.ReadAllText(tempPath);
            var dictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(text);

            return dictionary;
        }

        private static string TryGetCache(string path)
        {
            var dictionary = GetCache();
            if (dictionary == null)
            {
                return null;
            }

            return dictionary.ContainsKey(path) ? dictionary[path] : null;
        }

        private static void SaveToCache(string path, string value)
        {
            var dictionary = GetCache() ?? new Dictionary<string, string>();
            dictionary[path] = value;

            var tempPath = GetCachePath();
            var text = JsonConvert.SerializeObject(dictionary, Formatting.Indented);

            File.WriteAllText(tempPath, text);
        }

        #endregion
    }
}
