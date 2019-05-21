using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatFormInit : MonoBehaviour
{   
    private GameObject player;
    private Renderer thisMat;
    public int visibleDist = 20;
    private int green = 0;
    void Start(){
        player = GameObject.Find("Player");
        thisMat = GetComponent<Renderer>();
    }
    void Update(){
        float dist = Vector3.Distance(player.transform.position, transform.position);
        Debug.Log(dist);

        if(dist <= visibleDist){
            thisMat.material.color = new Color(0,green,0, (visibleDist-dist)/visibleDist);
        }

        if(player.transform.position.y - 0.5f > this.transform.position.y){
            GetComponent<BoxCollider>().enabled = true;
            green = 255;
        } else{
            GetComponent<BoxCollider>().enabled = false;
            green = 0;
        }
    }
}
