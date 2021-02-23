using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;
    public static int minionCount { get; set; }
    public Transform AnalogUI;
    public TextMeshProUGUI ScoreText;

    private Rigidbody rb;
    private Vector2 ClickPosOrigin;
    private Vector2 ClickPosCur;
    private float RotationAngle;
    private AnalogState State;
    private Animator _playerAnim;

    [SerializeField]
    private float Speed;
    

    private const float minClamp = 0f;
    private const float maxClamp = 50f;
    
    private enum AnalogState
    {
        Usable,
        Idle
    }


    void Awake()
    {
        ScoreText.text = "0";
        State = AnalogState.Idle;
        AnalogUI.gameObject.SetActive(false);
        rb = GetComponent<Rigidbody>();
        if(Instance == null)
            Instance = this;
        _playerAnim = GetComponentInChildren<Animator>();
        
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            State = AnalogState.Usable;
            ClickPosOrigin = Input.mousePosition;
            AnalogUI.position = ClickPosOrigin;
            AnalogUI.gameObject.SetActive(true);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            State = AnalogState.Idle;
            AnalogUI.gameObject.SetActive(false);

        }

        
    }
    void FixedUpdate()
    {
        switch (State)
        {
            case AnalogState.Usable:
                _playerAnim.SetBool("IsRunning", true);
                ClickPosCur = Input.mousePosition;
                RotationAngle = Mathf.Atan2(ClickPosCur.x - ClickPosOrigin.x, ClickPosCur.y - ClickPosOrigin.y) * Mathf.Rad2Deg;
                transform.eulerAngles = new Vector3(0, RotationAngle, 0);
                float distanceBetweenPoints = Mathf.Clamp(Vector2.Distance(ClickPosCur, ClickPosOrigin), minClamp, maxClamp);
                AnalogUI.GetChild(0).position = ClickPosOrigin +
                    new Vector2(ClickPosCur.x - ClickPosOrigin.x, ClickPosCur.y - ClickPosOrigin.y).normalized * distanceBetweenPoints;
                rb.MovePosition(rb.position + transform.forward * Speed * Time.deltaTime);
                break;

            case AnalogState.Idle:
                _playerAnim.SetBool("IsRunning", false);
                break;
        }
    }

    public void AddPerson(){
        ScoreText.text = minionCount.ToString();
    }
}
