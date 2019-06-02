using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatFormInit : MonoBehaviour
{   
    public bool SuperJumpPad;
    public bool CheckPoint;
    public bool AutoJump;
    
    private GameObject player;
    private Renderer thisMat;
    [HideInInspector]
    public PlatformGenerator platformGenerator;
    [HideInInspector]
    public GameObject lava;
    private int visibleDist;
    private int green = 0;
    private bool once = true;
    void Start(){
        player = GameObject.Find("Player");
        visibleDist = Mathf.RoundToInt((player.GetComponent<PlayerControllerSimple>().jumpStrenght / 2) + 1);
        thisMat = GetComponent<Renderer>();
    }
    void Update(){
        float dist = Vector3.Distance(new Vector3(transform.position.x, player.transform.position.y, transform.position.z), transform.position);

        if(SuperJumpPad){
            if(dist <= visibleDist){
                thisMat.material.color = new Color(0,(255-dist)/255,0, (visibleDist-dist)/visibleDist);
            }
        } else if(AutoJump){
            if(dist <= visibleDist){
                thisMat.material.color = new Color((255-dist)/255,0,0, (visibleDist-dist)/visibleDist);
            }
        } else if(CheckPoint){
            if(dist <= visibleDist){
                thisMat.material.color = new Color((255-dist)/255,(255-dist)/255,(255-dist)/255, (visibleDist-dist)/visibleDist);
            }
        } else {
            if(dist <= visibleDist){
                thisMat.material.color = new Color((255-dist)/255,0,(255-dist)/255, (visibleDist-dist)/visibleDist);
            }
        }



        if(player.transform.position.y - 0.5f > this.transform.position.y){
            GetComponent<BoxCollider>().enabled = true;
            if(once){
                platformGenerator.CreatePlatform();
                once = false;
            }
        } else{
            GetComponent<BoxCollider>().enabled = false;
        }

        if(lava.transform.position.y > transform.position.y){
            Destroy(this.gameObject);
        }
    }
}
