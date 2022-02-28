using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LEA.util
{
    public abstract class Pack
    {
        public Pack()
        {

        }

        public static int bigEndianToInt(byte[] bs, int Off)
        {
            int n = bs[Off] << 24;

            n |= (bs[++Off] & 0xff) << 16;
            n |= (bs[++Off] & 0xff) << 8;
            n |= (bs[++Off] & 0xff);

            return n;
        }

        public static void bigEndianToInt(byte[] bs, int Off, int[] ns)
        {
            for (int i = 0; i < ns.Length; ++i)
            {
                ns[i] = bigEndianToInt(bs, Off);
                Off += 4;
            }
        }

        public static byte[] intToBigEndian(int n)
        {
            byte[] bs = new byte[4];
            intToBigEndian(n, bs, 0);
            return bs;
        }

        public static void intToBigEndian(int n, byte[] bs, int Off)
        {
            bs[Off] = (byte)((int)(uint)n >> 24);
            bs[++Off] = (byte)((int)(uint)n >> 16);
            bs[++Off] = (byte)((int)(uint)n >> 8);
            bs[++Off] = (byte)(n);
        }

        public static byte[] intToBigEndian(int[] ns)
        {
            byte[] bs = new byte[4 * ns.Length];
            intToBigEndian(ns, bs, 0);
            return bs;
        }

        public static void intToBigEndian(int[] ns, byte[] bs, int Off)
        {
            for (int i = 0; i < ns.Length; ++i)
            {
                intToBigEndian(ns[i], bs, Off);
                Off += 4;
            }
        }

        public static long bigEndianToLong(byte[] bs, int off)
        {
            int hi = bigEndianToInt(bs, off);
            int lo = bigEndianToInt(bs, off + 4);
            return ((long)(hi & 0xffffffffL) << 32) | (long)(lo & 0xffffffffL);
        }

        public static void bigEndianToLong(byte[] bs, int off, long[] ns)
        {
            for (int i = 0; i < ns.Length; ++i)
            {
                ns[i] = bigEndianToLong(bs, off);
                off += 8;
            }
        }

        public static byte[] longToBigEndian(long n)
        {
            byte[] bs = new byte[8];
            longToBigEndian(n, bs, 0);
            return bs;
        }

        public static void longToBigEndian(long n, byte[] bs, int off)
        {
            intToBigEndian(((int)(uint)n >> 32), bs, off);
            intToBigEndian((int)(n & 0xffffffffL), bs, off + 4);
        }

        public static byte[] longToBigEndian(long[] ns)
        {
            byte[] bs = new byte[8 * ns.Length];
            longToBigEndian(ns, bs, 0);
            return bs;
        }

        public static void longToBigEndian(long[] ns, byte[] bs, int off)
        {
            for (int i = 0; i < ns.Length; ++i)
            {
                longToBigEndian(ns[i], bs, off);
                off += 8;
            }
        }

        public static int littleEndianToInt(byte[] bs, int off)
        {
            int n = bs[off] & 0xff;
            n |= (bs[++off] & 0xff) << 8;
            n |= (bs[++off] & 0xff) << 16;
            n |= bs[++off] << 24;
            return n;
        }

        public static void littleEndianToInt(byte[] bs, int off, int[] ns)
        {
            for (int i = 0; i < ns.Length; ++i)
            {
                ns[i] = littleEndianToInt(bs, off);
                off += 4;
            }
        }

        public static void littleEndianToInt(byte[] bs, int bOff, int[] ns, int nOff, int count)
        {
            for (int i = 0; i < count; ++i)
            {
                ns[nOff + i] = littleEndianToInt(bs, bOff);
                bOff += 4;
            }
        }

        public static byte[] intToLittleEndian(int n)
        {
            byte[] bs = new byte[4];
            intToLittleEndian(n, bs, 0);
            return bs;
        }

        public static void intToLittleEndian(int n, byte[] bs, int off)
        {
            bs[off] = (byte)(n);
            bs[++off] = (byte)((int)(uint)n >> 8);
            bs[++off] = (byte)((int)(uint)n >> 16);
            bs[++off] = (byte)((int)(uint)n >> 24);
        }

        public static byte[] intToLittleEndian(int[] ns)
        {
            byte[] bs = new byte[4 * ns.Length];
            intToLittleEndian(ns, bs, 0);
            return bs;
        }

        public static void intToLittleEndian(int[] ns, byte[] bs, int off)
        {
            for (int i = 0; i < ns.Length; ++i)
            {
                intToLittleEndian(ns[i], bs, off);
                off += 4;
            }
        }

        public static long littleEndianToLong(byte[] bs, int off)
        {
            int lo = littleEndianToInt(bs, off);
            int hi = littleEndianToInt(bs, off + 4);
            return ((long)(hi & 0xffffffffL) << 32) | (long)(lo & 0xffffffffL);
        }

        public static void littleEndianToLong(byte[] bs, int off, long[] ns)
        {
            for (int i = 0; i < ns.Length; ++i)
            {
                ns[i] = littleEndianToLong(bs, off);
                off += 8;
            }
        }

        public static byte[] longToLittleEndian(long n)
        {
            byte[] bs = new byte[8];
            longToLittleEndian(n, bs, 0);
            return bs;
        }

        public static void longToLittleEndian(long n, byte[] bs, int off)
        {
            intToLittleEndian((int)(n & 0xffffffffL), bs, off);
            intToLittleEndian((int)((int)(uint)n >> 32), bs, off + 4);
        }

        public static byte[] longToLittleEndian(long[] ns)
        {
            byte[] bs = new byte[8 * ns.Length];
            longToLittleEndian(ns, bs, 0);
            return bs;
        }

        public static void longToLittleEndian(long[] ns, byte[] bs, int off)
        {
            for (int i = 0; i < ns.Length; ++i)
            {
                longToLittleEndian(ns[i], bs, off);
                off += 8;
            }
        }

    }
}
