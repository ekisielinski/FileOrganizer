using FluentValidation;

namespace FileOrganizer.WebUI.Pages
{
    public sealed class CreateUserFormValidator : AbstractValidator<CreateUserForm>
    {
        public CreateUserFormValidator()
        {
            RuleFor( x => x.UserName )
                .NotEmpty()
                .Length( 3, 50 )
                .Matches( "^[a-z0-9_]*$" ).WithMessage( "You can use only these characters: a..z 0..9 and _ (underscore)." );

            RuleFor( x => x.UserDisplayName )
                .Length( 0, 80 );

            RuleFor( x => x.Password )
                .NotEmpty()
                .Length( 8, 100 );
        }
    }
}
