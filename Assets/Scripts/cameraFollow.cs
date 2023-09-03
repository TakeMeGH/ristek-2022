using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    [SerializeField] float FollowSpeed = 2f;
    [SerializeField] Transform playerTransform;

    // Update is called once per frame
    void Update()
    {
        if(playerTransform == null) return;
        Vector3 playerPos = new Vector3(playerTransform.position.x, playerTransform.position.y, transform.position.z);
        transform.position = Vector3.Slerp(transform.position, playerPos, FollowSpeed*Time.deltaTime);
    }
}