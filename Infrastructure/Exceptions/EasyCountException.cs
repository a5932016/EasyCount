using System.Runtime.Serialization;

namespace Infrastructure.Exceptions
{
    public class EasyCountException : Exception
    {
        public virtual int? Code { get; set; }

        public EasyCountException()
        {
        }

        public EasyCountException(string message, byte? Code)
            : base(message)
        {
            this.Code = Code;
        }

        public EasyCountException(string message)
            : base(message)
        {
        }

        public EasyCountException(string message, Exception inner)
            : base(message, inner)
        {
        }

        public EasyCountException(ExceptionCode exceptionCode)
            : base(exceptionCode.ToString())
        {
            this.Code = (int)exceptionCode;
        }

        protected EasyCountException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public override string Message
        {
            get
            {
                if (this.Code == null)
                {
                    return base.Message;
                }
                else
                {
                    return string.Format("Code:{0}, Message:{1}", this.Code, base.Message);
                }
            }
        }
    }
}