using System;
using System.Diagnostics.CodeAnalysis;

namespace Edrs.ActionListenerService.Models.Helpers
{
    [ExcludeFromCodeCoverage]
    public static class GuidHelper
    {
        /// <summary>
        /// Encodes a GUID into a Base64 string.
        /// </summary>
        /// <param name="guid">The GUID to encode.</param>
        /// <returns>A Base64 encoded string representation of the GUID.</returns>
        public static string EncodeGuidToBase64(Guid guid)
        {
            return Convert.ToBase64String(guid.ToByteArray());
        }

        /// <summary>
        /// Decodes a Base64 string back into a GUID.
        /// </summary>
        /// <param name="base64String">The Base64 string to decode.</param>
        /// <returns>The decoded GUID.</returns>
        public static Guid DecodeBase64ToGuid(string base64String)
        {
            return new Guid(Convert.FromBase64String(base64String));
        }
    }
}
