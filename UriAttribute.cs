namespace System.ComponentModel.DataAnnotations
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public class UriAttribute : DataTypeAttribute
    {
        private readonly UriKind kind;

        public UriAttribute(UriKind kind = UriKind.RelativeOrAbsolute) : base(DataType.Url)
        {
            this.kind = kind;
        }

        /// <summary>
        /// Checks if the value is valid
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool IsValid(object value)
        {
            var url = Convert.ToString(value);

            return Uri.IsWellFormedUriString(url, kind);
        }
    }
}