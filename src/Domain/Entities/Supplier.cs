namespace Domain.Entities;

public class Supplier
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string CNPJ { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }
    public Guid CompanyId { get; set; }
    public Company Company { get; set; }

    public Supplier(Guid companyId, string name, string CNPJ, string email, string phone, string address)
    {
        Id = Guid.NewGuid();
        CompanyId = companyId;
        Name = name;
        this.CNPJ = CNPJ;
        Email = email;
        Phone = phone;
        Address = address;
    }
}