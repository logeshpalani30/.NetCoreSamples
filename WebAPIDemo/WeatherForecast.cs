using System;

namespace WebAPIDemo
{
    public class WeatherForecast
    {
        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public string Summary { get; set; }
    }

    public class CheckPrimeOrNotRepository: ICheckPrimeOrNot
    {
        private int totalValue;

        public string CheckPrimeOrNot(string givenText)
        {
            totalValue = 0;
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

            for (i = 2; i < totalValue; i++)
                if (totalValue % i == 0)
                    return "Not Prime Number";

            if (totalValue == i)
                return "Prime Number";

            return "Not Prime Number";
        }
    }

    public interface ICheckPrimeOrNot
    {
        string CheckPrimeOrNot(string number);
    }
}
