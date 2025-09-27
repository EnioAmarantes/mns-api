using System.ComponentModel;

namespace Domain.Enums;
public enum EFinanceType
{
    [Description("Pagar")]
    Pagar = 1,
    [Description("Receber")]
    Receber = 2
}