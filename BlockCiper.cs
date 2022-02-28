using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LEA
{
    public abstract class BlockCiper
    {
        public enum Mode
        {
            ENCRYPT, //Encryption Mode
            DECRYPT  //Decryption Mode
        }

        public abstract void init(Mode mode, byte[] mk);

        //param mode
        //param mk

        public abstract void reset();

        public abstract string getAlgorithmName { get; }
        //return AlgorithmName


        public abstract int getBlockSize { get; }

        //return 1 Block Size

        public abstract int processBlock(byte[] In, int InOff, byte[] Out, int OutOff);
    }
}
