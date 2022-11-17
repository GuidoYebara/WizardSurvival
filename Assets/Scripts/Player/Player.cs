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

    public TypeOfSpells spellType;
    public SpellsPool spellsPool;

    private Animator anim;
    private CapsuleCollider collid;
    private Transform trans;
    
    //Controls
    private PlayerControls controls;
    private PlayerControls.OnFootActions onFootControls;
    
    
    private Vector2 GetmovVec;
    private Vector2 GetmousePos;
    private Vector3 movVector;
    private Vector3 mousePos;

    private bool MainSpellPressed;
    private bool SecSpellPressed;
    
    
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
        ReadInput();
        SetAnimations();

        if (MainSpellPressed || SecSpellPressed)
            CastSpells(MainSpellPressed, SecSpellPressed);

    }
    void FixedUpdate()
    {
       Move();
       View();
       
    }

    private void CastSpells(bool MainSpell, bool SecSpell)
    {
        spellsPool.GetSpell(transform.position + new Vector3(0,1,0), transform.rotation, spellType);
    }
    private void ReadInput()
    {
        //read WSAD, movement
        GetmovVec = onFootControls.Walk.ReadValue<Vector2>();
        movVector.Set(GetmovVec.x,0,GetmovVec.y);
        
        //read mouse coordinates
        GetmousePos = onFootControls.MousePos.ReadValue<Vector2>();
        mousePos.Set(GetmousePos.x,GetmousePos.y,0);
        
        //Spells
        MainSpellPressed = onFootControls.MainSpell.IsPressed();
        SecSpellPressed = onFootControls.SecSpell.IsPressed();
        
    }

    private void SetAnimations()
    {
        anim.SetFloat("vertical",movVector.z);
        anim.SetFloat("horizontal",movVector.x);
        
        
    }
    private void Move()
    {
        Vector3 targetvelocity = movVector * speed;

        rb.AddForce(targetvelocity,ForceMode.Impulse);
    }
    private void View()
    {
        Plane playerPlane = new Plane(Vector3.up, transform.position);
        Ray ray = UnityEngine.Camera.main.ScreenPointToRay(mousePos);

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
