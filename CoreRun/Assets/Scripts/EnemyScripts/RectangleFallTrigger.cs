using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Workaround to support animation with falling collider while keeping rectangle graphic in a seperate object
public class RectangleFallTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        transform.parent.GetComponent<EvilRectangle>().OnTriggerFire(other);
    }
}
