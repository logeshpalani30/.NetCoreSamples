using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;

namespace UITask.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        public string givenText;
        private int totalValue=0;

        public static string Message { get; set; }

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet(string givenText)
        {
            this.givenText = givenText;

            Message = AddValues();
            totalValue = 0;
        }

        private string AddValues()
        {
            if (givenText == null)
                return "Enter Some Numbers";

            foreach (var number in givenText)
            {
                if (47 < number && number < 58)
                    totalValue += Convert.ToInt32(char.GetNumericValue(number));
                else
                    return "Enter numbers only";
            }
            return PrimeOrNot(totalValue);
        }

        private string PrimeOrNot(int totalValue)
        {
            int i;

            for (i=2; i < totalValue; i++)
                if (totalValue % i == 0)
                    return "Not Prime Number";

            if (totalValue == i)
                return "Prime Number";

            return "Not Prime Number";
        }
    }
}
