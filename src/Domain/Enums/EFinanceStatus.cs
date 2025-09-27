using System.ComponentModel;

namespace Domain.Enums;

public enum EFinanceStatus
{
    [Description("Pendente")]
    Pendente = 1,
    [Description("Pago")]
    Pago = 2
}