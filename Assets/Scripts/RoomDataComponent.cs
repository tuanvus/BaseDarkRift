using MultiplayerGameModels;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomDataComponent : MonoBehaviour
{

    public Button startGame;
    private void Start()
    {
        GloballCallback.SetOnRoomData(GloballCallback.roomID);

    }
    void Awake()
    {
        GloballCallback.Callback_OnRoomDataSuccess += Callback_OnRoomDataSuccess;

    }

    private void Callback_OnRoomDataSuccess(RoomDataSuccess message)
    {
        Debug.Log("name1 :" + message.player1Name);
        Debug.Log("name2 :" + message.player2Name);
        if(message.isRoomFull)
        {
            startGame.interactable = true;
        }
    }

    private void OnDestroy()
    {
        GloballCallback.Callback_OnRoomDataSuccess -= Callback_OnRoomDataSuccess;

    }

  
}
