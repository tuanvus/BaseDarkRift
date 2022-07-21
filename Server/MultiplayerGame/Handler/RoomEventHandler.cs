using DarkRift;
using DarkRift.Server;
using MultiplayerGame.Models;
using MultiplayerGameModels;
using System;
using System.Collections.Generic;

namespace MultiplayerGame.Handler
{
    public class RoomEventHandler
    {
        #region Create Room
        public void OnCreateRoomHandler(DarkRiftReader reader, MessageReceivedEventArgs e,
            Dictionary<int, User> clientIDtoPlayer, Dictionary<int, Room> roomIDtoRoom,int roomID)
        {
            CreateRoom createRoom = reader.ReadSerializable<CreateRoom>();
            Room room = new Room();
            room.name = createRoom.name;

            room.id = roomID;

            roomIDtoRoom.Add(room.id, room);

            PlayerJoinRoom(clientIDtoPlayer[e.Client.ID], room);

            foreach (var pair in clientIDtoPlayer)
            {
                Room userRoom = pair.Value.room;
                if (userRoom == null)
                {
                    CreateRoomSuccessfull createRoomSuccessfull = new CreateRoomSuccessfull();
                    createRoomSuccessfull.name = room.name;
                    createRoomSuccessfull.roomID = room.id;
                    Console.WriteLine("Create room");

                    OnCreateRoomSuccessful(createRoomSuccessfull, pair.Value.client);
                }
            }
        }
        private void PlayerJoinRoom(User user, Room room)
        {
            room.numberOfPlayer++;

            user.room = room;

            JoinRoomSuccessfull joinRoomSuccessfull = new JoinRoomSuccessfull();
            joinRoomSuccessfull.name = room.name;
            joinRoomSuccessfull.roomID = room.id;
            if (room.numberOfPlayer > 2)
            {
                joinRoomSuccessfull.state = (int)Tags.JoinRoomState.ROOM_IS_FULL;
            }
            else
            {
                joinRoomSuccessfull.state = (int)Tags.JoinRoomState.SUCCESS;
                room.users.Add(user);
            }
            OnJoinRoomSuccessful(joinRoomSuccessfull, user.client);
        }
        private void OnJoinRoomSuccessful(JoinRoomSuccessfull joinRoomSuccessfull, IClient client)
        {
            Console.WriteLine("JOIN_ROOM_SUCCES");

            using (Message message = Message.Create((ushort)Tags.Tag.JOIN_ROOM_SUCCES, joinRoomSuccessfull))
            {
                client.SendMessage(message, SendMode.Reliable);
            }
        }
        private void OnCreateRoomSuccessful(CreateRoomSuccessfull createRoomSuccessfull, IClient client)
        {
            Console.WriteLine("CREATE_ROOM_SUCCESS");
            using (Message message = Message.Create((ushort)Tags.Tag.CREATE_ROOM_SUCCESS, createRoomSuccessfull))
            {
                client.SendMessage(message, SendMode.Reliable);
            }
        }
        #endregion

        #region Join Room
        public void OnJoinRoomHandler(DarkRiftReader reader, MessageReceivedEventArgs e,
             Dictionary<int, User> clientIDtoPlayer, Dictionary<int, Room> roomIDtoRoom, int roomID)
        {
            JoinRoom joinRoom = reader.ReadSerializable<JoinRoom>();
            if (roomIDtoRoom.ContainsKey(joinRoom.roomID))
            {
                Room userRoom = roomIDtoRoom[joinRoom.roomID];
                PlayerJoinRoom(clientIDtoPlayer[e.Client.ID], userRoom);


            }
        }
        #endregion

        #region Room Data
        public void OnRoomDataHandler(DarkRiftReader reader, MessageReceivedEventArgs e, Dictionary<int, Room> roomIDtoRoom)
        {
            RoomData roomData = reader.ReadSerializable<RoomData>();
            if (roomIDtoRoom.ContainsKey(roomData.roomID))
            {
                Room userRoom = roomIDtoRoom[roomData.roomID];
                string userName1 = "";
                string userName2 = "";
                bool isRoomFull = false;
                if (userRoom.users.Count > 1)
                {
                    userName1 = userRoom.users[0].username;
                    userName2 = userRoom.users[1].username;
                    isRoomFull = true;
                }
                else
                {
                    userName1 = userRoom.users[0].username;

                }
                foreach (User user in userRoom.users)
                {
                    RoomDataSuccess roomDataSuccess = new RoomDataSuccess();
                    roomDataSuccess.player1Name = userName1;
                    roomDataSuccess.player2Name = userName2;
                    roomDataSuccess.isRoomFull = isRoomFull;

                    RoomDataSuccessful(roomDataSuccess, user.client);
                }
            }
        }
        private void RoomDataSuccessful(RoomDataSuccess m, IClient client)
        {
            Console.WriteLine("JOIN_ROOM_SUCCES");

            using (Message message = Message.Create((ushort)Tags.Tag.ROOM_DATA_SUCCES, m))
            {
                client.SendMessage(message, SendMode.Reliable);
            }
        }
        #endregion

    }
}
