using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlWatergun : MonoBehaviour
{
    public GameObject Gun;
    public LayerMask MouseLayer;

    private void Update(){
        Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast (ray, out hit, 10, MouseLayer)) 
        {
            Gun.transform.LookAt(hit.point, Vector3.right);                
        }    
    }
}
