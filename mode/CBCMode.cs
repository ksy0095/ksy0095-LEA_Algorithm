using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mode = LEA.BlockCiper.Mode;
using BlockCiper = LEA.BlockCiper;
using BlockCiperModeBlock = LEA.BlockCiperModeBlock;
using static LEA.util.Ops;

namespace LEA.mode
{ 
    public class CBCMode : BlockCiperModeBlock
    {
        public byte[] iv;
        public byte[] feedback;

        public CBCMode(BlockCiper ciper) : base(ciper) { }

        public override string getAlgorithmName
        {
            get 
            {
                return engine.getAlgorithmName + "/CBC";
            }
        }

        public override void init(Mode mode, byte[] mk, byte[] iv)
        {   
            this.mode = mode;
            engine.init(mode, mk);
            this.iv = clone(iv);

            this.feedback = new byte[blocksize];
            reset();
        }

        public override void reset()
        {
            this.reset();
            Array.Copy(iv, 0, feedback, 0, blocksize);
        }

        public override int processBlock(byte[] In, int InOff, byte[] Out, int OutOff, int length)
        {
            if(length!=blocksize)
            {
                throw new ArgumentException("length should be "+ blocksize + " In" + getAlgorithmName);
            }
            if(mode == Mode.ENCRYPT)
            {
                return encryptBlock(In, InOff, Out, OutOff);
            }

            return decryptBlock(In, InOff, Out, OutOff);
        }

        public int encryptBlock(byte[] In, int InOff, byte[] Out, int OutOff)
        {
            if((InOff + blocksize) > In.Length)
            {
                throw new InvalidOperationException("Input data too short");
            }

            XOR(feedback, 0, In, InOff, blocksize);
            engine.processBlock(feedback, 0, Out, OutOff);
            Array.Copy(Out, OutOff, feedback, 0, blocksize);
            return blocksize;
        }
        public int decryptBlock(byte[] In, int InOff, byte[] Out, int OutOff)
        {
            if ((InOff + blocksize) > In.Length)
            {
                throw new InvalidOperationException("Input data too short");
            }
            engine.processBlock(In, InOff, Out, OutOff);
            XOR(Out, OutOff,feedback, 0, blocksize);

            Array.ConstrainedCopy(In, InOff, feedback, 0, blocksize);
            return blocksize;
        }


    }
}
