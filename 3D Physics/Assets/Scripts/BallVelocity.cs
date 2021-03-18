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
    const float PHOTON_MAX_DISTANCE = 1;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = startingVelocity;
        CalculateImagePosition();
        hasBeenRefracted = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (CheckIfInRefractionRegion())
        {
            CalculateImagePosition();
            if (spawnPointIsFocalLength)
            {
                rb.velocity = (lens.pose.position - startingPosition).normalized * startingVelocity.magnitude;
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

    void CalculateImagePosition()
    {
        var startingDistance = Mathf.Abs(startingPosition.x);
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
        print(startingPosition);
        print(lens.pose.position);
        imagePosition = lens.pose.position + (new Vector3(imageDistance, (startingPosition.y - lens.pose.position.y) * imageRatio, (startingPosition.z - lens.pose.position.z) * imageRatio));

    }

    bool CheckIfInRefractionRegion()
    {
        if (!hasBeenRefracted)
        {
            var ballPosition = this.transform.position;
            var lensPosition = lens.pose.position;
            if (ballPosition.x > lensPosition.x)
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
            if (imagePosition.x > 0)
            {
                if (this.transform.position.x > Mathf.Min(imagePosition.x, PHOTON_MAX_DISTANCE))
                {
                    return true;
                }
            } else {
                if (this.transform.position.x > 0.5)
                {
                    return true;
                }
            }
        }
        return false;
    }
}
