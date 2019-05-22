using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerSimple : MonoBehaviour
{
    public float jumpStrenght;
    public float superJumpStrenght;
    public Camera cam;
    public Camera SecondCam;
    public GameObject landingPlane;
    public LayerMask groundLayer;
    public LayerMask platformLayer;
    public GameObject canvas;
    public PlatformGenerator platformGenerator;

    private Rigidbody rb;
    private bool canJump = true;
    private GameObject ground;
    private bool SuperJump = false;

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
        if (Physics.Raycast (ray, out hit, Mathf.Infinity, platformLayer)) {
            landingPlane.transform.position = new Vector3(hit.point.x, hit.point.y+0.1f, hit.point.z);
        }   
        if (Physics.Raycast (ray, out hit, Mathf.Infinity, groundLayer)) {
            playerPosX = hit.point.x;
            playerPosZ = hit.point.z;
        }   
        transform.position = new Vector3(playerPosX,transform.position.y,playerPosZ);

        //Jump
        if(Input.GetAxis("Jump") > 0.1f && canJump){
            Debug.Log("Jump");
            if(SuperJump){
                rb.AddForce(transform.up * superJumpStrenght, ForceMode.Impulse);
                SuperJump = false;
            } else {
                rb.AddForce(transform.up * jumpStrenght, ForceMode.Impulse);
            }
            canJump = false;
        }

        //Move cam with height
        cam.transform.position = new Vector3(cam.transform.position.x, transform.position.y + 30, cam.transform.position.z);
        SecondCam.transform.position = new Vector3(SecondCam.transform.position.x, transform.position.y, SecondCam.transform.position.z);
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<PlatFormInit>()){
            PlatFormInit PFT = collision.gameObject.GetComponent<PlatFormInit>();
            if(PFT.AutoJump){
                Debug.Log("Jump");
                rb.AddForce(transform.up * jumpStrenght, ForceMode.Impulse);
                canJump = false;
            } else if(PFT.SuperJumpPad){
                SuperJump = true;
                canJump = true;
            } else {
                canJump = true;
            }
        } else {
            canJump = true;
        }
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
