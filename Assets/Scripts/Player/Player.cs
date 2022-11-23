using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Profiling;
using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CapsuleCollider))]
public class Player : MonoBehaviour
{
    [SerializeField] private GameObject child;
    
    [SerializeField] private float speed;
    
    [SerializeField] private SpellsPool spellsPool;
    
    [SerializeField] private float Mainspellcd;
    [SerializeField] private float Secspellcd;
    
    private Rigidbody rb;
    private Animator anim;
    private CapsuleCollider collid;
    private Transform trans;
    private HealthSystem heatlhsys;
    
    //Controls
    private PlayerControls controls;
    private PlayerControls.OnFootActions onFootControls;
    
    
    private Vector2 GetmovVec;
    private Vector2 GetmousePos;
    private Vector3 movVector;
    private Vector3 mousePos;

    //Spells
    private bool MainSpellPressed;
    private bool SecSpellPressed;
    private float MainSpellCdRemaining;
    private float SecSpellCdRemaining;
    
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        anim = gameObject.GetComponent<Animator>();
        collid = gameObject.GetComponent<CapsuleCollider>();
        heatlhsys = gameObject.GetComponent<HealthSystem>();
        
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
                
        //Update cooldown
        MainSpellCdRemaining = (MainSpellCdRemaining <= 0) ? 0 : MainSpellCdRemaining -= Time.deltaTime;
        SecSpellCdRemaining = (SecSpellCdRemaining <= 0) ? 0 : SecSpellCdRemaining -= Time.deltaTime;
    }
    void FixedUpdate()
    {
       Move();
       View();
    }

    private void CastSpells(bool MainSpell, bool SecSpell)
    {
        Vector3 spellpos;
        spellpos = transform.position;
        spellpos.z += 0.2f;
        spellpos.y += 0.3f;
        
        if (MainSpell && MainSpellCdRemaining <= 0)
        {
            spellsPool.GetSpell(spellpos, transform.rotation, TypeOfSpells.Fireball);
            MainSpellCdRemaining = Mainspellcd;
        }
        
        if (SecSpell && SecSpellCdRemaining <= 0)
        {
            spellsPool.GetSpell(spellpos, transform.rotation, TypeOfSpells.Ligthingball);
            SecSpellCdRemaining = Secspellcd;
        }
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

