using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform materialHolder;
    [SerializeField]private CraftingMaterial holdingMaterial;

    public float speed = 5;
    public float dashCooldown = 5;
    private bool canDash = true;
    private bool isDashing = false;
    
    void Update()
    {
        if(!isDashing) Move();
        if(Input.GetKeyDown(KeyCode.F)) DiscardMaterial();
        if(Input.GetKeyDown(KeyCode.LeftShift) && canDash) Dash();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent(out MaterialManipulator materialManipulator))
        {
            if (IsHoldingMaterial() && materialManipulator.CanHoldMaterial())
            {
                materialManipulator.HoldMaterial(RemoveMaterial());
                return;
            }
            if (!IsHoldingMaterial() && materialManipulator.CanRemoveMaterial())
            {
                HoldMaterial(materialManipulator.RemoveMaterial());
                return;
            }
        }
    }

    private void Move()
    {
        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
        if(input.magnitude > 0)
        {
            transform.forward = input;
            transform.position += transform.forward * input.magnitude * speed * Time.deltaTime;
        }
    }    

    private void Dash()
    {
        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
        if (input.magnitude > 0)
        {
            StartCoroutine(DashCoroutine(input));
        }
    }
    private IEnumerator DashCoroutine(Vector3 input)
    {
        canDash = false;
        isDashing = true;
        transform.forward = input;
        float counter = 0;
        while(counter < 0.5f)
        {
            transform.position += transform.forward * input.magnitude * speed * 2f * Time.deltaTime;
            counter += Time.deltaTime;
            yield return null;
        }
        isDashing = false;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

    public void HoldMaterial(CraftingMaterial material) 
    {
        if(!holdingMaterial && material)
        {
            holdingMaterial = material;
            holdingMaterial.transform.parent = materialHolder;
            holdingMaterial.transform.localPosition = Vector3.zero;
            holdingMaterial.transform.localRotation = Quaternion.identity;
        }
    }

    public CraftingMaterial RemoveMaterial()
    {
        CraftingMaterial material = holdingMaterial;
        holdingMaterial = null;
        return material;
    }
    public bool IsHoldingMaterial() => holdingMaterial;


    public void DiscardMaterial()
    {
        if (holdingMaterial)
        {
            Destroy(holdingMaterial.gameObject);
            holdingMaterial = null;
        }
    }
}