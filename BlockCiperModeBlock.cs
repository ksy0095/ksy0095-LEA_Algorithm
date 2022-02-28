using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LEA.BlockCiper;
using Mode = LEA.BlockCiper.Mode;

namespace LEA
{

    public abstract class BlockCiperModeBlock : BlockCiperModeImpl
    {
        
        public Padding padding;

        public BlockCiperModeBlock(BlockCiper ciper) : base(ciper) { }

        public override int getOutputSize(int len)
        {
            int size = ((len + bufferOffset) & blockmask) + blocksize;
            if(mode == Mode.ENCRYPT)
            {
                return padding != null ? size : len;
            }
            return len;
        }

        public override int getUpdateOutputSize(int len)
        {
            return (len + bufferOffset) & blockmask;
        }

        public override void init(Mode mode, byte[] mk)
        {
            throw new InvalidOperationException("This init method is not applicable to " + getAlgorithmName);
        }

        public override void init(Mode mode, byte[] mk, byte[] iv)
        {
            throw new InvalidOperationException("This init method is not applicable to " + getAlgorithmName);
        }

        public override void reset()
        {
            bufferOffset = 0;
            Array.Clear(buffer, (byte)0, buffer.Length);
        }

        public override Padding Padding
        {
            set
            {
                this.padding = value;
            }
        }
        

        public override byte[] update(byte[] msg)
        {
            if (padding != null && mode == Mode.DECRYPT)
            {
                return decryptWithPadding(msg);
            }

            if (msg == null)
            {
                return null;
            }

            int len = msg.Length;
            int gap = buffer.Length - bufferOffset;
            int inOff = 0;
            int outOff = 0;
            byte[] Out = new byte[getUpdateOutputSize(len)];

            if (len >= gap)
            {
                Array.Copy(msg, inOff, buffer, bufferOffset, gap);
                outOff += processBlock(buffer, 0, Out, outOff);

                bufferOffset = 0;
                len -= gap;
                inOff += gap;

                while (len >= buffer.Length)
                {
                    outOff += processBlock(msg, inOff, Out, outOff);
                    len -= blocksize;
                    inOff += blocksize;
                }
            }

            if (len > 0)
            {
                Array.Copy(msg, inOff, buffer, bufferOffset, len);
                bufferOffset += len;
                len = 0;
            }

            return Out;
        }

        public override byte[] doFinal()
        {
            if (padding != null)
            {
                return doFinalWithPadding();
            }

            if (bufferOffset == 0)
            {
                return null;

            }
            else if (bufferOffset != blocksize)
            {
                throw new InvalidOperationException("Bad padding");
            }

            byte[] Out = new byte[blocksize];
            processBlock(buffer, 0, Out, 0, blocksize);

            return Out;
        }

        /*
         * @param msg
         * @return
         * 
         */
        public byte[] decryptWithPadding(byte[] msg)
        {
            if (msg == null)
            {
                return null;
            }
            int len = msg.Length;
            int gap = buffer.Length - bufferOffset;
            int InOff = 0;
            int OutOff = 0;
            byte[] Out = new byte[getUpdateOutputSize(len)];

            if (len > gap)
            {
                Array.Copy(msg, InOff, buffer, bufferOffset, gap);
                OutOff += processBlock(buffer, 0, Out, OutOff);

                bufferOffset = 0;
                len -= gap;
                InOff += gap;

                while (len > buffer.Length)
                {
                    OutOff += processBlock(msg, InOff, Out, OutOff);
                    len -= blocksize;
                    InOff += blocksize;
                }
            }

            if (len > 0)
            {
                Array.Copy(msg, InOff, buffer, bufferOffset, len);
                bufferOffset += len;
                len = 0;
            }

            return Out;
        }


        /**
	 * 패딩 사용시 마지막 블록 처리
	 * 
	 * @return
	 */
        public byte[] doFinalWithPadding()
        {
            byte[] Out = null;

            if (mode == Mode.ENCRYPT)
            {
                padding.pad(buffer, bufferOffset);
                Out = new byte[getOutputSize(0)];
                processBlock(buffer, 0, Out, 0);

            }
            else
            {
                byte[] blk = new byte[blocksize];
                processBlock(buffer, 0, blk, 0);
                Out = padding.unpad(blk);
            }

            return Out;
        }

    }
}
