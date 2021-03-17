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
    private Vector3 worldPosition;
    private Vector3 worldVelocity;
    private Vector3 imagePosition;
    private bool virtualImage;
    private bool spawnPointIsFocalLength;
    const float PHOTON_MAX_DISTANCE = 1;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = this.transform.parent.TransformDirection(startingVelocity);
        CalculateImagePosition();
        hasBeenRefracted = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 vel = rb.velocity;
        if (CheckIfInRefractionRegion())
        {
            CalculateImagePosition();
            if (spawnPointIsFocalLength)
            {
                vel = (lens.pose.position - startingPosition).normalized * startingVelocity.magnitude;
            }
            else if (virtualImage)
            {
                vel = -(imagePosition - this.transform.position).normalized * startingVelocity.magnitude;
            }
            else
            {
                vel = (imagePosition - this.transform.position).normalized * startingVelocity.magnitude;
            }
            hasBeenRefracted = true;
        }
        rb.velocity = this.transform.parent.TransformDirection(vel);

        if (CheckIfInImageRegion())
        {
            Destroy(this.gameObject);
        }
    }

    void CalculateImagePosition()
    {
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
        imagePosition = this.transform.parent.InverseTransformDirection(new Vector3(startingPosition.x * imageRatio, startingPosition.y * imageRatio, imageDistance));
    }

    bool CheckIfInRefractionRegion()
    {
        if (!hasBeenRefracted)
        {
            var ballPosition = this.transform.parent.InverseTransformDirection(this.transform.position);
            var lensPosition = this.transform.parent.InverseTransformDirection(lens.pose.position);
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
            Vector3 pos = this.transform.parent.InverseTransformDirection(this.transform.position);
            if (imagePosition.z > 0)
            {
                if (pos.z > Mathf.Min(imagePosition.z, PHOTON_MAX_DISTANCE))
                {
                    return true;
                }
            } else {
                if (pos.z > 0.5)
                {
                    return true;
                }
            }
        }
        return false;
    }
}
