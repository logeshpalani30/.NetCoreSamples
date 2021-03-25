namespace UserDataFlow.Model.Contact
{
    public class AddContact
    {
        public string Number { get; set; }
        public string NumberType { get; set; }
    }
    public class AddContactModel : AddContact
    {
        public int UserId { get; set; }
    }
    public class ContactNumberModel : AddContactModel
    {
        public int ContactId { get; set; }
    }

    public class ContactUpdate : AddContact
    {
        public int ContactId { get; set; }
    }
}