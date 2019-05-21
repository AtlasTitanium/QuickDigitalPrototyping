using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed;
    public float rotationSpeed;
    public float jumpSpeed;
    public Camera cam;


    private float currentRotationY = 0;
    private float currentRotationX = 0;
    private Rigidbody rb;
    private bool canJump = true;

    void Start(){
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        //Movement
        float verticalMovement = Input.GetAxis("Vertical") * (movementSpeed * 100) * Time.deltaTime;
        float horizontalMovement = Input.GetAxis("Horizontal") * (movementSpeed * 100) * Time.deltaTime;

        if(rb.velocity.x < 5 && rb.velocity.x > -5 && rb.velocity.z < 5 && rb.velocity.z > -5){
            rb.AddForce(transform.right * horizontalMovement);
            rb.AddForce(transform.forward * verticalMovement);
        }

        //Rotation
        float horizontalRotation = Input.GetAxis("Mouse X") * (rotationSpeed * 10) * Time.deltaTime;
        float verticalRotation = Input.GetAxis("Mouse Y") * (rotationSpeed * 10) * Time.deltaTime;
        currentRotationY += horizontalRotation;
        currentRotationX -= verticalRotation;
        
        transform.rotation = Quaternion.Euler(currentRotationX, currentRotationY, transform.rotation.z);
        // transform.rotation = Quaternion.Euler(transform.rotation.x, currentRotationY, transform.rotation.z);
        // cam.transform.rotation = Quaternion.Euler(currentRotationX, cam.transform.rotation.y, cam.transform.rotation.z);

        //Jump
        if(Input.GetAxis("Jump") > 0.1f && canJump){
            Debug.Log("Jump");
            rb.AddForce(transform.up * jumpSpeed, ForceMode.Impulse);
            canJump = false;
        }

        if(!canJump){
            // rb.AddForce(-Vector3.up * jumpSpeed/2);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        canJump = true;
    }

    void OnCollisionExit(Collision other)
    {
        canJump = false;
    }
}
