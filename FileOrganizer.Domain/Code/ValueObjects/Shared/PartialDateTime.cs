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

        //====== override: Object

        public override string ToString()
        {
            var year   = Year?  .ToString() ?? "????";
            var month  = Month? .ToString() ?? "??";
            var day    = Day?   .ToString() ?? "??";
            var hour   = Hour?  .ToString() ?? "??";
            var minute = Minute?.ToString() ?? "??";

            return $"{year}.{month}.{day} {hour}:{minute}"; // TODO: format to 2 digits
        }
    }
}
