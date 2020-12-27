using System;

namespace FileOrganizer.WebUI.Pages.Shared.Components
{
    public sealed class PagerModel
    {
        public int PageCount   { get; set; } = 1;
        public int CurrentPage { get; set; } = 0;

        public bool IsRequired => PageCount > 1;

        public Func<int, string> UrlFactory { get; set; } = (_) => string.Empty;
    }
}
