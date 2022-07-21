using MultiplayerGameModels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GloballCallback
{
    public static int roomID = -1;


    public delegate void OnCreateRoom(string name);
    public static OnCreateRoom Callback_OnCreateRoom;
    public static void SetOnCreateRoom(string name)
    {
        if(Callback_OnCreateRoom != null)
        {
            Callback_OnCreateRoom(name);
        }    
    }

    public delegate void OnCreateRoomSuccess(CreateRoomSuccessfull message);
    public static OnCreateRoomSuccess Callback_OnCreateRoomSuccess;
    public static void SetOnCreateRoomSuccess(CreateRoomSuccessfull message)
    {
        if (Callback_OnCreateRoomSuccess != null)
        {
            Callback_OnCreateRoomSuccess(message);
        }
    }

    //-------------------------------

    public delegate void OnJoinRoom(int id);
    public static OnJoinRoom Callback_OnJoinRoom;
    public static void SetOnJoinRoom(int id)
    {
        if (Callback_OnJoinRoom != null)
        {
            Callback_OnJoinRoom(id);
        }
    }

    public delegate void OnJoinRoomSuccess(JoinRoomSuccessfull message);
    public static OnJoinRoomSuccess Callback_OnJoinRoomSuccess;
    public static void SetOnJoinRoomSuccess(JoinRoomSuccessfull message)
    {
        if (Callback_OnJoinRoomSuccess != null)
        {
            Callback_OnJoinRoomSuccess(message);
        }
    }

    public delegate void RoomData(int id);
    public static RoomData Callback_OnRoomData;
    public static void SetOnRoomData(int id)
    {
        if (Callback_OnRoomData != null)
        {
            Callback_OnRoomData(id);
        }
    }
    public delegate void OnRoomDataSuccess(RoomDataSuccess message);
    public static OnRoomDataSuccess Callback_OnRoomDataSuccess;
    public static void SetOnRoomDataSuccess(RoomDataSuccess message)
    {
        if (Callback_OnRoomDataSuccess != null)
        {
            Callback_OnRoomDataSuccess(message);
        }
    }

}
