using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LEA.BlockCiper;
using Mode = LEA.BlockCiper.Mode;

namespace LEA
{
	public abstract class BlockCiperModeImpl : BlockCiperMode
	{
		public Mode mode;
		public BlockCiper engine;

		public byte[] buffer;
		public int bufferOffset;
		public int blocksize;
		public int blockmask;

		public BlockCiperModeImpl(BlockCiper cipher)
		{
			engine = cipher;
			blocksize = engine.getBlockSize;
			blockmask = getBlockmask(blocksize);
			buffer = new byte[blocksize];
		}

		public override byte[] doFinal(byte[] msg)
		{
			byte[] part1 = update(msg);
			byte[] part2 = doFinal();

			int len1 = part1 == null ? 0 : part1.Length;
			int len2 = part2 == null ? 0 : part2.Length;

			byte[] Out = new byte[len1 + len2];

			if (len1 > 0)
			{
				Array.Copy(part1, 0, Out, 0, len1);
			}

			if (len2 > 0)
			{
				Array.Copy(part2, 0, Out, len1, len2);
			}

			return Out;
		}

		public abstract int processBlock(byte[] In, int InOff, byte[] Out, int OutOff, int length);

		public int processBlock(byte[] In, int InOff, byte[] Out, int OutOff)
		{
			return processBlock(In, InOff, Out, OutOff, blocksize);
		}

		public static int getBlockmask(int blocksize)
		{
			uint mask = 0;

			switch (blocksize)
			{
				case 8: // 64-bit
					mask = 0xfffffff7;
					break;

				case 16: // 128-bit
					mask = 0xfffffff0;
					break;

				case 32: // 256-bit
					mask = 0xffffffe0;
					break;
			}

			return (int)mask;
		}

		public static byte[] clone(byte[] array)
		{
			if (array == null)
			{
				return null;
			}

			byte[] clone = new byte[array.Length];
			Array.Copy(array, 0, clone, 0, clone.Length);
			return clone;
		}

	}
}
