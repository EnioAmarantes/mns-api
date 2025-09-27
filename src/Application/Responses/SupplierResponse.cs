namespace Application.Responses;

public record SupplierResponse{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string CNPJ { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }
}