using System;
using Quobject.SocketIoClientDotNet.Client;
using SocketsIoSample.Interfaces;
using SocketsIoSample.iOS.PlatformSpecifics;
using Xamarin.Forms;

[assembly: Dependency(typeof(SocketIOClientImpl))]

namespace SocketsIoSample.iOS.PlatformSpecifics
{
    public class SocketIOClientImpl : ISocketIOClient
    {
        private Socket _socket;

        /// <summary>
        /// Creates the socket.
        /// </summary>
        /// <param name="address">Address of the server.</param>
        /// <param name="ns">Namespace</param>
        public void Init(string address, string ns = "/")
        {
            _socket = new Socket(new Manager(new Uri(address)), ns);

            _socket.Connect();
        }

        /// <summary>
        /// Send a message to the DoSomething endpoint
        /// </summary>
        /// <param name="message">The message to send</param>
		public void DoSomething(string message)
		{
            if (_socket == null)
                throw new Exception("Call Init first");

            _socket.Emit("DoSomething", new [] { message });
		}
    }
}