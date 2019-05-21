using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int bulletSpeed;
    private int bulletDamage;
    public void  Shoot(int _bulletDamage){
        bulletDamage = _bulletDamage;
        GetComponent<Rigidbody>().AddForce(transform.forward * bulletSpeed, ForceMode.Impulse);
        StartCoroutine(TillDeath());
    }

    IEnumerator TillDeath(){
        yield return new WaitForSeconds(1);
        Destroy(this.gameObject);
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<Enemy>()){
            StopAllCoroutines();
            collision.gameObject.GetComponent<Enemy>().GainDamage(bulletDamage);
            Destroy(this.gameObject);
        }
    }
}
