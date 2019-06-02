using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathGround : MonoBehaviour
{
    public LayerMask npcLayer;

    void OnCollisionEnter(Collision collision)
    {
        if ((npcLayer & 1 << collision.gameObject.layer) == 1 << collision.gameObject.layer)
        {
            Destroy(collision.transform.parent.gameObject);
        }
    }

}
