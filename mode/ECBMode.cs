using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mode = LEA.BlockCiper.Mode;
using BlockCiper = LEA.BlockCiper;
using BlockCiperModeBlock = LEA.BlockCiperModeBlock;

namespace LEA.mode
{
    public class ECBMode : BlockCiperModeBlock
    {
        public ECBMode(BlockCiper ciper) : base(ciper) { }

        public override string getAlgorithmName
        {
            get
            {
                return engine.getAlgorithmName + "/ECB";
            }
        }

        public override void init(Mode mode, byte[] mk)
        {
            this.mode = mode;
            engine.init(mode, mk);
        }

        public override int processBlock(byte[] In, int InOff, byte[] Out, int OutOff, int Outlen)
        {
            if (Outlen != blocksize)
            {
                throw new InvalidOperationException("Outlen should be " + blocksize + " in " + getAlgorithmName);
            }

            if ((InOff + blocksize) > In.Length)
            {
                throw new InvalidOperationException("input data too short");
            }

            return engine.processBlock(In, InOff, Out, OutOff);
        }



    }
}
