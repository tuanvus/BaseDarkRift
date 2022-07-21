using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DarkRift;
using DarkRift.Server;
using MultiplayerGameModels;
using MultiplayerGame.Models;
using MultiplayerGame.Handler;

namespace MultiplayerGame
{
    public class MultiplayerGame : Plugin
    {
        public override bool ThreadSafe => false;
        public override Version Version => new Version(1, 0, 0);

        public Dictionary<int, User> clientIDtoPlayer = new Dictionary<int, User>();
        public Dictionary<int, Room> roomIDtoRoom = new Dictionary<int, Room>();
        public int roomID = 1;
        public int maxNumberOfPlayer = 2;
        RoomEventHandler roomEventHandler;
        UserEventHandler userEventHandler;
        LoginEventHandler loginEventHandler;

        public MultiplayerGame(PluginLoadData pluginLoadData) : base(pluginLoadData)
        {
            Console.WriteLine("we are here");
            ClientManager.ClientConnected += OnClientConnected;
            ClientManager.ClientDisconnected += OnClientDisConnected;
            roomEventHandler = new RoomEventHandler();
            userEventHandler = new UserEventHandler();
            loginEventHandler = new LoginEventHandler();
        }
        private void OnClientConnected(object sender, ClientConnectedEventArgs e)
        {
            Console.WriteLine("player connected");
            e.Client.MessageReceived += OnMessageReceived;
            if (!clientIDtoPlayer.ContainsKey(e.Client.ID))
            {
                User user = new User();
                user.client = e.Client;
                clientIDtoPlayer.Add(e.Client.ID, user);
            }

        }
        private void OnClientDisConnected(object sender, ClientDisconnectedEventArgs e)
        {
            Console.WriteLine("player DisConnected");
            if (clientIDtoPlayer.ContainsKey(e.Client.ID))
            {
                clientIDtoPlayer.Remove(e.Client.ID);
            }
        }

        private void OnMessageReceived(object sender, MessageReceivedEventArgs e)
        {

            using (Message message = e.GetMessage())
            {
                using (DarkRiftReader reader = message.GetReader())
                {
                    switch (message.Tag)
                    {
                        case (ushort)Tags.Tag.USER_LOGIN:
                            userEventHandler.OnUserLoginHandler(reader, e, clientIDtoPlayer);
                            break;
                        //ROOM
                        case (ushort)Tags.Tag.CREATE_ROOM:
                            roomEventHandler.OnCreateRoomHandler(reader, e, clientIDtoPlayer, roomIDtoRoom, roomID);
                            roomID++;
                            break;
                        case (ushort)Tags.Tag.JOIN_ROOM:
                            roomEventHandler.OnJoinRoomHandler(reader, e, clientIDtoPlayer, roomIDtoRoom, roomID);
                            break;
                        case (ushort)Tags.Tag.ROOM_DATA:
                            roomEventHandler.OnRoomDataHandler(reader, e, roomIDtoRoom);
                            break;
                    }

                }

            }




        }
    }
}
