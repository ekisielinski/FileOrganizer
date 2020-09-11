namespace FileOrganizer.Core
{
    public sealed class UploadDescription : IValueObject
    {
        public UploadDescription( string value )
        {
            Value = value;
        }

        //====== public properties

        public string Value { get; }

        //====== public static properties

        public static UploadDescription None { get; } = new UploadDescription( string.Empty );

        //====== override: Object

        public override string ToString() => Value.ToString();
    }
}
