using FileOrganizer.CommonUtils;
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
            => this.mediator = Guard.NotNull( mediator, nameof( mediator ) );

        //====== IDatabaseInitializer

        private void CreateAppUsers()
        {
            var cmd1 = new CreateAppUserCommand( new( "admin" ), new( "Administrator" ), new( "admin" ) );
            var cmd2 = new CreateAppUserCommand( new( "mod"   ), new( "Moderator"     ), new( "mod"   ) );
            var cmd3 = new CreateAppUserCommand( new( "user"  ), new( "User"          ), new( "user"  ) );

            mediator.Send( cmd1 ).Wait(); // TODO: .Wait()
            mediator.Send( cmd2 ).Wait();
            mediator.Send( cmd3 ).Wait();
        }
        
        private void UpdateUserRoles()
        {
            var cmd1 = new SetAppUserRolesCommand( new( "admin" ), new( new[] { UserRole.Administrator } ) );
            var cmd2 = new SetAppUserRolesCommand( new( "mod" ),   new( new[] { UserRole.Moderator } ) );

            mediator.Send( cmd1 ).Wait();
            mediator.Send( cmd2 ).Wait();
        }
        
        private void UpdateEmails()
        {
            var cmd1 = new UpdateAppUserDetailsCommand( new( "admin" ), null, DataUpdateBehavior<EmailAddress>.CreateOrUpdateValue( new( "admin@x.com" ) ) );
            var cmd2 = new UpdateAppUserDetailsCommand( new( "mod" ),   null, DataUpdateBehavior<EmailAddress>.CreateOrUpdateValue( new( "mod@x.com"   ) ) );
            var cmd3 = new UpdateAppUserDetailsCommand( new( "user" ),  null, DataUpdateBehavior<EmailAddress>.CreateOrUpdateValue( new( "user@x.com"  ) ) );

            mediator.Send( cmd1 ).Wait(); // todo: wait
            mediator.Send( cmd2 ).Wait();
            mediator.Send( cmd3 ).Wait();
        }

        public void UpdateDisplayName()
        {
            var cmd = new UpdateAppUserDetailsCommand( new( "user" ), new( "user (updated)" ), DataUpdateBehavior<EmailAddress>.IgnoreValue() );
            mediator.Send( cmd ).Wait(); // todo: wait
        }

        private void UploadFiles()
        {
            var upload1 = new UploadParameters( new[]
            {
                CreateFakeImage( 100, 100, Color.Red,   "red.jpg"   ),
                CreateFakeImage( 25,  25,  Color.Green, "green.jpg" ),
                CreateFakeImage( 100, 20,  Color.Blue,  "blue.jpg"  ),
                CreateFakeImage( 20,  80,  Color.Black, "black.jpg" ),
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
            UpdateDisplayName();
            UploadFiles();

            var cmd = new UpdateFileDetailsCommand(
                new FileId( 1 ),
                new FileTitle( "Red square" ),
                new FileDescription( "Red square - description" ),
                new PartialDateTime( 2020, 1, 2, 3, 4 ) );

            mediator.Send( cmd ).Wait(); // todo: wait

            var cmd1 = new AddFileLinkCommand(
                new FileId( 1 ),
                new LinkUrl( "http://example.com" ),
                new LinkTitle( "Example Site" ),
                new LinkComment( "Comment.." ) );

            mediator.Send( cmd1 ).Wait(); // todo: wait

            var cmd2 = new CreateTagCommand( new( "tag1" ), new( "Tag1 Display name" ), new( "Tag1 desc" ) );
            var cmd3 = new CreateTagCommand( new( "tag2" ), new( "Tag2 Display name" ), new( "Tag2 desc" ) );

            mediator.Send( cmd2 ).Wait(); // todo: wait
            mediator.Send( cmd3 ).Wait(); // todo: wait
        }

        private static SourceFile CreateFakeImage( int width, int height, Color color, string? orginalFileName )
        {
            var bitmap = new Bitmap( width, height );

            using var grp = Graphics.FromImage( bitmap );
            grp.Clear( color );

            var ms = new MemoryStream();
            bitmap.Save( ms, ImageFormat.Jpeg );

            return new SourceFile( ms, new( "image/jpeg" ), orginalFileName );
        }

        private static SourceFile CreateFakeTextFile( string text, string? orginalFileName )
        {
            byte[]? bytes = Encoding.UTF8.GetBytes( text );

            var ms = new MemoryStream( bytes );

            return new SourceFile( ms, new( "text/plain" ), orginalFileName );
        }
    }
}
