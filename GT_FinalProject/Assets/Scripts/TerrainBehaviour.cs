using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainBehaviour : MonoBehaviour
{
    private void Awake()
    {
        //Randomise size
        float objectSize = Random.Range(0.15f, 0.5f);
       transform.localScale = new Vector3 (objectSize, objectSize, 0);

        //Adjust y position in accordance with new size
       float newYPosition = transform.position.y-((0.5f-objectSize)*5);
        transform.position = new Vector3(transform.position.x, newYPosition, transform.position.z);

        Vector3 target = new Vector3(4.07f, 0, 3.67f);
        transform.LookAt(target);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
