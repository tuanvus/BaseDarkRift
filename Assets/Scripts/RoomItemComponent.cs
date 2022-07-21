using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomItemComponent : MonoBehaviour
{
    public int roomID;
    public string name;
    public void SetRoom(string _name,int _id)
    {
        name = _name;
        roomID = _id;
    }

    public void OnRoomClick()
    {
        GloballCallback.SetOnJoinRoom(roomID);
    }
}
