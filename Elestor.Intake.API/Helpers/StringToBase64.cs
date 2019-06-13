using System;
using System.Text;

namespace Elestor.Intake.API.Helpers
{
    public static class StringToBase64
    {
     
        public static byte[] GetBytes(this string str)
        {
           return Encoding.ASCII.GetBytes(str); ;
        }

        public static string GetString(this byte[] bytes)
        {
            return Encoding.ASCII.GetString(bytes, 0, bytes.Length);
        }
    }
}
