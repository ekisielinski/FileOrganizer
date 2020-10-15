using FileOrganizer.WebUI.Models;
using FluentValidation;
using System;

namespace FileOrganizer.WebUI.Validators
{
    public sealed class PartialDateTimeModelValidator : AbstractValidator<PartialDateTimeModel>
    {
        public PartialDateTimeModelValidator()
        {
            RuleFor( x => x.Year )
                .InclusiveBetween( -5000, 5000 );

            RuleFor( x => x.Month )
                .InclusiveBetween( 1, 12 );

            RuleFor( x => x.Day )
                .InclusiveBetween( 1, 31 );

            // TODO: read FV docs
            //RuleFor( x => x )
            //    .Must( model => model.Day >= 1 && model.Day <= DateTime.DaysInMonth( model.Year!.Value, model.Month!.Value ) )
            //    .When( model => model.Year.HasValue && model.Month.HasValue );

            RuleFor( x => x.Hour )
                .InclusiveBetween( 0, 23 );

            RuleFor( x => x.Minute )
                .InclusiveBetween( 0, 59 );
        }
    }
}
