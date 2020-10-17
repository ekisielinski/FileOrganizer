using FileOrganizer.Core.Helpers;
using FileOrganizer.Domain;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FileOrganizer.Core.FakeDatabase
{
    public sealed class CreateAppUserHandler : IRequestHandler<CreateAppUserCommand>
    {
        readonly FakeDatabaseSingleton database;
        readonly IPasswordHasher passwordHasher;
        readonly IActivityLogger logger;

        //====== ctors

        public CreateAppUserHandler( FakeDatabaseSingleton database, IPasswordHasher passwordHasher, IActivityLogger logger )
        {
            this.database = database;
            this.passwordHasher = passwordHasher;
            this.logger = logger;
        }

        public Task<Unit> Handle( CreateAppUserCommand request, CancellationToken cancellationToken )
        {
            if (database.Users.Any( x => x.AppUserDetails.User.Name.Value == request.UserName.Value ))
            {
                throw new Exception( "User already exists." );
            }

            string hash = passwordHasher.HashPassword( request.UserPassword );
            var appu = new AppUser( request.UserName, request.DisplayName, UserRoles.Empty);
            var appUserDetails = new AppUserDetails( appu, null, database.UtcNow );

            database.Users.Add( new UserEntry { AppUserDetails = appUserDetails, PasswordHash = hash } );

            logger.Add( " new user created:" + request.UserName );

            return Unit.Task;
        }
    }
}
