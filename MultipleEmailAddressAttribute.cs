using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace System.ComponentModel.DataAnnotations
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public class MultipleEmailAddressAttribute : DataTypeAttribute
    {
        private static readonly EmailAddressAttribute _emailAddressAttribute = new EmailAddressAttribute();
        private char[] Separators;

        public MultipleEmailAddressAttribute(string separators = ",;") : base(DataType.EmailAddress)
        {
            this.Separators = separators.ToCharArray();
        }

        /// <summary>
        /// Checks if the value is valid
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool IsValid(object value)
        {
            if (value == null)
                return true;

            var emailAddress = Convert.ToString(value);

            if (string.IsNullOrWhiteSpace(emailAddress))
                return false;

            string[] emails = emailAddress.Split(Separators, StringSplitOptions.RemoveEmptyEntries);

            return emails.All(t => _emailAddressAttribute.IsValid(t));
        }
    }
}