using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApi.Model;

namespace UITask.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IHttpClientFactory _clientFactory;
        public string givenText;
        private int totalValue=0;

        public static string Message { get; set; }

        public IndexModel(ILogger<IndexModel> logger,IHttpClientFactory clientFactory)
        {
            _logger = logger;
            _clientFactory = clientFactory;
        }

        public async Task OnGet(string givenText)
        {
            this.givenText = givenText;

            // Message = AddValues();
            // totalValue = 0;

            // var request = new HttpRequestMessage(HttpMethod.Post, "link");
            // request.Headers.Add("Accept", "application/vnd.github.v3+json");
            // request.Headers.Add("User-Agent", "Task-Test-Agent");
            
            if (this.givenText != null)
            {
                var numberContent = new PrimeOrNotRequest()
                {
                    number = this.givenText
                };

                var content = new StringContent(JsonSerializer.Serialize(numberContent),
                    Encoding.UTF8,
                    "application/json");

                var client = _clientFactory.CreateClient("test");

                var jsonResponseMessage = await client.PostAsync("weatherforecast/checkPrime", content);
                var primeOrNotResponse = await jsonResponseMessage.Content.ReadAsStreamAsync();
                var response = await JsonSerializer.DeserializeAsync<PrimeOrNotResponse>(primeOrNotResponse);

                Message = response?.PrimeOrNot;
            }
        }

        // private string AddValues()
        // {
        //     if (givenText == null)
        //         return "Enter Some Numbers";
        //
        //     foreach (var number in givenText)
        //     {
        //         if (47 < number && number < 58)
        //             totalValue += Convert.ToInt32(char.GetNumericValue(number));
        //         else
        //             return "Enter numbers only";
        //     }
        //     return PrimeOrNot(totalValue);
        // }
        //
        // private string PrimeOrNot(int totalValue)
        // {
        //     int i;
        //
        //     for (i=2; i < totalValue; i++)
        //         if (totalValue % i == 0)
        //             return "Not Prime Number";
        //
        //     if (totalValue == i)
        //         return "Prime Number";
        //
        //     return "Not Prime Number";
        // }
    }
}
