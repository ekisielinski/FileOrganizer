using FileOrganizer.Domain;
using MediatR;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;

namespace FileOrganizer.Core
{
    public sealed class DefaultDatabaseInitializer : IDatabaseInitializer
    {
        readonly IMediator mediator;

        //====== ctors

        public DefaultDatabaseInitializer( IMediator mediator )
        {
            this.mediator = mediator;
        }

        //====== IDatabaseInitializer

        private void CreateAppUsers()
        {
            var cmd1 = new CreateAppUserCommand( new UserName( "admin" ), new UserDisplayName( "Administrator" ), new UserPassword( "admin" ) );
            var cmd2 = new CreateAppUserCommand( new UserName( "mod"   ), new UserDisplayName( "Moderator"     ), new UserPassword( "mod"   ) );
            var cmd3 = new CreateAppUserCommand( new UserName( "user"  ), new UserDisplayName( "User"          ), new UserPassword( "user"  ) );

            mediator.Send( cmd1 ).Wait(); // TODO: .Wait()
            mediator.Send( cmd2 ).Wait();
            mediator.Send( cmd3 ).Wait();
        }

        private void UpdateEmails()
        {
            var cmd1 = new UpdateAppUserDetailsCommand( new UserName( "admin" ), null, new EmailAddress( "admin@x.com" ), false );
            var cmd2 = new UpdateAppUserDetailsCommand( new UserName( "mod" ),   null, new EmailAddress( "mod@x.com" ),   false );
            var cmd3 = new UpdateAppUserDetailsCommand( new UserName( "user" ),  null, new EmailAddress( "user@x.com" ),  false );

            mediator.Send( cmd1 ).Wait(); // todo: wait
            mediator.Send( cmd2 ).Wait();
            mediator.Send( cmd3 ).Wait();
        }

        private void UpdateUserRoles()
        {
            var cmd1 = new SetAppUserRolesCommand( new UserName( "admin" ), new UserRoles( new[] { UserRole.Administrator } ) );
            var cmd2 = new SetAppUserRolesCommand( new UserName( "mod" ),   new UserRoles( new[] { UserRole.Moderator } ) );

            mediator.Send( cmd1 ).Wait();
            mediator.Send( cmd2 ).Wait();
        }

        private void UploadFiles()
        {
            var upload1 = new UploadParameters( new[]
            {
                CreateFakeImage( 100, 100, Color.Red, "red.jpg" ),
                CreateFakeImage( 25,  25, Color.Green, "green.jpg" ),
                CreateFakeImage( 100, 20, Color.Blue, "blue.jpg" ),
                CreateFakeImage( 20, 80, Color.Black, "black.jpg" ),
            }, new UploadDescription( "Small rectangles" ) );

            var upload2 = new UploadParameters( new[]
            {
                CreateFakeImage( 400, 400, Color.AliceBlue, null ),
                CreateFakeImage( 500, 200, Color.BlueViolet, "blue-violet.jpg" ),
                CreateFakeImage( 300, 400, Color.Crimson, "crimson.jpg" ),
            }, new UploadDescription( "Big rectangles" ) );

            var upload3 = new UploadParameters( Enumerable.Repeat( CreateFakeImage( 50, 50, Color.BurlyWood, "_.jpg" ), 10),
                new UploadDescription( "Many small squares" ) );

            var upload4 = new UploadParameters( new[] { CreateFakeTextFile( "hello", "hello.txt" ) }, new UploadDescription( "1 text file" ) );

            mediator.Send( new UploadFilesCommand( upload1 ) ).Wait(); // todo wait
            mediator.Send( new UploadFilesCommand( upload2 ) ).Wait();
            mediator.Send( new UploadFilesCommand( upload3 ) ).Wait();
            mediator.Send( new UploadFilesCommand( upload4 ) ).Wait();
        }

        public void Init()
        {
            CreateAppUsers();
            UpdateUserRoles();
            UpdateEmails();
            UploadFiles();

            var cmd = new UpdateFileDetailsCommand(
                new FileId( 1 ),
                new FileTitle( "Red square" ),
                new FileDescription( "Red square - description" ),
                new PartialDateTime( 2020, 1, 2, 3, 4 ) );

            mediator.Send( cmd ).Wait(); // todo: wait
        }

        private static SourceFile CreateFakeImage( int width, int height, Color color, string? orginalFileName )
        {
            var bitmap = new Bitmap( width, height );

            using var grp = Graphics.FromImage( bitmap );
            grp.Clear( color );

            var ms = new MemoryStream();
            bitmap.Save( ms, ImageFormat.Jpeg );

            return new SourceFile( ms, new MimeType( "image/jpeg" ), orginalFileName );
        }

        private static SourceFile CreateFakeTextFile( string text, string? orginalFileName )
        {
            byte[]? bytes = Encoding.UTF8.GetBytes( text );

            var ms = new MemoryStream( bytes );

            return new SourceFile( ms, new MimeType( "text/plain" ), orginalFileName );
        }
    }
}
