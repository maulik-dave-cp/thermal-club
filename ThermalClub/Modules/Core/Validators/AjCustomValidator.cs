using FluentValidation;

namespace ThermalClub.Modules.Core.Validators
{
    public static class AjCustomValidator
    {
        public static IRuleBuilderOptions<T, TProperty> AjNotNull<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new AjNotNullValidator());
        }
    }
}