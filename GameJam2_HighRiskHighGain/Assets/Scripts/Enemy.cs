using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 100;
    public float floatStrenght = 10;
    public float floatDistance = 1;
    private Rigidbody rb;

    void Start(){
        rb = GetComponent<Rigidbody>();
    }
    void Update(){
        if(Physics.Raycast(transform.position, -Vector3.up, floatDistance)){
            rb.AddForce(transform.up * floatStrenght);
        }
    }
    public void GainDamage(int damage){
        health -= damage;
    }
}
