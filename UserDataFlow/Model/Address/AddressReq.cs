namespace UserDataFlow.Model.Address
{
    public class Address
    {
        public string? DoorNo { get; set; }
        public string? Street { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public string Nationality { get; set; }
        public string PinCode { get; set; }
    }
    public class AddressReq : Address
    {
        public int UserId { get; set; }
    }

    public class AddressRes : AddressReq
    {
        public int AddressId { get; set; }
    }

    public class AddressUpdate : Address
    {
        public int AddressId { get; set; }
    }
}