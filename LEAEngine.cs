using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LEA.util.Ops;
using BlockCiper = LEA.BlockCiper;

namespace LEA
{
    public class LEAEngine : BlockCiper
    {
        public const int BLOCKSIZE = 16;
        public static readonly int[] delta = new int[] {unchecked((int)0xc3efe9db), 0x44626b02, 0x79e27c8a, 0x78df30ec, 0x715ea49e,unchecked((int) 0xc785da0a),unchecked((int) 0xe04ef22a),unchecked((int) 0xe5c40957)};
        
        public Mode mode;
        public int rounds;
        public int[,] roundKeys;
        public int[] block;

        public LEAEngine()
        {
            block = new int[BLOCKSIZE / 4];
        }

        public override void init(Mode mode, byte[] mk)
        {
            this.mode = mode;
            generateRoundKeys(mk);
        }

        public override void reset()
        {
            Array.Clear(block, 0, block.Length);
        }

        public override string getAlgorithmName
        {
            get
            {
                return "LEA";
            }
        }
        
        public override int getBlockSize
        {
            get
            {
                return BLOCKSIZE;
            }
        }
        
        public override int processBlock(byte[] In, int InOff, byte[] Out, int OutOff)
        {
            if (In == null || Out == null)
            {
                throw new NullReferenceException("In and Out should not be null");
            }

            if (In.Length - InOff < BLOCKSIZE)
            {
                throw new InvalidOperationException("too short input data " + In.Length + " " + InOff);
            }

            if (Out.Length - OutOff < BLOCKSIZE)
            {
                throw new InvalidOperationException("too short output buffer " + Out.Length + " / " + OutOff);
            }

            if (mode == Mode.ENCRYPT)
            {
                return encryptionBlock(In, InOff, Out, OutOff);
            }
           
            return decryptionBlock(In, InOff, Out, OutOff);
            

            throw new InvalidOperationException();
        }

        public int encryptionBlock(byte[] In, int InOff, byte[] Out, int OutOff)
        {
            pack(In, InOff, block, 0, 16);

            for (int i = 0; i < this.rounds; ++i)
            {
                block[3] = ROR((block[2] ^ roundKeys[i, 4]) + (block[3] ^ roundKeys[i, 5]), 3);
                block[2] = ROR((block[1] ^ roundKeys[i, 2]) + (block[2] ^ roundKeys[i, 3]), 5);
                block[1] = ROL((block[0] ^ roundKeys[i, 0]) + (block[1] ^ roundKeys[i, 1]), 9);
                ++i;

                block[0] = ROR((block[3] ^ roundKeys[i, 4]) + (block[0] ^ roundKeys[i, 5]), 3);
                block[3] = ROR((block[2] ^ roundKeys[i, 2]) + (block[3] ^ roundKeys[i, 3]), 5);
                block[2] = ROL((block[1] ^ roundKeys[i, 0]) + (block[2] ^ roundKeys[i, 1]), 9);

                ++i;
                block[1] = ROR((block[0] ^ roundKeys[i, 4]) + (block[1] ^ roundKeys[i, 5]), 3);
                block[0] = ROR((block[3] ^ roundKeys[i, 2]) + (block[0] ^ roundKeys[i, 3]), 5);
                block[3] = ROL((block[2] ^ roundKeys[i, 0]) + (block[3] ^ roundKeys[i, 1]), 9);

                ++i;
                block[2] = ROR((block[1] ^ roundKeys[i, 4]) + (block[2] ^ roundKeys[i, 5]), 3);
                block[1] = ROR((block[0] ^ roundKeys[i, 2]) + (block[1] ^ roundKeys[i, 3]), 5);
                block[0] = ROL((block[3] ^ roundKeys[i, 0]) + (block[0] ^ roundKeys[i, 1]), 9);
            }

            unpack(block, 0, Out, OutOff, 4);
            return BLOCKSIZE;
        }
        public int decryptionBlock(byte[] In, int InOff, byte[] Out, int OutOff)
        {
            pack(In, InOff, block, 0, 16);

            for (int i = this.rounds - 1; i >= 0; --i)
            {
                block[0] = (ROR(block[0], 9) - (block[3] ^ roundKeys[i, 0])) ^ roundKeys[i, 1];
                block[1] = (ROL(block[1], 5) - (block[0] ^ roundKeys[i, 2])) ^ roundKeys[i, 3];
                block[2] = (ROL(block[2], 3) - (block[1] ^ roundKeys[i, 4])) ^ roundKeys[i, 5];
                --i;

                block[3] = (ROR(block[3], 9) - (block[2] ^ roundKeys[i, 0])) ^ roundKeys[i, 1];
                block[0] = (ROL(block[0], 5) - (block[3] ^ roundKeys[i, 2])) ^ roundKeys[i, 3];
                block[1] = (ROL(block[1], 3) - (block[0] ^ roundKeys[i, 4])) ^ roundKeys[i, 5];
                --i;

                block[2] = (ROR(block[2], 9) - (block[1] ^ roundKeys[i, 0])) ^ roundKeys[i, 1];
                block[3] = (ROL(block[3], 5) - (block[2] ^ roundKeys[i, 2])) ^ roundKeys[i, 3];
                block[0] = (ROL(block[0], 3) - (block[3] ^ roundKeys[i, 4])) ^ roundKeys[i, 5];
                --i;

                block[1] = (ROR(block[1], 9) - (block[0] ^ roundKeys[i, 0])) ^ roundKeys[i, 1];
                block[2] = (ROL(block[2], 5) - (block[1] ^ roundKeys[i, 2])) ^ roundKeys[i, 3];
                block[3] = (ROL(block[3], 3) - (block[2] ^ roundKeys[i, 4])) ^ roundKeys[i, 5];
            }

            unpack(block, 0, Out, OutOff, 4);

            return BLOCKSIZE;
        }
        public void generateRoundKeys(byte[] mk)
        {
            if (mk == null || ((mk.Length != 16) && (mk.Length != 24) && (mk.Length != 32)))
            {
                throw new ArgumentException("Illegal Key");
            }

            int[] T = new int[8];

            this.rounds = (mk.Length >> 1) + 16;
            this.roundKeys = new int[this.rounds, 6];

            pack(mk, 0, T, 0, 16);
            if (mk.Length > 16)
            {
                pack(mk, 16, T, 4, 8);
            }
            if (mk.Length > 24)
            {
                pack(mk, 24, T, 6, 8);
            }

            if (mk.Length == 16)
            {
                for (int i = 0; i < 24; ++i)
                {
                    int temp = ROL((int)delta[i & 3], i);

                    this.roundKeys[i, 0] = T[0] = ROL(T[0] + ROL(temp, 0), 1);
                    this.roundKeys[i, 1] = this.roundKeys[i, 3] = this.roundKeys[i, 5] = T[1] = ROL(T[1] + ROL(temp, 1), 3);
                    this.roundKeys[i, 2] = T[2] = ROL(T[2] + ROL(temp, 2), 6);
                    this.roundKeys[i, 4] = T[3] = ROL(T[3] + ROL(temp, 3), 11);
                }

            }
            else if (mk.Length == 24)
            {
                for (int i = 0; i < 28; ++i)
                {
                    int temp = ROL((int)delta[i % 6], i);

                    this.roundKeys[i, 0] = T[0] = ROL(T[0] + ROL(temp, 0), 1);
                    this.roundKeys[i, 1] = T[1] = ROL(T[1] + ROL(temp, 1), 3);
                    this.roundKeys[i, 2] = T[2] = ROL(T[2] + ROL(temp, 2), 6);
                    this.roundKeys[i, 3] = T[3] = ROL(T[3] + ROL(temp, 3), 11);
                    this.roundKeys[i, 4] = T[4] = ROL(T[4] + ROL(temp, 4), 13);
                    this.roundKeys[i, 5] = T[5] = ROL(T[5] + ROL(temp, 5), 17);
                }
            }
            else
            {
                for (int i = 0; i < 32; ++i)
                {
                    int temp = ROL(delta[i % 8], i & 0x1f);

                    this.roundKeys[i, 0] = T[(6 * i + 0) % 8] = ROL(T[(6 * i + 0) % 8] + temp, 1);
                    this.roundKeys[i, 1] = T[(6 * i + 1) % 8] = ROL(T[(6 * i + 1) % 8] + ROL(temp, 1), 3);
                    this.roundKeys[i, 2] = T[(6 * i + 2) % 8] = ROL(T[(6 * i + 2) % 8] + ROL(temp, 2), 6);
                    this.roundKeys[i, 3] = T[(6 * i + 3) % 8] = ROL(T[(6 * i + 3) % 8] + ROL(temp, 3), 11);
                    this.roundKeys[i, 4] = T[(6 * i + 4) % 8] = ROL(T[(6 * i + 4) % 8] + ROL(temp, 4), 13);
                    this.roundKeys[i, 5] = T[(6 * i + 5) % 8] = ROL(T[(6 * i + 5) % 8] + ROL(temp, 5), 17);
                }
            }
        }

        public static int ROL(int state, int num)
        {
            return (int)((state << num) | ((int)((uint)state >>(32 - num))));
        }

        public static int ROR(int state, int num)
        {
            return ((int)((uint)state >> num)) | (state << (32 - num));
        }

    }
}
