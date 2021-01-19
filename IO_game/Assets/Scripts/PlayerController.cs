using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private Vector2 ClickPosOrigin;
    private Vector2 ClickPosCur;
    private float RotationAngle;
    private AnalogState State;

    [SerializeField]
    private float Speed;
    
    private enum AnalogState
    {
        Usable,
        Idle
    }


    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            State = AnalogState.Usable;
            ClickPosOrigin = Input.mousePosition;
            rb.MovePosition(rb.position + transform.forward * Speed * Time.deltaTime);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            State = AnalogState.Idle;
        }
        switch (State)
        {
            case AnalogState.Usable:
                ClickPosCur = Input.mousePosition;
                RotationAngle = Mathf.Atan2(ClickPosCur.x - ClickPosOrigin.x, ClickPosCur.y - ClickPosOrigin.y) * Mathf.Rad2Deg;
                transform.eulerAngles = new Vector3(0, RotationAngle, 0);
                break;

            case AnalogState.Idle:
                break;
        }
    }
}
