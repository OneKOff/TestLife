using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float zoomSpeedMultiplier;
    [Header("Bounds")]
    [SerializeField] private Vector3Int minBounds;
    [SerializeField] private Vector3Int maxBounds;

    private Vector3 _input;

    private void Update()
    {
        _input = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), Input.mouseScrollDelta.y * zoomSpeedMultiplier);
    }

    private void FixedUpdate()
    {
        var boundPosition = transform.position + _input * speed * Time.deltaTime;
        boundPosition = new Vector3(Mathf.Clamp(boundPosition.x, minBounds.x, maxBounds.x), 
            Mathf.Clamp(boundPosition.y, minBounds.y, maxBounds.y), Mathf.Clamp(boundPosition.z, minBounds.z, maxBounds.z));

        transform.position = boundPosition;
    }
}
