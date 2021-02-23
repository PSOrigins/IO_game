using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minion : MonoBehaviour
{
    public Transform IndicatorUI;
    
    private Camera _mainCamera;
    private Transform _mainCanvas;
    private Renderer _renderer;

    private Transform PlayerTransform;
    [SerializeField]
    private float Radius;
    private Rigidbody rb;
    [SerializeField]
    private float speed ;
    private Transform Target;
    [SerializeField]
    private float offSet;
    private Animator Animator;

    const float minX = -500f, maxX = 500f, maxZ = 500f, minZ = -500f;


    void Start()
    {
        transform.position = new Vector3(Random.Range(minX, maxX), transform.position.y, Random.Range(minZ, maxZ));
        PlayerTransform = PlayerController.Instance.transform;
        rb = GetComponent<Rigidbody>();
        transform.position = new Vector3(transform.position.x, 0.5f, transform.position.z);
        Animator = GetComponent<Animator>();

        _mainCamera = Camera.main;
        _mainCanvas = FindObjectOfType<Canvas>().transform; //Naudojame tik tada, kai yra vienas objektas scenoje.
        _renderer = GetComponentInChildren<Renderer>();
        IndicatorUI = Instantiate(IndicatorUI.gameObject, _mainCanvas, false).transform;
    }
    void Update() {
        if(Target == null){
            if(Vector3.Distance(transform.position, PlayerTransform.position) < Radius)
            {
                IndicatorUI.gameObject.SetActive(false);
            }
            else
            {
                IndicatorUI.gameObject.SetActive(true);
                IndicatorUI.position = _mainCamera.WorldToScreenPoint(transform.position + Vector3.up * 5);
                IndicatorUI.position = 
                    new Vector2(Mathf.Clamp(IndicatorUI.position.x, 50f, Screen.width - 50f), 
                    Mathf.Clamp(IndicatorUI.position.y, 50f, Screen.height - 50f));
                IndicatorUI.GetChild(0).eulerAngles = 
                    new Vector3(0, 0, -Mathf.Atan2(PlayerTransform.position.x - 
                    transform.position.x, PlayerTransform.position.z - transform.position.z) * Mathf.Rad2Deg);
            }
        }
        else if(IndicatorUI != null){
            Destroy(IndicatorUI.gameObject);
        }
    }


    void FixedUpdate()
    {

        if (Mathf.Sqrt(
            Mathf.Pow(transform.position.x - PlayerTransform.position.x, 2) + 
            Mathf.Pow(transform.position.z - PlayerTransform.position.z, 2)
            ) < Radius
            && Target == null
            )
        {
            PlayerController.minionCount++;
            Target = PlayerTransform;
            PlayerController.Instance.AddPerson();
        }
        else if (Target != null && Vector3.Distance(transform.position, Target.position) > offSet)
        {
            rb.MovePosition(rb.position + transform.forward * speed * Time.deltaTime);
            transform.LookAt(Target);
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
            Animator.SetBool("IsRunning", true);
        }
        else if(Target != null)
        {
            Animator.SetBool("IsRunning", false);
        }
        rb.velocity = Vector3.zero;


    }
}
