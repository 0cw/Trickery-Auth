using Leaf.xNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TrickeryAUTH
{
    public class AUTH
    {

        //poopcode
        public static bool Login(string AuthKey)
        {
            using (var request = new HttpRequest())
            {
                bool response;
                string get = request.Get("https://trickery.to/forum/tapi/index.php?users/me&oauth_token=" + AuthKey).ToString();
                if (get.Contains("user"))
                {
                    response = true;
                }
                return response;
            }
        }

        public static string GetRawJSON(string AuthKey)
        {
            if (Login(AuthKey))
            {
                using (var request = new HttpRequest())
                {
                    string get = request.Get("https://trickery.to/forum/tapi/index.php?users/me&oauth_token=" + AuthKey).ToString();
                    return get;
                }
            }
            else
            {
                return "Invalid Key";
            }
        }

        public static string GetUsername(string AuthKey)
        {
            if (Login(AuthKey))
            {
                using (var request = new HttpRequest())
                {
                    string get = request.Get("https://trickery.to/forum/tapi/index.php?users/me&oauth_token=" + AuthKey).ToString();
                    string Username = Parse(get, "\"username\": \"", "\",");
                    return Username;
                }
            }
            else
            {
                return "Invalid Key";
            }
        }

        public static string GetUserID(string AuthKey)
        {
            if (Login(AuthKey))
            {
                using (var request = new HttpRequest())
                {
                    string get = request.Get("https://trickery.to/forum/tapi/index.php?users/me&oauth_token=" + AuthKey).ToString();
                    string UID = Parse(get, "\"user_id\": ", ",");
                    return UID;
                }
            }
            else
            {
                return "Invalid Key";
            }
        }

        public static string GetUserEmail(string AuthKey)
        {
            if (Login(AuthKey))
            {
                using (var request = new HttpRequest())
                {
                    string get = request.Get("https://trickery.to/forum/tapi/index.php?users/me&oauth_token=" + AuthKey).ToString();
                    string UID = Parse(get, "\"user_email\": \"", "\",");
                    return UID;
                }
            }
            else
            {
                return "Invalid Key";
            }
        }
        public static string GetRegistrationDate(string AuthKey)
        {
            if (Login(AuthKey))
            {
                using (var request = new HttpRequest())
                {
                    string get = request.Get("https://trickery.to/forum/tapi/index.php?users/me&oauth_token=" + AuthKey).ToString();
                    string timestampstring = Parse(get, "\"user_register_date\": ", ",");
                    double timestamp = double.Parse(timestampstring);
                    DateTime time = UnixTimeStampToDateTime(timestamp);
                    string time2 = time.ToString("dd/MM/yyyy");
                    return time2;
                }
            }
            else
            {
                return "Invalid Key";
            }
        }


        private static string Parse(string source, string left, string right) => source.Split(new string[1]
        {
                      left
        }, StringSplitOptions.None)[1].Split(new string[1]
        {
                      right
        }, StringSplitOptions.None)[0];

        public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }


    }
}
