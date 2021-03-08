using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallVelocity : MonoBehaviour
{
    public Rigidbody rb;
    public float startingVelocity;
    private bool hasBeenRefracted;
    private Vector3 startingForce;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        startingForce = new Vector3(0.5f, 1.0f, 0.5f);
        startingForce = startingVelocity * startingForce;
        rb.AddForce(startingForce);
        hasBeenRefracted = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasBeenRefracted && rb.transform.position.y > 0)
        {
            rb.AddForce(new Vector3(startingForce.x, 0, startingForce.z));
            hasBeenRefracted = true;
        }
    }
}
