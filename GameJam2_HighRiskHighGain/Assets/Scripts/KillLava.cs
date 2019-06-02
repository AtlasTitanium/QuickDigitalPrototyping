using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillLava : MonoBehaviour
{
    public int dangerzoneDistance = 20;
    public int speedUpPercent = 5;
    private bool On = true;
    private float timeScaler;
    private GameObject player;

    void Start(){
        player = GameObject.Find("Player");
    }
    void Update()
    {
        timeScaler = Time.deltaTime;
        if(player.transform.position.y > this.transform.position.y + dangerzoneDistance){
            timeScaler += Time.deltaTime * speedUpPercent;
        }
        if(On){
            transform.position = new Vector3(transform.position.x, transform.position.y + timeScaler, transform.position.z);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.GetComponent<PlayerControllerSimple>()){
            On = false;
            other.transform.GetComponent<PlayerControllerSimple>().GameOver();
        } else {
            Destroy(other.gameObject);
        }
    }
}
