using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    public BallVelocity ballPrefab;
    public float startingSpeed;
    public Vector3 startingPosition;
    public float spawnRadius;
    public bool spawnPoint;
    public Lens lens;
    private int counter = 0;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (counter % 1 == 0)
        {
            var spawnPosition = startingPosition;
            var spawnOffset = Random.insideUnitCircle * spawnRadius;
            //print(spawnOffset);
            var spawnVelocity = new Vector3();
            if (spawnPoint)
            {
                spawnVelocity = ((new Vector3(spawnOffset.x, spawnOffset.y, 0) - spawnPosition).normalized) * startingSpeed;
            }
            else
            {
                spawnPosition.x = spawnOffset.x;
                spawnPosition.y = spawnOffset.y;
                spawnVelocity = new Vector3(0, 0, startingSpeed);
            }

            Vector3 relativePosition = this.transform.TransformDirection(spawnPosition);


            BallVelocity ball = Instantiate(ballPrefab, relativePosition, Quaternion.identity, this.transform);
            ball.startingPosition = spawnPosition;
            ball.startingVelocity = spawnVelocity;
            ball.lens = lens;
        }
        counter++;
    }

}
