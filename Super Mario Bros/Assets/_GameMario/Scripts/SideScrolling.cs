using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideScrolling : MonoBehaviour
{
    private Transform player;
    public Vector3 offset;
    public float minX; 
    public float maxX = float.PositiveInfinity;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    private void LateUpdate()
    {
        Vector3 targetPosition = new Vector3(player.position.x + offset.x, transform.position.y + offset.y, transform.position.z + offset.z);
        targetPosition.x = Mathf.Clamp(targetPosition.x, minX, maxX);
        transform.position = targetPosition;
    }
}
