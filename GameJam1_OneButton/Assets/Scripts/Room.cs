using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public Transform backwall;
    public LayerMask playerLayer;

    private bool going = false;
    private float t = 0;

    private float startPosRoom;
    private float startPosBackwall;
    private float speed;
    private bool hasPlayer;

    public void MoveRoom(float _speed){
        startPosRoom = transform.position.z;
        startPosBackwall = backwall.position.y;
        t = 0;
        speed = _speed;
        going = true;
        hasPlayer = Physics.CheckBox(transform.position, new Vector3(10,8,20), Quaternion.identity, playerLayer);
    }

    public void Update(){
        if(going){
            if(hasPlayer){
                backwall.position = new Vector3(backwall.position.x, Mathf.Lerp(startPosBackwall,10,t) ,backwall.position.z);
            }
            transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.Lerp(startPosRoom, startPosRoom-20, t));
            t += speed * Time.deltaTime;
            if(t >= 1.0f){
                backwall.position = new Vector3(backwall.position.x, startPosBackwall ,backwall.position.z);
                going = false;
            }
        }

        if(transform.position.z <= -19){
            going = false;
            Destroy(this.gameObject);
        }
    }
}
