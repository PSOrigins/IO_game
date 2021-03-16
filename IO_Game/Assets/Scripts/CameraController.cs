using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform PlayerTransform;
    private Vector3 CameraOffset;

    void Start()
    {
        PlayerTransform = PlayerController.Instance.transform;
        CameraOffset = transform.position;
    }

    void Update()
    {
        transform.position = PlayerTransform.position + CameraOffset;
    }
}
