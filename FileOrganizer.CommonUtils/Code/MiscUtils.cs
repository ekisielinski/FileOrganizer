namespace FileOrganizer.CommonUtils
{
    public static class MiscUtils
    {
        public static int? TryParse( string number )
            => int.TryParse( number, out int result ) ? result : (int?) null;
    }
}
