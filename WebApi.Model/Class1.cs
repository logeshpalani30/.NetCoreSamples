using System;

namespace WebApi.Model
{
    public class PrimeOrNotRequest
    {
        public string number { get; set; }
    }

    public class PrimeOrNotResponse:PrimeOrNotRequest
    {
        public string PrimeOrNot { get; set; }
    }
}
