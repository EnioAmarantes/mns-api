using System.ComponentModel;

namespace Domain.Enums;

public enum EStockMoveType
{
    [Description("Entrada")]
    IN = 1,
    [Description("Saída")]
    OUT = 2
}