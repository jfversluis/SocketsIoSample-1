using System;
using SocketsIoSample.Interfaces;
using Xamarin.Forms;
using SocketsIoSample.Models;
using System.Collections.ObjectModel;
using Newtonsoft.Json;

namespace SocketsIoSample
{
    public partial class SocketsIoSamplePage : ContentPage
    {
        private readonly ISocketIOClient _socketClient;
        string roomname = "dtrick01@gmail.com";
        string ChatName = "dtrick01@gmail.com";
        string ChatText;

        public SocketsIoSamplePage()
        {
            InitializeComponent();

            _socketClient = DependencyService.Get<ISocketIOClient>();

            if (_socketClient == null)
                throw new Exception("Something went wrong, forgot to register dependency?");

            _socketClient.Init("http://facilichat.com:8088");


            //call the incoming chat in here or do i replace it with just a static itemsource of the Chatoutput?
            //var _ChatList = new ObservableCollection<ChatOutput>();
            //Listview_ChatWindow.ItemsSource = _ChatList;

            MessagingCenter.Subscribe<object, string>(this, "UpdateChat", (s, e) =>
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    Listview_ChatWindow.ItemsSource = e;
                });
            });


            Entry_ChatText.Completed += (s, e) => SendMessage_Clicked(s, e);
        }

        void SendMessage_Clicked(object sender, EventArgs e)
        {
            ChatText = Entry_ChatText.Text;

            ChatInput cInput = new ChatInput();
            cInput.room = roomname;
            cInput.name = ChatName;
            cInput.message = ChatText;

            if (Entry_ChatText.Text == null)
            {
                //alert soon
            }
            else
            {
                var obj = JsonConvert.SerializeObject(cInput, Formatting.Indented);
                _socketClient.SendMessage(obj);
            }


            Entry_ChatText.Text = ""; //clears value for new message.
        }
    }
}