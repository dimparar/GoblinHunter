using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kill : MonoBehaviour
{
    [SerializeField] float time;
    [SerializeField] float damage;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Hit", time);
    }

    // Update is called once per frame
    private void Hit()
    {
        //GetComponent<PlayerController>().TakeDamage(damage);
    }
}
