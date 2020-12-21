using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Api
{
    public static class ApiUtilities
    {
        /// <summary>
        /// Used to build headers to make api calls.
        /// </summary>
        /// <param name="userKey"></param>
        /// <param name="apiKey"></param>
        /// <param name="privateKey"></param>
        /// <param name="path"></param>
        /// <param name="req"></param>
        /// <returns></returns>
        public static Dictionary<string, string> BuildHeaders(string userKey, string apiKey, string privateKey, string path, string req = "GET")
        {
            var headers = new Dictionary<string, string>();
            string unixTime = DateTime.Now.ToString();
            string[] signed_parts = {
                req,
                path,
                unixTime,
                apiKey,
                userKey
            };
            string data = String.Join("\r\n", signed_parts);
            string calculatedHash = computeHash(data, apiKey, privateKey);
            headers.Add("X-OS-Hash", calculatedHash);
            headers.Add("X-OS-ApiKey", apiKey);
            headers.Add("X-OS-UserKey", userKey);
            headers.Add("X-OS-Timestamp", unixTime);
            return headers;
        }

        /// <summary>
        /// Used in Buildheaders to make the hash.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="apiKey"></param>
        /// <param name="privateKey"></param>
        /// <returns></returns>
        private static string computeHash(string data, string apiKey, string privateKey)
        {
            HMACSHA1 sha1 = new HMACSHA1(Encoding.UTF8.GetBytes(privateKey));
            byte[] computedHash = sha1.ComputeHash(Encoding.UTF8.GetBytes(data));
            string hash = Convert.ToBase64String(computedHash);
            return hash;
        }

    }
}
