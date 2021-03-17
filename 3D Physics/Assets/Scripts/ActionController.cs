using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionController : MonoBehaviour
{
    public GameObject spawner;
    public Slider rotation;
    public Slider size;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        spawner.transform.rotation = Quaternion.AngleAxis(Mathf.Rad2Deg * rotation.value, Vector3.up);
        spawner.transform.localScale = new Vector3(size.value, size.value, size.value);
    }
}
