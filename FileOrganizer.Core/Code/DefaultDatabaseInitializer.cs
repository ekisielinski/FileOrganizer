using FileOrganizer.CommonUtils;
using FileOrganizer.Core.Services;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

namespace FileOrganizer.Core
{
    public sealed class DefaultDatabaseInitializer : IDatabaseInitializer
    {
        readonly IAppUserCreator appUserCreator;
        readonly IFileUploader fileUploader;
        readonly IFileDetailsUpdater fileDetailsUpdater;

        //====== ctors

        public DefaultDatabaseInitializer( IAppUserCreator appUserCreator, IFileUploader fileUploader, IFileDetailsUpdater fileDetailsUpdater )
        {
            this.appUserCreator = Guard.NotNull( appUserCreator, nameof( appUserCreator ) );
            this.fileUploader = Guard.NotNull( fileUploader, nameof( fileUploader ) );
            this.fileDetailsUpdater = Guard.NotNull( fileDetailsUpdater, nameof( fileDetailsUpdater ) );
        }

        //====== IDatabaseInitializer

        public void Init()
        {
            appUserCreator.Create( CreateUser( "admin", "Administrator", UserRole.Administrator, UserRole.Moderator ), new UserPassword( "admin" ) );
            appUserCreator.Create( CreateUser( "mod", "Moderator", UserRole.Moderator ), new UserPassword( "mod" ) );
            appUserCreator.Create( CreateUser( "user", "User" ), new UserPassword( "user" ) );

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

            fileUploader.Upload( upload1 );
            fileUploader.Upload( upload2 );
            fileUploader.Upload( upload3 );

            // temp
            fileDetailsUpdater.UpdateTitle( new FileId( 0 ), new FileTitle( "Red square" ) );
            fileDetailsUpdater.UpdateDescription( new FileId( 0 ), new FileDescription( "Red square - description" ) );
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

        private static AppUser CreateUser( string name, string displayName, params UserRole[] roles )
        {
            var userRoles = new UserRoles( roles );

            return new AppUser( new UserName( name ), new UserDisplayName( displayName ), userRoles );
        }
    }
}
