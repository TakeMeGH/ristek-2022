using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarFollow : MonoBehaviour
{
    // Start is called before the first frame update
    Vector3 offset;
    [SerializeField] Transform playerTransform;
    void Start()
    {
        offset = transform.position - playerTransform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(playerTransform == null) return;
        transform.position = playerTransform.position + offset;
    }
}
