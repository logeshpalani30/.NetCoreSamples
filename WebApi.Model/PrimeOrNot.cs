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

    public class SendSmsRequest
    {
        public string To { get; set; }
        public string Body { get; set; }
    }
}
