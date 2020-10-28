namespace FileOrganizer.CommonUtils
{
    public sealed class DataUpdateBehavior<T> where T : class
    {
        public T? Data { get; private set; } = null;

        public bool Delete { get; private set; } = false;
        public bool Ignore { get; private set; } = false;

        public bool CreateOrUpdate => Data != null;

        //====== public static methods

        public static DataUpdateBehavior<T> CreateOrUpdateValue( T data )
            => new DataUpdateBehavior<T>() { Data = Guard.NotNull( data, nameof( data ) ) };

        public static DataUpdateBehavior<T> DeleteValue()
            => new DataUpdateBehavior<T>() { Delete = true };

        public static DataUpdateBehavior<T> IgnoreValue()
            => new DataUpdateBehavior<T>() { Ignore = true };
    }
}
