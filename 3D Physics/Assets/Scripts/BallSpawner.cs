using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    public BallVelocity ballPrefab;
    public float startingVelocity;
    public Vector3 startingPosition;
    private int counter = 0;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (counter % 150 == 0)
        {
            BallVelocity ball = Instantiate(ballPrefab, startingPosition, Quaternion.identity);
            ball.startingVelocity = startingVelocity;
        }
        counter++;
    }
}
