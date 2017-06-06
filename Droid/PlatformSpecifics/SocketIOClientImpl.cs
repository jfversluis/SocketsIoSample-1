using System;
using Quobject.SocketIoClientDotNet.Client;
using SocketsIoSample.Droid.PlatformSpecifics;
using SocketsIoSample.Interfaces;
using Xamarin.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SocketsIoSample.Models;
using System.Collections.ObjectModel;

[assembly: Dependency(typeof(SocketIOClientImpl))]

namespace SocketsIoSample.Droid.PlatformSpecifics
{
    public class SocketIOClientImpl : ISocketIOClient
    {
        private Socket _socket;
        public ObservableCollection<ChatOutput> _ChatList = new ObservableCollection<ChatOutput>();

        public void Init(string address, string ns = "/")
        {
            _socket = new Socket(new Manager(new Uri(address)), ns);

            _socket.Connect();
            _socket.Emit("subscribe", "dtrick01@gmail.com"); // sub to channel usually username for now.

            _socket.On("output", data =>
            {
                var jobject = data as JToken;
                
                foreach (var x in jobject)
                {
                    string name = x.Value<string>("name");
                    string message = x.Value<string>("message");
                    string OutputChat = name + ": " + message;

                    //not sure if this is right not sure how to call it in the SocketsIoSamplePage backendcode
                    _ChatList.Add(new ChatOutput
                    {
                        name = name,
                        message = message
                    });

                    Console.WriteLine(OutputChat);
                    
                    MessagingCenter.Send<object, string>(this, "UpdateChat", OutputChat);
                }

            });

        }

        public void SendMessage(string message)
        {
            if (_socket == null)
                throw new Exception("Call Init first");

            var json = JObject.Parse(message); // Parses to an Object

            _socket.Emit("input", json);
        }


    }
}