#if !BESTHTTP_DISABLE_ALTERNATE_SSL && (!UNITY_WEBGL || UNITY_EDITOR)
#pragma warning disable
using System;
using System.Runtime.Serialization;

namespace Best.HTTP.SecureProtocol.Org.BouncyCastle.Tsp
{
    [Serializable]
    public class TspException
		: Exception
	{
		public TspException()
			: base()
		{
		}

		public TspException(string message)
			: base(message)
		{
		}

		public TspException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		protected TspException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
#pragma warning restore
#endif
