#if !BESTHTTP_DISABLE_ALTERNATE_SSL && (!UNITY_WEBGL || UNITY_EDITOR)
#pragma warning disable
using System;
using System.Collections;
using System.IO;

using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Bcpg
{
    /**
    * Basic type for a user attribute packet.
    */
    public class UserAttributePacket
        : ContainedPacket
    {
        private readonly UserAttributeSubpacket[] subpackets;

        public UserAttributePacket(
            BcpgInputStream bcpgIn)
        {
            UserAttributeSubpacketsParser sIn = new UserAttributeSubpacketsParser(bcpgIn);
            UserAttributeSubpacket sub;

            IList v = BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.Platform.CreateArrayList();
            while ((sub = sIn.ReadPacket()) != null)
            {
                v.Add(sub);
            }

            subpackets = new UserAttributeSubpacket[v.Count];

            for (int i = 0; i != subpackets.Length; i++)
            {
                subpackets[i] = (UserAttributeSubpacket)v[i];
            }
        }

        public UserAttributePacket(
            UserAttributeSubpacket[] subpackets)
        {
            this.subpackets = subpackets;
        }

        public UserAttributeSubpacket[] GetSubpackets()
        {
            return subpackets;
        }

        public override void Encode(
            BcpgOutputStream bcpgOut)
        {
            MemoryStream bOut = new MemoryStream();

            for (int i = 0; i != subpackets.Length; i++)
            {
                subpackets[i].Encode(bOut);
            }

            bcpgOut.WritePacket(PacketTag.UserAttribute, bOut.ToArray(), false);
        }
    }
}
#pragma warning restore
#endif
