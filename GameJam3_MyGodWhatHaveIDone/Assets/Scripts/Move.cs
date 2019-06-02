using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public AudioSource EvilSound;
    public AudioSource GoodSound;
    
    private bool evilOn = false;
    private float time;
    private float volume;

    void Start(){
        volume = GoodSound.volume;
    }

    void Update(){
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + Time.deltaTime * 5);
        if(evilOn){
            if(time + 1 <= Time.time){
                EvilSound.volume = 0;
                GoodSound.volume = volume;
                evilOn = false;
            }
        }
    }

    public void IfError(){
        GoodSound.volume = 0;
        EvilSound.volume = 0.5f;

        evilOn = true;
        time = Time.time;
    }

    public void No(){
        GoodSound.volume = 0;
        EvilSound.volume = 0.8f;
    }
}
