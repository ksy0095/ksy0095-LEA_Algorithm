//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using static LEA.Padding;
//delegate string del(byte[] padByteArr);

//namespace LEA.padding
//{
//    public class PKCS7Padding : Padding
//    {
//        static readonly del dt = null;

//        public PKCS7Padding(int blocksize) : base(blocksize)
//        {
//            this.blocksize = blocksize;
//        }

//        public override byte[] unpad(byte[] padByteArr)
//        {
//            if (padByteArr == null)
//            {
//                throw new ArgumentNullException("padByteArr", "padByteArr can not be null");
//            }

//            dynamic last = padByteArr[padByteArr.Length - 1];
//            if (padByteArr.Length <= last)
//            {
//                return padByteArr;
//            }

//            for (int i = padByteArr.Length - 2; i >= padByteArr.Length - last; i += -1)
//            {
//                if (dt(BitConverter.GetBytes(i)) != last)
//                {
//                    return padByteArr;
//                }
//            }

//            return SubArr(padByteArr, 0, (padByteArr.Length - last));
//        }

//        public override byte[] pad(byte[] data, int len)
//        {
//            if (data.Length > 256)
//            {
//                throw new ArgumentOutOfRangeException("data", "data must be <= 256 in length");
//            }
//            if (len > 256)
//            {
//                throw new ArgumentOutOfRangeException("paddingLength", "paddingLength must be <= 256");
//            }

//            if (len <= data.Length)
//            {
//                return data;
//            }

//            byte[] output = new byte[len];
//            Buffer.BlockCopy(data, 0, output, 0, data.Length);
//            for (int i = data.Length; i < output.Length; i++)
//            {
//                output[i] = (byte)(len - data.Length);
//            }
//            return output;
//        }

//        public override T[] SubArr<T>(T[] arr, int start, int len)
//        {
//            T[] result = new T[len];
//            Buffer.BlockCopy(arr, start, result, 0, len);
//            return result;
//        }

//    }
//}
