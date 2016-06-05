using Org.Apache.Hadoop.IO;
using Org.Apache.Hadoop.Util;


namespace Org.Apache.Hadoop.FS
{
	/// <summary>For CRC32 with the Castagnoli polynomial</summary>
	public class MD5MD5CRC32CastagnoliFileChecksum : MD5MD5CRC32FileChecksum
	{
		/// <summary>Same as this(0, 0, null)</summary>
		public MD5MD5CRC32CastagnoliFileChecksum()
			: this(0, 0, null)
		{
		}

		/// <summary>Create a MD5FileChecksum</summary>
		public MD5MD5CRC32CastagnoliFileChecksum(int bytesPerCRC, long crcPerBlock, MD5Hash
			 md5)
			: base(bytesPerCRC, crcPerBlock, md5)
		{
		}

		public override DataChecksum.Type GetCrcType()
		{
			// default to the one that is understood by all releases.
			return DataChecksum.Type.Crc32c;
		}
	}
}
