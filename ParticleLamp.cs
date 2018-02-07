using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace JWBParticleLamp
{

    public class ParticleLamp
    {
        const string StevesLampURL = "";
        public string DeviceID { get; set; }
        public string Token { get; set; }
        public string LastResponse { get; set; }

        public ParticleLamp() { }

        public async void Trigger(int eventid = 0)
        {
            await Task.Run(() =>
            {
                var url = GetDeviceURL() + "/Trigger";

                var postData = "args=" + eventid.ToString();
                var data = Encoding.ASCII.GetBytes(postData);


                var request = (HttpWebRequest)WebRequest.Create(url);

                request.Method = "POST";

                request.ContentType = "application/x-www-form-urlencoded";
                request.Accept = "application/json";
                request.Headers.Add("Authorization", "Bearer " + Token);
                request.ContentLength = data.Length;
                request.UserAgent = "Mozilla/4.0 (Windows 7 6.1) Java/1.6.0_26";
                request.Proxy = null;

                using (var stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }


                var response = request.GetResponse() as HttpWebResponse;

                LastResponse = new StreamReader(response.GetResponseStream()).ReadToEnd();
            });

        }



        private string GetDeviceURL()
        {
            return "https://api.Particle.io/v1/devices/" + DeviceID.ToString();

        }
    }
}
