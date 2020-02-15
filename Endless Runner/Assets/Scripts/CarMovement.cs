using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CarMovement : MonoBehaviour
{

    public float forwardForce = 10;
    public float horizontalMultiplier = 5;

    Rigidbody rb;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(Input.GetAxis("vertical")+","+ Input.GetAxis("horizontal"));
        rb.AddForce(Input.GetAxis("horizontal") * horizontalMultiplier*Time.deltaTime, 0, 0);

        rb.AddForce(0, 0, forwardForce*Time.deltaTime);

        
    }
}
