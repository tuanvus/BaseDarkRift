using DarkRift;
using DarkRift.Server;
using MultiplayerGame.Models;
using MultiplayerGameModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiplayerGame.Handler
{
    public class UserEventHandler
    {
        public void OnUserLoginHandler(DarkRiftReader reader, MessageReceivedEventArgs e, Dictionary<int, User> clientIDtoPlayer)
        {
            UserLogin userLogin = reader.ReadSerializable<UserLogin>();
            if (clientIDtoPlayer.ContainsKey(e.Client.ID))
            {
                User users = clientIDtoPlayer[e.Client.ID];
                users.username = userLogin.username;

            }
            Console.WriteLine("==================");

            foreach (var pair in clientIDtoPlayer)
            {
                Console.WriteLine(pair.Value.username);
            }
            Console.WriteLine("==================");
            OnUserLoginSuccessful(e.Client);
        }


        private void OnUserLoginSuccessful(IClient client)
        {
            UserLoginSuccessfull userLoginSuccessfull = new UserLoginSuccessfull();

            using (Message message = Message.Create((ushort)Tags.Tag.USER_LOGIN_SUCCESSFULL, userLoginSuccessfull))
            {
                client.SendMessage(message, SendMode.Reliable);
            }
        }

    }
}
