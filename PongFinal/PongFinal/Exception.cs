using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongFinal
{
    public class PongException: ApplicationException
    {
        public PongException() { }

        public PongException(string message) : base(message) { }

        public PongException(string message, System.Exception inner): base(message, inner) { } 

        protected PongException (System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base (info, context) { }
}
}
