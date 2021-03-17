using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lens : MonoBehaviour
{

    public float focalLength = 2;
    public GameObject convexPrefab;
    public GameObject concavePrefab;
    public Pose pose;
    private bool focalLengthPositive;
    private GameObject currentLens;
    private Vector3 originalScale;
    
    // Start is called before the first frame update
    void Start()
    {
        if (focalLength > 0)
        {
            focalLengthPositive = true;
            currentLens = Instantiate(convexPrefab, pose.position, pose.rotation);
        }
        else
        {
            focalLengthPositive = false;
            currentLens = Instantiate(concavePrefab, pose.position, pose.rotation);
        }
        originalScale = currentLens.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        SwitchPrefab();
    }

    private void SwitchPrefab()
    {
        if (focalLength >= 0 && !focalLengthPositive)
        {
            focalLengthPositive = true;
            Destroy(currentLens);
            currentLens = Instantiate(convexPrefab, pose.position, pose.rotation);
        }
        else if (focalLength < 0 && focalLengthPositive)
        {
            focalLengthPositive = false;
            Destroy(currentLens);
            currentLens = Instantiate(concavePrefab, pose.position, pose.rotation);
        }
        Vector3 lensScale;
        if (focalLengthPositive)
        {
            lensScale = new Vector3(0.05f, 0.05f, Mathf.Abs(this.focalLength) * 0.2f);
        }
        else
        {
            lensScale = new Vector3(0f, 0f, (Mathf.Abs(this.focalLength) * 0.02f) - 0.08f);
        }
        currentLens.transform.localScale = originalScale + lensScale;
    }
}
