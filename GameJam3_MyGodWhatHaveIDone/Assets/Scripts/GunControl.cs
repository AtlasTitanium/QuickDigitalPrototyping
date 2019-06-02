using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunControl : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform barrelEnd;
    public float maxCubeSize = 2;

    private GameObject currentBullet;
    private bool clicked = false;
    private float cubSize = 0f;
    private float time;

    void Update(){
        if(Input.GetMouseButtonDown(0)){
            clicked = true;
            currentBullet = Instantiate(bulletPrefab, barrelEnd.position, Quaternion.identity);

            currentBullet.GetComponent<Rigidbody>().AddTorque(transform.up * 10, ForceMode.Impulse);
            currentBullet.GetComponent<Rigidbody>().AddTorque(transform.right * 10, ForceMode.Impulse);
            currentBullet.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;

            time = Time.time;
            Debug.Log("Instantiate Bullet");
        }
        if(Input.GetMouseButton(0)){
            currentBullet.transform.position = barrelEnd.position;

            if(time+0.05f <= Time.time){
                cubSize += 0.01f;
                if(cubSize >= maxCubeSize){
                    cubSize = maxCubeSize;
                }
                time = Time.time;
            }
            currentBullet.transform.localScale = new Vector3(cubSize,cubSize,cubSize);

            Debug.Log("Grow bullet");
        } else {
            
            if(cubSize >= 0.2f){
                if(clicked == true){
                    Shoot();
                }
            } else {
                Destroy(currentBullet);
                currentBullet = null;
            }
            cubSize = 0f;
            clicked = false;
        }
    }

    private void Shoot(){
        currentBullet.GetComponent<CubeExplode>().ShootCube();
        currentBullet.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        currentBullet.GetComponent<Rigidbody>().AddForce(transform.forward * cubSize * 20, ForceMode.Impulse);
        currentBullet = null;
            
        Debug.Log("ShootBullet");
    }
}
