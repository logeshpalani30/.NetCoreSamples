namespace UserDataFlow.Model.Contact
{
    public class ContactNumberModel
    {
        public int UserId { get; set; }
        public int ContactId { get; set; }
        public  string NumberType { get; set; }
        public string Number { get; set; }
    }
}