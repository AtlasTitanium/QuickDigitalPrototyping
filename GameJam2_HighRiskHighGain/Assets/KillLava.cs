using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillLava : MonoBehaviour
{
    private bool On = true;
    void Update()
    {
        if(On){
            transform.position = new Vector3(transform.position.x, transform.position.y + Time.deltaTime, transform.position.z);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.GetComponent<PlayerControllerSimple>()){
            On = false;
            other.transform.GetComponent<PlayerControllerSimple>().GameOver();
        }
    }
}
