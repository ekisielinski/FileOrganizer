using FileOrganizer.CommonUtils;
using FileOrganizer.Core.Services;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

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
            appUserCreator.Create( new AppUser( "admin", "Administrator", UserRoles.Administrator, UserRoles.Moderator ), new UserPassword( "admin" ) );
            appUserCreator.Create( new AppUser( "mod", "Moderator", UserRoles.Moderator ), new UserPassword( "mod" ) );
            appUserCreator.Create( new AppUser( "user", "User" ), new UserPassword( "user" ) );

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

            fileUploader.Upload( upload1 );
            fileUploader.Upload( upload2 );

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
    }
}
