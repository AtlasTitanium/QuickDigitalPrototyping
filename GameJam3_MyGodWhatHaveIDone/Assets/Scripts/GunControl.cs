using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunControl : MonoBehaviour
{
    public GameObject waterPoolParent;
    public GameObject bulletPrefab;
    public GameObject waterPrefab;
    public Transform barrelEnd;
    public float maxCubeSize = 2;

    private GameObject currentBullet;
    private List<GameObject> waterPool = new List<GameObject>();
    private float cubSize = 0f;
    private float time;

    void Start(){
        Cursor.visible = false;
    }

    void Update(){
        if(Input.GetMouseButton(0)){ 
            //Shoot water every x seconds
            float x = 0.05f;
            if(time + x >= Time.time){
                return;
            } else {
                time = Time.time;
            }     

            //Shoot normal water stream
            if(waterPool.Count <= 40){
                //create water pool

                GameObject currentWater = Instantiate(waterPrefab, barrelEnd.position, Quaternion.identity);
                currentWater.GetComponent<Rigidbody>().AddForce(transform.forward * 20, ForceMode.Impulse);
                currentWater.transform.localScale = new Vector3(Random.Range(0.1f,0.6f), Random.Range(0.1f,0.6f), Random.Range(0.1f,0.6f));
                currentWater.transform.parent = waterPoolParent.transform;                

                waterPool.Add(currentWater);
            } else {
                //Use the created waterpool

                GameObject currentWater = waterPool[0];
                waterPool.RemoveAt(0);
                currentWater.transform.position = barrelEnd.position;
                currentWater.GetComponent<Rigidbody>().AddForce(transform.forward * 20, ForceMode.Impulse);
                currentWater.transform.localScale = new Vector3(Random.Range(0.1f,0.6f), Random.Range(0.1f,0.6f), Random.Range(0.1f,0.6f));

                waterPool.Add(currentWater);
            }
            currentBullet = null;
        }

        //Shoot water bomb        
        if(Input.GetMouseButtonDown(1)){
            cubSize = 0f;
            currentBullet = Instantiate(bulletPrefab, barrelEnd.position, Quaternion.identity);

            currentBullet.GetComponent<Rigidbody>().AddTorque(transform.up * 10, ForceMode.Impulse);
            currentBullet.GetComponent<Rigidbody>().AddTorque(transform.right * 10, ForceMode.Impulse);
            currentBullet.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;

            time = Time.time;
        }

        if(currentBullet == null){
            return;
        }

        if(Input.GetMouseButton(1)){
            currentBullet.transform.position = barrelEnd.position;
            if(time+0.01f <= Time.time){
                cubSize += 0.02f;
                if(cubSize >= maxCubeSize){
                    cubSize = maxCubeSize;
                }
                time = Time.time;
            }
            currentBullet.transform.localScale = new Vector3(cubSize,cubSize,cubSize);
        } else {
            if(cubSize >= 0.2f){
                Shoot();
            } else {
                Destroy(currentBullet);
            }
            currentBullet = null;
        }
    }

    private void Shoot(){
        currentBullet.GetComponent<CubeExplode>().ShootCube();
        currentBullet.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        currentBullet.GetComponent<Rigidbody>().AddForce(barrelEnd.transform.forward * cubSize * 30, ForceMode.Impulse);
    }
}
