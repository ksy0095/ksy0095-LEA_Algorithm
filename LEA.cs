using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LEA.mode;
using static LEA.LEAEngine;
using static LEA.BlockCiper;
using ECBMode = LEA.mode.ECBMode;
using CBCMode = LEA.mode.CBCMode;

namespace LEA
{
    public class LEA
    {
        public LEA()
        {
            throw new Exception();
        }

        public static BlockCiper getEngine
        {
            get
            {
                return new LEAEngine();
            }
        }
        
        public partial class ECB : ECBMode
        {
            public ECB() : base(getEngine) { }
        }

        public partial class CBC : CBCMode
        {
            public CBC() : base(getEngine) { }
        }
    }
}
