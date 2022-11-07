using System.Collections;
using System.Collections.Generic;
using UnityEditor.Profiling;
using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CapsuleCollider))]
public class Player : MonoBehaviour
{
    private Rigidbody rb;
    public GameObject child;
    
    public float speed;
    
    private Vector2 movVector;
    
    private Animator anim;
    private CapsuleCollider collid;
    private Transform trans;
    
    //Controls
    private PlayerControls controls;
    private PlayerControls.OnFootActions onFootControls;
   
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        anim = gameObject.GetComponent<Animator>();
        collid = gameObject.GetComponent<CapsuleCollider>();

        movVector = Vector2.zero;
        
        controls = new PlayerControls();
        onFootControls = controls.OnFoot;
        onFootControls.Enable();
    }

    void Update()
    {
        movVector = onFootControls.Walk.ReadValue<Vector2>();

        anim.SetFloat("vertical",movVector.y);
        anim.SetFloat("horizontal",movVector.x);

    }
    void FixedUpdate()
    {
       Move();
       View();
    }

    private void Move()
    {
        Vector3 targetvelocity = new Vector3(movVector.x,0, movVector.y) * speed;

        rb.AddForce(targetvelocity,ForceMode.Impulse);
    }
    private void View()
    {
        Plane playerPlane = new Plane(Vector3.up, transform.position);
        Ray ray = UnityEngine.Camera.main.ScreenPointToRay(Input.mousePosition);

        float hitDist = 0f;

        if (playerPlane.Raycast(ray, out hitDist))
        {
            Vector3 targetPoint = ray.GetPoint(hitDist);
            Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);
            targetRotation.x = 0;
            targetRotation.z = 0;
            
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 7f * Time.deltaTime);
            
        }
    }
    
}
