using FileOrganizer.CommonUtils;
using System;
using System.Linq;
using System.Text;

namespace FileOrganizer.Core
{
    public sealed class Sha256Hash : IEquatable<Sha256Hash>
    {
        readonly byte[] data;

        //====== ctors

        public Sha256Hash( byte[] data )
        {
            this.data = Guard.NotNull( data, nameof( data ) );

            if (data.Length != 32) throw new ArgumentException( "Array must contains exactly 32 bytes." );
        }

        //====== public methods

        public string ToHexString()
        {
            var sb = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sb.Append( data[i].ToString( "x2" ) );
            }

            return sb.ToString();
        }

        //====== IEquatable<Sha256Hash>

        public bool Equals( Sha256Hash? other )
        {
            if (other is null) return false;

            return data.SequenceEqual( other.data );
        }

        //====== override: Object

        public override bool Equals( object? obj ) => Equals( obj as Sha256Hash );

        public override int GetHashCode() => ToHexString().GetHashCode(); // TODO: slow

        public override string ToString() => ToHexString();
    }
}
