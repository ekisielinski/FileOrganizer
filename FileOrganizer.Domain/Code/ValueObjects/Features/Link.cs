namespace FileOrganizer.Domain
{
    public abstract class Link
    {
        public LinkId       Id        { get; set; }
        public LinkUrl      Address   { get; set; }
        public LinkTitle    Title     { get; set; }
        public LinkComment  Comment   { get; set; }
        public UtcTimestamp WhenAdded { get; set; }
        public AppUserNames AddedBy   { get; set; }
    }
}
