using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minion : MonoBehaviour
{
    public Transform IndicatorUI;

    private Camera MainCamera;
    private Transform MainCanvas;
    private Renderer _Renderer;

    private Transform PlayerTransform;
    private Transform Target;
    private Rigidbody RB;
    private Animator Anim;

    const float minX = -500, maxX = 500, minZ = -500, maxZ = 500;

    [SerializeField]
    private float Speed;
    [SerializeField]
    private float Radius;
    [SerializeField]
    private float Offset;

    void Start()
    {
        transform.position = new Vector3(Random.Range(minX, maxX), transform.position.y, Random.Range(minZ, maxZ));
        PlayerTransform = PlayerController.Instance.transform;
        RB = GetComponent<Rigidbody>();
        Anim = GetComponentInChildren<Animator>();

        MainCamera = Camera.main;
        MainCanvas = FindObjectOfType<Canvas>().transform;
        _Renderer = GetComponentInChildren<Renderer>();
        IndicatorUI = Instantiate(IndicatorUI.gameObject, MainCanvas, false).transform;
    }

    void Update()
    {
        if (Target == null)
        {
            if (Vector3.Distance(transform.position, PlayerTransform.position) < Radius)
            {
                IndicatorUI.gameObject.SetActive(false);
            }
            else
            {
                IndicatorUI.gameObject.SetActive(true);
                IndicatorUI.position = MainCamera.WorldToScreenPoint(transform.position + Vector3.up * 5);
                IndicatorUI.position = new Vector2(Mathf.Clamp(IndicatorUI.position.x, 50, Screen.width - 50), Mathf.Clamp(IndicatorUI.position.y, 50, Screen.height - 50));
                IndicatorUI.GetChild(0).eulerAngles =
                    new Vector3(0, 0, -Mathf.Atan2(PlayerTransform.position.x - transform.position.x,
                    PlayerTransform.position.z - transform.position.z) * Mathf.Rad2Deg);
            }
        }
        else if (IndicatorUI != null)
        {
            Destroy(IndicatorUI.gameObject);
        }


    }

    void FixedUpdate()
    {
        if  (Mathf.Sqrt(Mathf.Pow(transform.position.x - PlayerTransform.position.x, 2)
            + Mathf.Pow(transform.position.z - PlayerTransform.position.z, 2)) < Radius && Target == null)
        {
            PlayerController.MinionCount++;
            PlayerController.Instance.AddPerson();
            Target = PlayerTransform;
        }
        else if (Target != null && Vector3.Distance(transform.position, Target.position) > Offset)
        {
            RB.MovePosition(RB.position + transform.forward * Speed * Time.deltaTime);
            transform.LookAt(Target);
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
            Anim.SetBool("Runing", true);
        }
        else if (Target != null)
            Anim.SetBool("Runing", false);

        RB.velocity = Vector3.zero;
    }
}