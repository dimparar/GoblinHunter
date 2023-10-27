using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kill : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<PlayerController>().TakeDamage(150);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
