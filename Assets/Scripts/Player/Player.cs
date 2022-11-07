using System.Collections;
using System.Collections.Generic;
using UnityEditor.Profiling;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody rb;
    public GameObject child;
    
    public float speed;
    
    private Vector3 movVector;
    
    private Animator anim;
    private CapsuleCollider collid;
    private Transform trans;
    
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        anim = gameObject.GetComponent<Animator>();
        collid = gameObject.GetComponent<CapsuleCollider>();

        movVector = Vector3.zero;

    }

    void Update()
    {
        movVector.z = Input.GetAxis("Vertical");
        movVector.x = Input.GetAxis("Horizontal");
        
        //Animating
        if (movVector != Vector3.zero)
            anim.SetBool("running",true);
        else
            anim.SetBool("running",false);
        
    }
    void FixedUpdate()
    {
       Move();
       View();
    }

    private void Move()
    {
        //Vector3 targetvelocity = new Vector3(moveX * speed,0,moveY * speed);

        Vector3 targetvelocity = movVector * speed;
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
