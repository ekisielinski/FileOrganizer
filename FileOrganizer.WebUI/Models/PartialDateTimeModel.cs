using FileOrganizer.CommonUtils;
using FileOrganizer.Core;

namespace FileOrganizer.WebUI.Models
{
    public sealed class PartialDateTimeModel
    {
        public PartialDateTimeModel() { } // for binding

        public PartialDateTimeModel ( PartialDateTime dateTime )
        {
            Guard.NotNull( dateTime, nameof( dateTime ) );

            Year   = dateTime.Year;
            Month  = dateTime.Month;
            Day    = dateTime.Day;
            Hour   = dateTime.Hour;
            Minute = dateTime.Minute;
        }

        //====== public properties

        public int? Year   { get; set; }
        public int? Month  { get; set; }
        public int? Day    { get; set; }
        public int? Hour   { get; set; }
        public int? Minute { get; set; }

        //====== public methods

        public PartialDateTime ToPartialDateTime()
            => new PartialDateTime( Year, Month, Day, Hour, Minute );
    }
}
