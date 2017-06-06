using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;

namespace SocketsIoSample.Droid
{
    public class Input
    {
        public string room { get; set; }
        public string name { get; set; }
        public string message { get; set; }

        public Input()
        {
        }

        public string ToJsonString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        public static Input Deserialize(string jsonString)
        {
            return JsonConvert.DeserializeObject<Input>(jsonString);
        }

    }
}