using System;
using System.Text;

namespace Elestor.Intake.API.Helpers
{
    public static class StringToBase64
    {
     
        public static byte[] GetBytes(this string str)
        {
            return System.Convert.FromBase64String(str);
            //return Encoding.ASCII.GetBytes(str); ;
        }
        
        //System.Text.Encoding.UTF8.GetString(System.Convert.FromBase64String(base64EncodedData));

        public static string GetString(this byte[] bytes)
        {
            return System.Convert.ToBase64String(bytes);
            //return Encoding.ASCII.GetString(bytes, 0, bytes.Length);
        }
        
         //System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(plainTextBytes));
    }
}
