using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRooms : MonoBehaviour
{
    public Transform[] rooms;

    public void MoveRoomsBack(){
        for(int i = 0; i < rooms.Length; i++){
            if(rooms[i].GetComponent<Room>()){
                rooms[i].GetComponent<Room>().MoveRoom(0.5f);
            } else{
                Debug.LogError("Not all rooms have the Room Component");
            }
        }
    }
}
