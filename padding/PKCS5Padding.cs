using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LEA.Padding;

namespace LEA.padding
{
    public class PKCS5Padding : Padding
    {
        public delegate byte[] dele(byte[] In);

        public static dele dt;
        
        public PKCS5Padding(int blocksize) : base(blocksize)
        {
            this.blocksize = blocksize;
        }

        public override byte[] pad(byte[] In)
        {
            if (In == null)
            {
                throw new ArgumentNullException();
            }
            if (In.Length < 0 || In.Length > blocksize)
            {
                throw new ArgumentException("Input should be shorter than blocksize");
            }

            byte[] Out = new byte[blocksize];
            Array.Copy(In, 0, Out, 0, In.Length);
            pad(Out, In.Length);
            return Out;
        }

        public override byte[] pad(byte[] In, int len)
        {
            if (In.Length > 256)
            {
                throw new ArgumentOutOfRangeException("data", "data must be <= 256 in length");
            }
            if (len > 256)
            {
                throw new ArgumentOutOfRangeException("paddingLength", "paddingLength must be <= 256");
            }

            if (len <= In.Length)
            {
                return In;
            }

            byte[] output = new byte[len];
            Buffer.BlockCopy(In, 0, output, 0, In.Length);
            for (int i = In.Length; i < output.Length; i++)
            {
                output[i] = (byte)(len - In.Length);
            }
            return output;

        }

        public override byte[] unpad(byte[] In)
        {
            if(In == null)
            {
                throw new ArgumentNullException("padByteArr", "padByteArr can not be null");
            }

            dynamic last = In[In.Length - 1];
            if(In.Length <=last)
            {
                return In;
            }

            for(int i=In.Length -1; i>=In.Length -1; --i)
            {
                if(dt(BitConverter.GetBytes(i))!=last)
                {
                    return In;
                }
            }

            return SubArr(In, 0, (In.Length - last));
        }

        public override T[] SubArr<T>(T[] arr, int start, int len)
        {
            T[] result = new T[len];
            Buffer.BlockCopy(arr, start, result, 0, len);
            return result;
        }

    }
}
