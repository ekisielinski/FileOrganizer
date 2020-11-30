using FluentValidation;

namespace FileOrganizer.WebUI.Pages.CreateTag
{
    public sealed class CreateTagFormValidator : AbstractValidator<CreateTagForm>
    {
        public CreateTagFormValidator()
        {
            RuleFor( x => x.Name )
                .NotEmpty()
                .Length( 3, 50 )
                // TODO: allow more letters, e.g. ąśćź
                .Matches( "^[a-z0-9_]*$" ).WithMessage( "You can use only these characters: a..z 0..9 and _ (underscore)." );

            RuleFor( x => x.DisplayName )
                .MaximumLength( 80 );

            RuleFor( x => x.Description )
                .MaximumLength( 1000 );
        }
    }
}
