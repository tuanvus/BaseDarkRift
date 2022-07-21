using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DarkRift.Client.Unity;
using DarkRift;
using MultiplayerGameModels;
using DarkRift.Client;
using UnityEngine.SceneManagement;
using System;
using Random = UnityEngine.Random;

public class NetworkingManger : MonoBehaviour
{

    public UnityClient client;


    private void Awake()
    {
        client.MessageReceived += OnMessageReceived;

        GloballCallback.Callback_OnCreateRoom += Callback_OnCreateRoom;
        GloballCallback.Callback_OnJoinRoom += Callback_OnJoinRoomClick;
        GloballCallback.Callback_OnRoomData += Callback_OnRoomDataClick;


        DontDestroyOnLoad(this.gameObject);
    }

   

    public void OnDestroy()
    {

        client.MessageReceived -= OnMessageReceived;
        GloballCallback.Callback_OnCreateRoom -= Callback_OnCreateRoom;
        GloballCallback.Callback_OnJoinRoom -= Callback_OnJoinRoomClick;
        GloballCallback.Callback_OnRoomData -= Callback_OnRoomDataClick;


    }

    private void Callback_OnCreateRoom(string name)
    {
        CreateRoom createRoom = new CreateRoom();
        createRoom.name = name;
        using (Message message = Message.Create((ushort)Tags.Tag.CREATE_ROOM, createRoom))
        {
            client.SendMessage(message, SendMode.Reliable);
        }
    }
    public void Callback_OnJoinRoomClick(int roomID)
    {
        JoinRoom joinRoom = new JoinRoom();
        joinRoom.roomID = roomID;
        Debug.Log("id room join " + roomID);
        using (Message message = Message.Create((ushort)Tags.Tag.JOIN_ROOM, joinRoom))
        {
            client.SendMessage(message, SendMode.Reliable);
        }

    }
    private void Callback_OnRoomDataClick(int roomID)
    {
        RoomData roomData = new RoomData();
        roomData.roomID = roomID;
        Debug.Log("id room join " + roomID);
        using (Message message = Message.Create((ushort)Tags.Tag.ROOM_DATA, roomData))
        {
            client.SendMessage(message, SendMode.Reliable);
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
                    case (ushort)Tags.Tag.USER_LOGIN_SUCCESSFULL:
                        Debug.Log("USER_LOGIN_SUCCESSFULL");
                        SceneManager.LoadScene("Lobby");
                        break;
                    case (ushort)Tags.Tag.CREATE_ROOM_SUCCESS:
                        Debug.Log("CREATE_ROOM_SUCCESS");

                        CreateRoomSuccessfull createRoomSuccessfull = reader.ReadSerializable<CreateRoomSuccessfull>();
                        
                        GloballCallback.SetOnCreateRoomSuccess(createRoomSuccessfull);
                        Debug.Log(" room " + createRoomSuccessfull.name + " id =" + createRoomSuccessfull.roomID);
                        break;
                    case (ushort)Tags.Tag.JOIN_ROOM_SUCCES:

                        JoinRoomSuccessfull joinRoomSuccessfull = reader.ReadSerializable<JoinRoomSuccessfull>();

                        if(joinRoomSuccessfull.state == (int)Tags.JoinRoomState.SUCCESS)
                        {
                            Debug.Log("JOIN_ROOM_SUCCES");
                            GloballCallback.SetOnJoinRoomSuccess(joinRoomSuccessfull);
                            Debug.Log(" Joinroom " + joinRoomSuccessfull.name + " id =" + joinRoomSuccessfull.roomID);

                        }
                        else
                        {
                            Debug.Log("ROOM_IS_FULL");

                        }

                        break;
                    case (ushort)Tags.Tag.ROOM_DATA_SUCCES:
                        Debug.Log("ROOM_DATA_SUCCES");

                        RoomDataSuccess roomDataSuccess = reader.ReadSerializable<RoomDataSuccess>();

                        GloballCallback.SetOnRoomDataSuccess(roomDataSuccess);
                        break;
                }
            }
        }
    }



    public void OnClickTestButton()
    {
        Chat chat = new Chat();
        chat.chatMessage = "text text new way";


        using (Message message = Message.Create((ushort)Tags.Tag.TEST_MESSAGE_2, chat))
        {
            client.SendMessage(message, SendMode.Reliable);
        }

    }
    public void OnLoginCLick()
    {
        UserLogin userLogin = new UserLogin();
        int i = Random.Range(1, 100);
        userLogin.username = "test_" +i;
        using (Message message = Message.Create((ushort)Tags.Tag.USER_LOGIN, userLogin))
        {
            client.SendMessage(message, SendMode.Reliable);
        }

    }
  
}
