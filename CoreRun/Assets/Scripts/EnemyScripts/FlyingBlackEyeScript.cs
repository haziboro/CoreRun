using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingBlackEyeScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        GameObject blackBody = transform.parent.gameObject;

        blackBody.transform.parent.GetComponent<EvilFlyingBlack>().OnTriggerFire(other);
    }
}
