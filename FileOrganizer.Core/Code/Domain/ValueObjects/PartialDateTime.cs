using System;

namespace FileOrganizer.Core
{
    public sealed class PartialDateTime : IValueObject
    {
        public PartialDateTime( int? year, int? month, int? day, int? hour, int? minute )
        {
            // TODO: add guard method in common utils

            if (month < 0 || month > 11)
                throw new ArgumentOutOfRangeException( nameof( month ), month, "Value must be in range [0..11]." );

            if (day < 0 || day > 30) // TODO: additional ckecking if year or month is present
                throw new ArgumentOutOfRangeException( nameof( day ), day, "Value must be in range [0..30]." );

            if (hour < 0 || hour > 23)
                throw new ArgumentOutOfRangeException( nameof( hour ), hour, "Value must be in range [0..23]." );

            if (minute < 0 || minute > 59)
                throw new ArgumentOutOfRangeException( nameof( minute ), minute, "Value must be in range [0..59]." );

            Year   = year;
            Month  = month;
            Day    = day;
            Hour   = hour;
            Minute = minute;
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

            return $"{year}.{month}.{day} {hour}:{minute}";
        }
    }
}
