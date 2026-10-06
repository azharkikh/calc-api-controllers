using System.ComponentModel.DataAnnotations;

namespace CalcApiControllers.Validation;

public class NotZeroAttribute : ValidationAttribute
{
    public NotZeroAttribute() : base("Поле {0} не должно быть равно нулю.")
    {
    }

    public override bool IsValid(object? value)
    {
        return value is null || (value is double number && number != 0);
    }
}
