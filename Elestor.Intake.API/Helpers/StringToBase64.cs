using System;
using System.Text;

namespace Elestor.Intake.API.Helpers
{
    public static class StringToBase64
    {
     
        public static string Encode(this string str)
        {
            //return System.Convert.FromBase64String(str);
            //return Encoding.UTF8.GetBytes(str);
            return System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(str));

        }
        
        //System.Text.Encoding.UTF8.GetString(System.Convert.FromBase64String(base64EncodedData));

        public static string Decode(this string str)
        {
            //return System.Convert.ToBase64String(bytes);
            //return Encoding.UTF8.GetString(bytes, 0, bytes.Length);

            return System.Text.Encoding.UTF8.GetString(System.Convert.FromBase64String(str));
        }
        
         //System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(plainTextBytes));
    }
}
