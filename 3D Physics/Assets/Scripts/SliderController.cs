using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    public BallSpawner spawner;
    public Slider xSlider;
    public Slider ySlider;
    public Slider zSlider;
    public Slider focalLengthSlider;
    public Slider speedSlider;
    public Toggle emissionMode;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        spawner.startingPosition.x = xSlider.value;
        spawner.startingPosition.y = ySlider.value;
        spawner.startingPosition.z = -zSlider.value;
        spawner.startingSpeed = speedSlider.value;
        spawner.lens.focalLength = focalLengthSlider.value;
        spawner.lens.transform.localScale = new Vector3(1.5f, 1.5f, spawner.lens.focalLength * 3);
        spawner.spawnPoint = emissionMode.isOn;
    }
}
