using FluentValidation;
using System;

namespace FileOrganizer.WebUI.Pages
{
    public sealed class CreateLinkFormValidator : AbstractValidator<CreateLinkForm>
    {
        public CreateLinkFormValidator()
        {
            RuleFor( x => x.Url )
                .NotEmpty()
                .Must( url => Uri.TryCreate( url, UriKind.Absolute, out _ ) ).WithMessage( "Invalud URL!" );

            RuleFor( x => x.Title )
                .NotEmpty();

            RuleFor( x => x.Comment )
                .Length( 0, 4000 ); // todo: correct lengths
        }
    }
}
