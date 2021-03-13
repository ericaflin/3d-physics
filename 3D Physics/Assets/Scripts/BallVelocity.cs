using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallVelocity : MonoBehaviour
{
    public Rigidbody rb;
    public Lens lens;
    private bool hasBeenRefracted;
    public Vector3 startingPosition;
    public Vector3 startingVelocity;
    private Vector3 imagePosition;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = startingVelocity;
        var imageDistance = 1 / ((1/lens.focalLength) - (1/Mathf.Abs(startingPosition.z)));
        var imageRatio = -(imageDistance / startingPosition.z);
        imagePosition = new Vector3(startingPosition.x * imageRatio, startingPosition.y * imageRatio, imageDistance);
        hasBeenRefracted = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (CheckIfInRefractionRegion())
        {
            rb.velocity = (imagePosition - this.transform.position).normalized * startingVelocity.magnitude;
            print(rb.velocity);
            hasBeenRefracted = true;
        }
    }

    bool CheckIfInRefractionRegion()
    {
        if (!hasBeenRefracted)
        {
            var ballPosition = this.transform.position;
            var lensPosition = lens.transform.position;
            if (ballPosition.z > lensPosition.z)
            {
                return true;
            }
        }
        return false;
    }
}
