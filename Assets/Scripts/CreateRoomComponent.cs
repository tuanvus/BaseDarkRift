using MultiplayerGameModels;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreateRoomComponent : MonoBehaviour
{
    public RoomItemComponent roomItemComponent;


    private void Awake()
    {
#if UNITY_EDITOR
        Application.runInBackground = true;
#endif
        GloballCallback.Callback_OnCreateRoomSuccess += Callback_OnCreateRoomSuccess;
        GloballCallback.Callback_OnJoinRoomSuccess += Callback_OnJoinRoomSuccess;

    }
    public void OnDestroy()
    {
        GloballCallback.Callback_OnCreateRoomSuccess -= Callback_OnCreateRoomSuccess;
        GloballCallback.Callback_OnJoinRoomSuccess -= Callback_OnJoinRoomSuccess;


    }

    private void Callback_OnJoinRoomSuccess(JoinRoomSuccessfull message)
    {
        GloballCallback.roomID = message.roomID;
        SceneManager.LoadScene("Game");
    }

    private void Callback_OnCreateRoomSuccess(CreateRoomSuccessfull message)
    {
        CreateRoom(message);
    }

  
    public void CreateRoomOnClick()
    {
        GloballCallback.SetOnCreateRoom("test");
    }
    public void CreateRoom(CreateRoomSuccessfull message)
    {
        Debug.Log("Create rooom : "+message.name);
        
        roomItemComponent.SetRoom(message.name, message.roomID);
    }    
}
