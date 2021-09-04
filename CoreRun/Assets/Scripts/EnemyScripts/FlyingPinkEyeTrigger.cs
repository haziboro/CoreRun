using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingPinkEyeTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        GameObject pinkBody = transform.parent.gameObject;

        pinkBody.transform.parent.GetComponent<EvilFlyingPink>().OnTriggerFire(other);
    }
}
