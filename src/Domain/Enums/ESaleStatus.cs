using System.ComponentModel;

namespace Domain.Enums;

public enum ESaleStatus
{
    [Description("Pendente")]
    Pending = 1,
    [Description("Pago")]
    Paid = 2,
}