using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class NPC_Controller : MonoBehaviour
{
    public int WaterLayer;
    public GameObject particleSystem;
    public GameObject TalkBubble;

    private Rigidbody rb;
    private int jumpStrenght;
    private float time;

    void Start(){
        rb = GetComponent<Rigidbody>();
        jumpStrenght = Random.Range(2,5);
        float time = Time.time;
        
        //choose what the character says
        int choice = Random.Range(0,3);
        switch(choice){
            case 0:
                TalkBubble.GetComponentInChildren<Text>().text = "Yaaaay Water!"; 
            break;
            case 1:
                TalkBubble.GetComponentInChildren<Text>().text = "Me First!"; 
            break;
            case 2:
                TalkBubble.GetComponentInChildren<Text>().text = "Yea this is fun!"; 
            break;
        }
    }
    void Update(){
        //Timer
        if(time + Random.Range(4,8) <= Time.time){
            //run this every 4 to 8 seconds
            rb.AddForce(transform.up * jumpStrenght, ForceMode.Impulse);
            time = Time.time;
        }

        //Make bubble always look at camera, and make it always hover over character;
        TalkBubble.transform.position = new Vector3(transform.position.x, transform.position.y + 2, transform.position.z);
        TalkBubble.transform.LookAt(2 * transform.position - Camera.main.transform.position);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == WaterLayer) {
            FuckingDIE();
        }
    }

    public void FuckingDIE(){
        rb.AddForce(-transform.forward * 10, ForceMode.Impulse);

        particleSystem.SetActive(true);
        Debug.LogError("They fucking DIED!");

        //choose what the character says
        int choice = Random.Range(0,3);
        switch(choice){
            case 0:
                TalkBubble.GetComponentInChildren<Text>().text = "Oh GOD!!!"; 
            break;
            case 1:
                TalkBubble.GetComponentInChildren<Text>().text = "AAAAAAAHHHHHHHH!!"; 
            break;
            case 2:
                TalkBubble.GetComponentInChildren<Text>().text = "IM TO YOUNG TO DIE!!"; 
            break;
        }
    }
}
