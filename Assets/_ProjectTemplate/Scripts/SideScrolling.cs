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
        var position = transform.position;
        Vector3 targetPosition = new Vector3(player.position.x + offset.x, position.y + offset.y, position.z + offset.z);
        targetPosition.x = Mathf.Clamp(targetPosition.x, minX, maxX);
        position = targetPosition;
        transform.position = position;
    }
}
