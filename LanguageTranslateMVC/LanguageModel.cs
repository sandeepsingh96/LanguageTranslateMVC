using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace LanguageTranslateMVC
{
    public class LanguageTranslatorModel:ILanguageTranslatorModel
    {
        public bool ListCheck(string inputLang)
        {
            LanguageNames languagesList = new LanguageNames();
            if (languagesList.fulllanguageNames.ContainsKey(inputLang))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task FetchLanguageListAsync()
        {
            LanguageNames languagesList = new LanguageNames();
            Console.WriteLine("Language List with Code: ");
            var headers = new Dictionary<string, string>
            {
                { "X-RapidAPI-Key", "bb6cfa34dbmshcd6eda765266f8ep173bc4jsn9e1cfdca9bf4"},
                { "X-RapidAPI-Host", "google-translate1.p.rapidapi.com" },
            };
            using (var client = new HttpClient(new HttpClientHandler { UseCookies = false, }))
            using (var request = new HttpRequestMessage(HttpMethod.Get, "https://google-translate1.p.rapidapi.com/language/translate/v2/languages"))
            {
                
                foreach (var header in headers)
                {
                    request.Headers.Add(header.Key, header.Value);
                }

                using (var response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead))
                {
                    response.EnsureSuccessStatusCode();
                    var body = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<dynamic>(body);
                    var detections = result.data.languages;

                    foreach (var lang in detections)
                    {
                        string valueL = lang.language;
                        string detectedLanguageName = languagesList.fulllanguageNames.ContainsKey(valueL) ? languagesList.fulllanguageNames[valueL] : "Unknown";
                        Console.WriteLine($"{detectedLanguageName}-> {valueL}");
                    }
                }
            }
        }
        public async Task DetectLanguageAsync(string inputText)
        {
            LanguageNames languagesList = new LanguageNames();
            var client = new HttpClient();

           

            // Return early if input is empty
            if (string.IsNullOrEmpty(inputText))
            {
                Console.WriteLine("Input text is empty.");
                return;
            }

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://google-translate1.p.rapidapi.com/language/translate/v2/detect"),
                Headers =
        {
            { "X-RapidAPI-Key", "bb6cfa34dbmshcd6eda765266f8ep173bc4jsn9e1cfdca9bf4"},
            { "X-RapidAPI-Host", "google-translate1.p.rapidapi.com" },
        },
                Content = new FormUrlEncodedContent(new Dictionary<string, string>
        {
            { "q", inputText },
        }),
            };

            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<dynamic>(body);
                var detections = result.data.detections;

                // Return early if input text could not be detected
                if (detections == null || detections.Count == 0 || detections[0].Count == 0)
                {
                    Console.WriteLine("Could not detect language of input text.");
                    return;
                }

                string detectedLanguageCode = detections[0][0].language;
                string detectedLanguageName = languagesList.fulllanguageNames.ContainsKey(detectedLanguageCode) ? languagesList.fulllanguageNames[detectedLanguageCode] : "Unknown";
                Console.WriteLine("Detected language: {0}", detectedLanguageName);
            }
        }


        public async Task TranslateLanguageAsync(string inputText, string targetLang, string sourceLang)
        {
          
           

            // Create HTTP client and request
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://google-translate1.p.rapidapi.com/language/translate/v2"),
                Headers =
        {
            { "X-RapidAPI-Key", "bb6cfa34dbmshcd6eda765266f8ep173bc4jsn9e1cfdca9bf4" },
            { "X-RapidAPI-Host", "google-translate1.p.rapidapi.com" },
        },
                Content = new FormUrlEncodedContent(new Dictionary<string, string>
        {
            { "q", inputText },
            { "target", targetLang },
            { "source", sourceLang },
        }),
            };

            try
            {
                // Send HTTP request and deserialize response
                using (var response = await client.SendAsync(request))
                {
                    response.EnsureSuccessStatusCode();
                    var body = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<dynamic>(body);
                    var translations = result.data.translations;

                    // Extract the translated text from the response
                    var translatedText = ((JArray)translations)[0]["translatedText"].ToString();
                    Console.WriteLine(translatedText);
                }
            }
            catch (HttpRequestException ex)
            {
                // Handle HTTP request error
                Console.WriteLine($"HTTP request error: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Handle other errors
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

    }
}
