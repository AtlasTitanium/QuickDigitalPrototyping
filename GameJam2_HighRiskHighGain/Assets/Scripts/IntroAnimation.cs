using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroAnimation : MonoBehaviour
{
    private Rigidbody rb;

    void Start(){
        rb = GetComponent<Rigidbody>();
        StartCoroutine(Jump());
    }

    IEnumerator Jump(){
        yield return new WaitForSeconds(Random.Range(3, 8));
        rb.AddForce(transform.up * 10, ForceMode.Impulse);
        StartCoroutine(Jump());
    }
}
