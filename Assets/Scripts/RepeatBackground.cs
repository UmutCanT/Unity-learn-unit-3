using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatBackground : MonoBehaviour
{
    Vector3 startPosition;
    BoxCollider boxCollider;

    float offset;

    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
        offset = boxCollider.size.x / 2;
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x < startPosition.x - offset)
        {
            transform.position = startPosition;
        }
    }
}
