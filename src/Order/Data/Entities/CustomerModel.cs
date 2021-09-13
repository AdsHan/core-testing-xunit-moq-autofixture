using Order.Data.DomainObjects;

namespace Order.Data.Entities
{
    public class CustomerModel : BaseEntity, IAggregateRoot
    {
        public CustomerModel(string name, string phone, string email, string cep, string state, string city)
        {
            Name = name;
            Phone = phone;
            Email = email;
            CEP = cep;
            State = state;
            City = city;
        }

        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string CEP { get; set; }
        public string State { get; set; }
        public string City { get; set; }
    }
}