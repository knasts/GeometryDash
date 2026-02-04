using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;

    private void Start()
    {
        offset = transform.position - player.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(player != null)
        {
            Vector3 newPosition = player.position + offset;
            transform.position = new Vector3(newPosition.x, transform.position.y, transform.position.z);
        }
    }
}
