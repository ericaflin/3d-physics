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
                spawnVelocity = ((new Vector3(0, spawnOffset.y, spawnOffset.x) - spawnPosition).normalized) * startingSpeed;
            }
            else
            {
                spawnPosition.z = spawnOffset.x;
                spawnPosition.y = spawnOffset.y;
                spawnVelocity = new Vector3(startingSpeed, 0, 0);
            }
            spawnPosition = this.transform.position + spawnPosition;
            BallVelocity ball = Instantiate(ballPrefab, spawnPosition, Quaternion.identity);
            ball.startingPosition = spawnPosition;
            ball.startingVelocity = spawnVelocity;
            ball.lens = lens;
        }
        counter++;
    }

}
