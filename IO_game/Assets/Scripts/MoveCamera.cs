using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour 
{
    private Transform PlayerTransform;
    private Vector3 CameraOffSet;

    void Start()
    {
        PlayerTransform = PlayerController.Instance.transform;
        CameraOffSet = transform.position;
    }

    void Update()
    {
        transform.position = PlayerTransform.position + CameraOffSet;
    }
}
