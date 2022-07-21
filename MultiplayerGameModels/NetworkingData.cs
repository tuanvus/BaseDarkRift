using DarkRift;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiplayerGameModels
{
    //---------------User-----------------------
    public class Chat : IDarkRiftSerializable
    {
        public string chatMessage;
        public void Deserialize(DeserializeEvent e)
        {
            chatMessage = e.Reader.ReadString();

        }

        // Serialize will be called when the object is written to a writer or message
        public void Serialize(SerializeEvent e)
        {
            e.Writer.Write(chatMessage);

        }
    }
    public class UserLogin : IDarkRiftSerializable
    {
        public string username;
        public void Deserialize(DeserializeEvent e)
        {
            username = e.Reader.ReadString();

        }

        // Serialize will be called when the object is written to a writer or message
        public void Serialize(SerializeEvent e)
        {
            e.Writer.Write(username);

        }
    }
    public class UserLoginSuccessfull : IDarkRiftSerializable
    {
        public void Deserialize(DeserializeEvent e)
        {

        }

        // Serialize will be called when the object is written to a writer or message
        public void Serialize(SerializeEvent e)
        {

        }
    }

    //---------------Room-----------------------

    public class CreateRoom : IDarkRiftSerializable
    {
        public string name;

        public void Deserialize(DeserializeEvent e)
        {
            name = e.Reader.ReadString();

        }

        // Serialize will be called when the object is written to a writer or message
        public void Serialize(SerializeEvent e)
        {
            e.Writer.Write(name);

        }
    }
    public class CreateRoomSuccessfull : IDarkRiftSerializable
    {
        public string name;
        public int roomID;

        public void Deserialize(DeserializeEvent e)
        {
            name = e.Reader.ReadString();
            roomID = e.Reader.ReadInt32();

        }

        // Serialize will be called when the object is written to a writer or message
        public void Serialize(SerializeEvent e)
        {
            e.Writer.Write(name);
            e.Writer.Write(roomID);

        }
    }

    public class JoinRoomSuccessfull : IDarkRiftSerializable
    {
        public string name;
        public int roomID;
        public int state;

        public void Deserialize(DeserializeEvent e)
        {
            name = e.Reader.ReadString();
            roomID = e.Reader.ReadInt32();
            state = e.Reader.ReadInt32();
        }

        // Serialize will be called when the object is written to a writer or message
        public void Serialize(SerializeEvent e)
        {
            e.Writer.Write(name);
            e.Writer.Write(roomID);
            e.Writer.Write(state);
        }
    }

    public class JoinRoom : IDarkRiftSerializable
    {
        public int roomID;


        public void Deserialize(DeserializeEvent e)
        {
            roomID = e.Reader.ReadInt32();

        }

        // Serialize will be called when the object is written to a writer or message
        public void Serialize(SerializeEvent e)
        {
            e.Writer.Write(roomID);

        }
    }
    public class RoomData : IDarkRiftSerializable
    {
        public int roomID;


        public void Deserialize(DeserializeEvent e)
        {
            roomID = e.Reader.ReadInt32();
        }
        public void Serialize(SerializeEvent e)
        {
            e.Writer.Write(roomID);

        }
    }
    public class RoomDataSuccess : IDarkRiftSerializable
    {
        public string player1Name;
        public string player2Name;
        public bool isRoomFull;


        public void Deserialize(DeserializeEvent e)
        {
            player1Name = e.Reader.ReadString();
            player2Name = e.Reader.ReadString();
            isRoomFull = e.Reader.ReadBoolean();
        }

        // Serialize will be called when the object is written to a writer or message
        public void Serialize(SerializeEvent e)
        {
            e.Writer.Write(player1Name);
            e.Writer.Write(player2Name);
            e.Writer.Write(isRoomFull);

        }
    }
}
