#if !BESTHTTP_DISABLE_ALTERNATE_SSL && (!UNITY_WEBGL || UNITY_EDITOR)
#pragma warning disable
using System;
using Best.HTTP.SecureProtocol.Org.BouncyCastle.Crypto;

namespace Best.HTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters
{
    /**
     * parameters for using an integrated cipher in stream mode.
     */
    public class IesParameters : ICipherParameters
    {
        private byte[]  derivation;
        private byte[]  encoding;
        private int     macKeySize;

        /**
         * @param derivation the derivation parameter for the KDF function.
         * @param encoding the encoding parameter for the KDF function.
         * @param macKeySize the size of the MAC key (in bits).
         */
        public IesParameters(
            byte[]  derivation,
            byte[]  encoding,
            int     macKeySize)
        {
            this.derivation = derivation;
            this.encoding = encoding;
            this.macKeySize = macKeySize;
        }

        public byte[] GetDerivationV()
        {
            return derivation;
        }

        public byte[] GetEncodingV()
        {
            return encoding;
        }

        public int MacKeySize
        {
			get
			{
				return macKeySize;
			}
        }
    }

}
#pragma warning restore
#endif
