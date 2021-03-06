﻿using FileOrganizer.CommonUtils;

namespace FileOrganizer.Domain
{
    public class PagingParameters
    {
        public PagingParameters( int pageSize, int pageIndex )
        {
            PageSize  = Guard.MinValue( pageSize, 1, nameof( pageSize ) );
            PageIndex = Guard.NotNegative( pageIndex, nameof( pageIndex ) );
        }

        //====== public properties

        public int PageSize  { get; }
        public int PageIndex { get; }

        public int SkipCount => PageSize * PageIndex;

        //====== public static properties

        public static PagingParameters AllAtOnce { get; } = new( int.MaxValue, 0 );

        //====== override: Object

        public override string ToString() => $"Page: {PageIndex + 1}, Page Size: {PageSize}";
    }
}
