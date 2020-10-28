using FileOrganizer.CommonUtils;

namespace FileOrganizer.Domain
{
    public sealed class PartialDateTime
    {
        public PartialDateTime( int? year, int? month, int? day, int? hour, int? minute )
        {
            Year = year;

            Month  = Guard.InRangeNullable( month,  1, 12, nameof( month  ) );
            Day    = Guard.InRangeNullable( day,    1, 31, nameof( day    ) ); // TODO: additional ckecking if year or month is present
            Hour   = Guard.InRangeNullable( hour,   0, 23, nameof( hour   ) );
            Minute = Guard.InRangeNullable( minute, 0, 59, nameof( minute ) );
        }

        //====== public static properties

        public static PartialDateTime Empty { get; } = new PartialDateTime( null, null, null, null, null );

        //====== public properties

        public int? Year   { get; }
        public int? Month  { get; }
        public int? Day    { get; }
        public int? Hour   { get; }
        public int? Minute { get; }

        //====== public methods

        public string ToSpecialString()
        {
            var year   = Year?  .ToString( "0000" ) ?? "????";
            var month  = Month? .ToString( "00" ) ?? "??";
            var day    = Day?   .ToString( "00" ) ?? "??";
            var hour   = Hour?  .ToString( "00" ) ?? "??";
            var minute = Minute?.ToString( "00" ) ?? "??";

            return $"{year}.{month}.{day} {hour}:{minute}";
        }

        //====== public static methods

        public static PartialDateTime FromSpecialString( string value )
        {
            Guard.NotNull( value, nameof( value ) );

            // todo: throw if invalid format

            string[] segments = value.Split( '.', ' ', ':' );

            return new PartialDateTime(
                MiscUtils.TryParse( segments[0] ),
                MiscUtils.TryParse( segments[1] ),
                MiscUtils.TryParse( segments[2] ),
                MiscUtils.TryParse( segments[3] ),
                MiscUtils.TryParse( segments[4] ) );
        }

        //====== override: Object

        public override string ToString() => ToSpecialString();
    }
}
