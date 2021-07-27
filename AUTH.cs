using System;
using System.IO;
using System.Net;

namespace TrickeryAUTH
{
    public class AUTH
    {
        private static string Parse(string source, string left, string right) => source.Split(new string[1] { left }, StringSplitOptions.None)[1].Split(new string[1] { right }, StringSplitOptions.None)[0];
        private static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }
        private static string AuthResponse = "";
        public static bool Login(string AuthKey)
        {
            var request = (HttpWebRequest)WebRequest.Create("https://trickery.to/forum/tapi/index.php?users/me&oauth_token=" + AuthKey);
            var response = (HttpWebResponse)request.GetResponse();
            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            AuthResponse = responseString;
            if (AuthResponse.Contains("user"))
                return true;
            return false;
        }
        public static string GetRawJSON(string AuthKey)
        {
            if (Login(AuthKey))
                return AuthResponse;
            else
                return "Invalid Key";
        }
        public static string GetUsername(string AuthKey)
        {
            if (Login(AuthKey))
                return Parse(AuthResponse, "\"username\": \"", "\",");
            else
                return "Invalid Key";
        }
        public static string GetUserID(string AuthKey)
        {
            if (Login(AuthKey))
                return Parse(AuthResponse, "\"user_id\": ", ",");
            else
                return "Invalid Key";

        }
        public static string GetUserEmail(string AuthKey)
        {
            if (Login(AuthKey))

                return Parse(AuthResponse, "\"user_email\": \"", "\",");
            else
                return "Invalid Key";

        }
        public static string GetRegistrationDate(string AuthKey)
        {
            if (Login(AuthKey))
                return (UnixTimeStampToDateTime(double.Parse(Parse(AuthResponse, "\"user_register_date\": ", ",")))).ToString("dd/MM/yyyy");
            else
                return "Invalid Key";
        }
    }

}
