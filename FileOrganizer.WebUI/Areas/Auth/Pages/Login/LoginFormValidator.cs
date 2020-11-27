using FluentValidation;

namespace FileOrganizer.WebUI.Areas.Auth.Pages.Login
{
    public sealed class LoginFormValidator : AbstractValidator<LoginForm>
    {
        public LoginFormValidator()
        {
            RuleFor( x => x.UserName )
                .NotEmpty()
                .Length( 3, 50 )
                .Matches( "^[a-z0-9_]*$" ).WithMessage( "You can use only these characters: a..z 0..9 and _ (underscore)." );

            RuleFor( x => x.Password )
                .NotEmpty()
                .Length( 8, 100 );
        }
    }
}
