#if !BESTHTTP_DISABLE_ALTERNATE_SSL && (!UNITY_WEBGL || UNITY_EDITOR)
#pragma warning disable
using System;

using Best.HTTP.SecureProtocol.Org.BouncyCastle.Math;

namespace Best.HTTP.SecureProtocol.Org.BouncyCastle.Tls.Crypto
{
    /// <summary>Base interface for a generator for SRP-6 verifiers.</summary>
    public interface TlsSrp6VerifierGenerator
    {
        /// <summary>Creates a new SRP-6 verifier value.</summary>
        /// <param name="salt">The salt to use, generally should be large and random</param>
        /// <param name="identity">The user's identifying information (eg. username)</param>
        /// <param name="password">The user's password</param>
        /// <returns>A new verifier for use in future SRP authentication</returns>
        BigInteger GenerateVerifier(byte[] salt, byte[] identity, byte[] password);
    }
}
#pragma warning restore
#endif
