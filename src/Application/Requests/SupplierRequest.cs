using System.Text.Json.Serialization;

namespace Application.Requests;

public record SupplierRequest{
    public string Name { get; set; }
    [JsonPropertyName("cnpj")]
    public string CNPJ { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }
}