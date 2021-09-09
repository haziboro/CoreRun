using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterLoader : MonoBehaviour
{
    [SerializeField] PlayerGraphicContainer container;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(container.playerGraphic, transform.position,
            transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
