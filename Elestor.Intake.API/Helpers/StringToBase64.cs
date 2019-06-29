using System;
using System.Text;

namespace Elestor.Intake.API.Helpers
{
    public static class StringToBase64
    {
     
        public static byte[] GetBytes(this string str)
        {
            try
            {
                return System.Convert.FromBase64String(str);
            }
            catch(Exception ex)
            {
                return null;
            }
            
            //return Encoding.UTF8.GetBytes(str);
       
        }
        
        //System.Text.Encoding.UTF8.GetString(System.Convert.FromBase64String(base64EncodedData));

        public static string GetString(this byte[] bytes)
        {
            try
            {
                return System.Convert.ToBase64String(bytes);
            }
            catch(Exception ex)
            {
                return null;
            }
          
        }

        public static string Encode(this string plainTextBytes)
        {
            return System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(plainTextBytes));
        }

        public  static string Decode(this string base64EncodedData)
        {
            return System.Text.Encoding.UTF8.GetString(System.Convert.FromBase64String(base64EncodedData));
        }

    }
}
