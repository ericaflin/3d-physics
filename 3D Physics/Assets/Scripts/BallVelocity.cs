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
    private bool virtualImage;
    private bool spawnPointIsFocalLength;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = startingVelocity;
        var startingDistance = Mathf.Abs(startingPosition.z);
        if (lens.focalLength == startingDistance)
        {
            spawnPointIsFocalLength = true;
        }
        else
        {
            spawnPointIsFocalLength = false;
        }
        var imageDistance = 1 / ((1/lens.focalLength) - (1/startingDistance));
        var imageRatio = -(imageDistance / startingDistance);
        if (imageDistance < 0)
        {
            virtualImage = true;
            spawnPointIsFocalLength = false;
        }
        else
        {
            virtualImage = false;
        }
        imagePosition = new Vector3(startingPosition.x * imageRatio, startingPosition.y * imageRatio, imageDistance);
        hasBeenRefracted = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (CheckIfInRefractionRegion())
        {
            if (spawnPointIsFocalLength)
            {
                rb.velocity = (lens.transform.position - startingPosition).normalized * startingVelocity.magnitude;
            }
            else if (virtualImage)
            {
                rb.velocity = -(imagePosition - this.transform.position).normalized * startingVelocity.magnitude;
            }
            else
            {
                rb.velocity = (imagePosition - this.transform.position).normalized * startingVelocity.magnitude;
            }
            hasBeenRefracted = true;
        }
        if (CheckIfInImageRegion())
        {
            Destroy(this.gameObject);
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

    bool CheckIfInImageRegion()
    {
        if (hasBeenRefracted)
        {
            if (this.transform.position.z > imagePosition.z)
            {
                return true;
            }
        }
        return false;
    }
}
