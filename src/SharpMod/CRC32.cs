using System;
using SharpMod.MetaMod;

namespace SharpMod
{
	/// <summary>
	/// A class which exposes the server CRC32 functionality
	/// </summary>
	public class CRC32
	{
		private long crc32;
		private IntPtr crc32ptr;

		/// <summary>
		/// Creates an instance of the CRC32 class
		/// </summary>
		unsafe public CRC32()
		{
			fixed (long *longPtr =  &crc32) {
				crc32ptr = new IntPtr(longPtr);
			}
		}

		/// <summary>
		/// Creates an instance of the CRC32 class, the pointer points to
		/// the spot where the data is accumulated
		/// </summary>
		/// <param name="ptr">
		/// A pointer to an accumulation spot <see cref="IntPtr"/>
		/// </param>
		public CRC32(IntPtr ptr)
		{
			crc32ptr = ptr;
		}

		/// <summary>
		/// Processes a memory segment
		/// </summary>
		/// <param name="ptr">
		/// A pointer to the memory chunk <see cref="IntPtr"/>
		/// </param>
		/// <param name="size">
		/// the size of the memory <see cref="System.Int32"/>
		/// </param>
		public void Process(IntPtr ptr, int size)
		{
			MetaModEngine.engineFunctions.CRC32_ProcessBuffer(crc32ptr, ptr, size);
		}

		/// <summary>
		/// Processes a character
		/// </summary>
		/// <param name="ch">
		/// A character to process <see cref="System.Char"/>
		/// </param>
		public void Process(char ch)
		{
			MetaModEngine.engineFunctions.CRC32_ProcessByte(crc32ptr, ch);
		}

		/// <summary>
		/// Process an Integer
		/// </summary>
		/// <param name="i">
		/// An integer to process <see cref="System.Int32"/>
		/// </param>
		public void Process(int i)
		{
			Process((char)i);
			Process((char)(i >>  8));
			Process((char)(i >> 16));
			Process((char)(i >> 24));
		}

		/// <summary>
		/// Process a long
		/// </summary>
		/// <param name="l">
		/// A long <see cref="System.Int64"/>
		/// </param>
		public void Process(long l)
		{
			Process((int)l);
			Process((int)(l >> 32));
		}

		public void Process(string str)
		{
			foreach (char c in str) {
				Process(c);
			}
		}

		/// <summary>
		/// Finalizes the CRC32 algorithm and returns a value
		/// </summary>
		/// <returns>
		/// A crc32 number <see cref="System.Int64"/>
		/// </returns>
		public long Final()
		{
			return MetaModEngine.engineFunctions.CRC32_Final(crc32);
		}
	}
}
