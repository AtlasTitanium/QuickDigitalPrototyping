using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerSimple : MonoBehaviour
{
    public float jumpSpeed;
    public Camera cam;
    public Camera SecondCam;
    public GameObject landingPlane;
    public LayerMask groundLayer;
    public GameObject canvas;

    private Rigidbody rb;
    private bool canJump = true;
    private GameObject ground;

    void Start(){
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        //Move
        float playerPosX = transform.position.x;
        float playerPosZ = transform.position.z;

        Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast (ray, out hit, Mathf.Infinity, groundLayer)) {
            landingPlane.transform.position = new Vector3(hit.point.x, hit.point.y + 0.1f, hit.point.z);
            playerPosX = hit.point.x;
            playerPosZ = hit.point.z;
        }   
        transform.position = new Vector3(playerPosX,transform.position.y,playerPosZ);

        //Jump
        if(Input.GetAxis("Jump") > 0.1f && canJump){
            Debug.Log("Jump");
            rb.AddForce(transform.up * jumpSpeed, ForceMode.Impulse);
            canJump = false;
        }

        //Move cam with height
        cam.transform.position = new Vector3(cam.transform.position.x, transform.position.y + 30, cam.transform.position.z);
        SecondCam.transform.position = new Vector3(SecondCam.transform.position.x, transform.position.y, SecondCam.transform.position.z);
    }

    void OnCollisionEnter(Collision collision)
    {
        canJump = true;
    }

    void OnCollisionExit(Collision other)
    {
        canJump = false;
    }

    public void GameOver(){
        canvas.SetActive(true);
        Debug.Log("OOf, game over");
        this.enabled = false;
    }
}
