using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LEA
{
    public abstract class Padding
    {
        public int blocksize;

        public Padding(int blocksize)
        {
            this.blocksize = blocksize;
        }
        /**
         * 배열 인덱스 채우기
         *
         * @param in
         *            패딩을 추가할 메시지, 길이가 블록사이즈보다 같거나 작아야 함
         */
        //public abstract void Fill<T>(T[] In, T code, int InOff, int len);

        /**
         * 패딩 추가
         * 
         * @param in
         *            패딩을 추가할 메시지, 길이가 블록사이즈보다 같거나 작아야 함
         */
        public abstract byte[] pad(byte[] In);

        /**
         * 패딩 추가
         * 
         * @param In
         *            패딩을 추가할 메시지가 포함된 배열, 배열 전체의 길이는 블록암호 블록사이즈와 같아야 함
         * @param len
         *            메시지 길이
         */
        public abstract byte[] pad(byte[] In, int len);

        /**
         * 패딩 제거
         * 
         * @param in
         *            패딩을 제거할 메시지
         * @return 패딩이 제거된 메시지
         */
        public abstract byte[] unpad(byte[] In);

        /**
         * 패딩 길이 계산
         * 
         * @param in
         *            패딩이 포함된 메시지
         * @return 패딩의 길이
         */
        //public abstract int getPadCount(byte[] In);


        //public abstract byte[] ArrFill(byte[] In, byte data, int InOff);


        //    public abstract void padWithLen(byte[] In, int InOff, int len);

        //    public abstract int unpad(byte[] In, int InOff, int len);

        //    public abstract int padLength(int len);
        //}


        //public abstract byte[] unpad(byte[] padByteArr);

        //public abstract byte[] pad(byte[] data, int len);

        public abstract T[] SubArr<T>(T[] arr, int start, int len);
    }
    
}
