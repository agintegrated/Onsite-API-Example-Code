using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

namespace Api
{
    public static class ApiUtilities
    {
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

        private static string computeHash(string data, string apiKey, string privateKey)
        {
            HMACSHA1 sha1 = new HMACSHA1(Encoding.UTF8.GetBytes(privateKey));
            byte[] computedHash = sha1.ComputeHash(Encoding.UTF8.GetBytes(data));
            string hash = Convert.ToBase64String(computedHash);
            return hash;
        }

        public static byte[] ReadAllBytes(string fileName)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            List<string> embeddedResources = assembly.GetManifestResourceNames().ToList();

            string resourceName = embeddedResources.Find(a => a.Contains(fileName));
            if (String.IsNullOrWhiteSpace(resourceName))
            {
                throw new Exception($"Unable to locate the embedded file '{fileName}'");
            }

            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (Stream sourceStream = assembly.GetManifestResourceStream(resourceName))
                {
                    sourceStream.CopyTo(memoryStream);
                    return memoryStream.ToArray();
                }
            }
        }

    }
}
