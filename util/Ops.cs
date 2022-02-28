using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LEA.util
{
    public class Ops
    {
        private Ops()
        {

        }

        /*
         *lhs ^=rhs
         * 
         *@param lhs
         *           [Out]
         *@param rhs
         *           [In]
         */
        public static void XOR(byte[] lhs, byte[] rhs)
        {
            if (lhs == null || rhs == null)
            {
                throw new ArgumentNullException("Any of input arrays should not be null");
            }

            if (lhs.Length != rhs.Length)
            {
                throw new ArgumentException("the length of two arrays should be same");
            }
            for (int i = 0; i < lhs.Length; ++i)
            {
                lhs[i] ^= rhs[i];
            }
        }
        /*
         *@param    lhs
         *@param    lhsOff
         *@param    rhs
         *@param    rhsOff
         *@param    len
         */
        public static void XOR(byte[] lhs, int lhsOff, byte[] rhs, int rhsOff, int len)
        {
            if (lhs == null || rhs == null)
            {
                throw new ArgumentNullException("Any of input arrays should not be null");
            }
            if (lhs.Length < lhsOff + len || rhs.Length < rhsOff + len)
            {
                throw new ArgumentOutOfRangeException();
            }
            for (int i = 0; i < len; ++i)
            {
                lhs[lhsOff + i] ^= rhs[rhsOff + i];
            }
        }

        /*
        *lhs = rhs1 ^ rhs2
        *@param lhs
        *           [Out]
        *@param rhs1
        *           [In]
        *@param rhs2
        *           [In]
        */
        public static void XOR(byte[] lhs, byte[] rhs1, byte[] rhs2)
        {
            if (lhs == null || rhs1 == null || rhs2 == null)
            {
                throw new ArgumentNullException("Any of input arrays should not be same");
            }

            if (lhs.Length != rhs1.Length || lhs.Length != rhs2.Length)
            {
                throw new ArgumentException("the length of arrays should be same");
            }

            for (int i = 0; i < lhs.Length; ++i)
            {
                lhs[i] = (byte)(rhs1[i] ^ rhs2[i]);
            }
        }
        /*
        *@param Out
        *@param OutOff
        *@param len
        *@param lhs
        *@param lhs1Off
        *@param rhs
        *@param rhsOff
        */
        public static void XOR(byte[] Out, int OutOff, byte[] lhs, int lhs1Off, byte[] rhs, int rhsOff, int len)
        {
            if (Out == null || lhs == null || rhs == null)
            {
                throw new ArgumentNullException("Any of input arrays should not be null");
            }
            if (Out.Length < OutOff + len || lhs.Length < lhs1Off + len || rhs.Length < rhsOff + len)
            {
                throw new ArgumentOutOfRangeException();
            }
            for (int i = 0; i < len; ++i)
            {
                Out[OutOff + i] = (byte)(lhs[lhs1Off + i] ^ rhs[rhsOff + i]);
            }
        }
        /*
         *lhs ^=rhs 
         * 
         * @param lhs
         *            [Out]
         * @param rhs
         *            [In]
         */
        public static void XOR(int[] lhs, int[] rhs)
        {
            if (lhs == null || rhs == null)
            {
                throw new ArgumentNullException("Any of input arrays should not be null");
            }
            if (lhs.Length != rhs.Length)
            {
                throw new ArgumentException("the length of two arrays should be same");
            }

            for (int i = 0; i < lhs.Length; ++i)
            {
                lhs[i] ^= rhs[i];
            }
        }

        public static void XOR(int[] lhs, int lhsOff, int[] rhs, int rhsOff, int len)
        {
            if (lhs == null || rhs == null)
            {
                throw new ArgumentNullException("Any of input arrays should not be null");
            }
            if (lhs.Length < lhsOff + len || rhs.Length < rhsOff + len)
            {
                throw new ArgumentOutOfRangeException();
            }
            for (int i = 0; i < len; ++i)
            {
                lhs[lhsOff + i] ^= rhs[rhsOff + i];
            }
        }
        /*
         * lhs = rhs1 ^ rhs2 
         * 
         * @param lhs
         *           [Out]
         * @param rhs1
         *           [In]
         * @param rhs2
         *           [In]
         */

        public static void XOR(int[] lhs, int[] rhs1, int[] rhs2)
        {
            if (lhs == null || rhs1 == null || rhs2 == null)
            {
                throw new ArgumentNullException("Any of input arrays should not be null");
            }
            if (lhs.Length != rhs1.Length || lhs.Length != rhs2.Length)
            {
                throw new ArgumentException("the length of arrays should be same");
            }
            for (int i = 0; i < lhs.Length; ++i)
            {
                lhs[i] = rhs1[i] ^ rhs2[i];
            }
        }

        /*
         * @param lhs
         * @param lhsOff
         * @param len
         * @param rhs1
         * @param rhs1Off
         * @param rhs2
         * @param rhs2Off
         */
        public static void XOR(int[] lhs, int lhsOff, int[] rhs1, int rhs1Off, int[] rhs2, int rhs2Off, int len)
        {
            if (lhs == null || rhs1 == null || rhs2 == null)
            {
                throw new ArgumentNullException("Any of input arrays should not be null");
            }
            if (lhs.Length < lhsOff + len || rhs1.Length < rhs1Off + len || rhs2.Length < rhs2Off + len)
            {
                throw new ArgumentOutOfRangeException();
            }

            for (int i = 0; i < len; ++i)
            {
                lhs[lhsOff + i] = rhs1[rhs1Off + i] ^ rhs2[rhs2Off + i];
            }
        }

        public static long TripleShift(long n, int s)
        {
            if (n >= 0)
                return n >> s;
            return (n >> s) + (2 << ~s);
        }
        public static void shiftLeft(byte[] bytes, in int shift)
        {
            if (bytes == null)
            {
                throw new ArgumentNullException("Input array should not be null");
            }
            if (shift < 1 || shift > 7)
            {
                throw new ArgumentException("the allowed shift amount is 1 ~ 7");
            }

            int tmp = bytes[0];
            for (int i = 1; i < bytes.Length; ++i)
            {
                tmp = tmp << 8 | (bytes[i] & 0xff);
                //bytes[i - 1] = (byte) TripleShift(((tmp << shift)&0xff00), 8);
                bytes[i - 1] = (byte)(((tmp << shift) & 0xff00) >> 8);
            }
            bytes[bytes.Length - 1] <<= shift;
        }

        public static void shiftRight(byte[] bytes, in int shift)
        {
            if (bytes == null)
            {
                throw new ArgumentNullException("input array should not be null");
            }

            if (shift < 1 || shift > 7)
            {
                throw new ArgumentException("the allowed shift amount is 1~7");
            }

            int tmp = bytes[bytes.Length - 1];
            for (int i = bytes.Length - 1; i > 0; --i)
            {
                tmp = bytes[i - 1] << 8 | (bytes[i] & 0xff);
                bytes[i] = (byte)(tmp >> shift);
            }
            tmp = bytes[0] & 0xff;
            bytes[0] = (byte)(tmp >> shift);
        }

        /**
        * byte array to int array
        */
        public static void pack(byte[] In, int[] Out)
        {
            if (In == null || Out == null)
            {
                throw new ArgumentNullException();
            }

            if (In.Length != Out.Length * 4)
            {
                throw new ArgumentOutOfRangeException();
            }

            int outIdx = 0;
            for (int inIdx = 0; inIdx < In.Length; ++inIdx, ++outIdx)
            {
                Out[outIdx] = (In[inIdx] & 0xff);
                Out[outIdx] |= (In[++inIdx] & 0xff) << 8;
                Out[outIdx] |= (In[++inIdx] & 0xff) << 16;
                Out[outIdx] |= (In[++inIdx] & 0xff) << 24;
            }

        }

        public static void pack(byte[] In, int InOff, int[] Out, int OutOff, int Inlen)
        {
            if (In == null || Out == null)
            {
                throw new ArgumentNullException();
            }

            if ((Inlen & 3) != 0)
            {
                throw new ArgumentException("length should be multiple of 4");
            }

            if (In.Length < InOff + Inlen || Out.Length < OutOff + Inlen / 4)
            {
                throw new ArgumentOutOfRangeException();
            }

            int outIdx = OutOff;
            int endInIdx = InOff + Inlen;
            for (int inIdx = InOff; inIdx < endInIdx; ++inIdx, ++outIdx)
            {
                Out[outIdx] = (In[inIdx] & 0xff);
                Out[outIdx] |= (In[++inIdx] & 0xff) << 8;
                Out[outIdx] |= (In[++inIdx] & 0xff) << 16;
                Out[outIdx] |= (In[++inIdx] & 0xff) << 24;
            }
        }

        /**
 * int array to byte array
 */
        public static void unpack(int[] In, byte[] Out)
        {
            if (In == null || Out == null)
            {
                throw new ArgumentNullException();
            }

            if (In.Length * 4 != Out.Length)
            {
                throw new ArgumentOutOfRangeException();
            }

            int outIdx = 0;
            for (int inIdx = 0; inIdx < In.Length; ++inIdx, ++outIdx)
            {
                Out[outIdx] = (byte)In[inIdx];
                Out[++outIdx] = (byte)(In[inIdx] >> 8);
                Out[++outIdx] = (byte)(In[inIdx] >> 16);
                Out[++outIdx] = (byte)(In[inIdx] >> 24);
            }
        }

        public static void unpack(int[] In, int InOff, byte[] Out, int OutOff, int Inlen)
        {
            if (In == null || Out == null)
            {
                throw new ArgumentNullException();
            }

            if (In.Length < InOff + Inlen || Out.Length < OutOff + Inlen * 4)
            {
                throw new ArgumentOutOfRangeException();
            }

            int outIdx = OutOff;
            int endInIdx = InOff + Inlen;
            for (int inIdx = InOff; inIdx < endInIdx; ++inIdx, ++outIdx)
            {
                Out[outIdx] = (byte)In[inIdx];
                Out[++outIdx] = (byte)(In[inIdx] >> 8);
                Out[++outIdx] = (byte)(In[inIdx] >> 16);
                Out[++outIdx] = (byte)(In[inIdx] >> 24);
            }
        }

        public static void XOR(long[] lhs, int lhsOff, long[] rhs, int rhsOff, int len)
        {
            if (lhs == null || rhs == null)
            {
                throw new ArgumentNullException("any of input arrarys should not be null");
            }

            if (lhs.Length < lhsOff + len || rhs.Length < rhsOff + len)
            {
                throw new ArgumentOutOfRangeException();
            }

            for (int i = 0; i < len; ++i)
            {
                lhs[lhsOff + i] ^= rhs[rhsOff + i];
            }
        }
    }
}
