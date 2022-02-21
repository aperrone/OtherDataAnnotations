using System;

namespace System.ComponentModel.DataAnnotations
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public class RequireNonDefaultAttribute : DataTypeAttribute
    {
        public RequireNonDefaultAttribute() : base(DataType.Custom)
        {
            this.ErrorMessage = "The {0} field requires a non-default value.";
        }

        public override bool IsValid(object value)
        {
            if (value is null)
                return true;
            var type = value.GetType();

            return !Equals(value, Activator.CreateInstance(Nullable.GetUnderlyingType(type) ?? type));
        }
    }
}
