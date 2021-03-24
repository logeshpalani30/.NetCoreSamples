namespace UserDataFlow.Model.Address
{
    public class AddressReq
    {
        public int UserId { get; set; }
        public string? DoorNo { get; set; }
        public string? Street { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public string Nationality { get; set; }
        public string PinCode { get; set; }
    }

    public class AddressRes
    {
        public int AddressId { get; set; }
        public int UserId { get; set; }
        public string? DoorNo { get; set; }
        public string? Street { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public string Nationality { get; set; }
        public string PinCode { get; set; }
    }

}