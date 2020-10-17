using FileOrganizer.CommonUtils;

namespace FileOrganizer.Domain
{
    public sealed class UploadDescription
    {
        public UploadDescription( string value )
        {
            Value = Guard.NotNull( value, nameof( value ) );
            // TODO: validation
        }

        //====== public properties

        public string Value { get; }

        //====== public static properties

        public static UploadDescription None { get; } = new UploadDescription( string.Empty );

        //====== override: Object

        public override string ToString() => Value.ToString();
    }
}
