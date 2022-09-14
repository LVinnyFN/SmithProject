using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialHeater : MaterialManipulator
{
    public Transform[] itemSlots;
    [SerializeField]private CraftingMaterial[] holdingMaterials;

    private void Awake()
    {
        holdingMaterials = new CraftingMaterial[itemSlots.Length];
    }

    private void Update()
    {
        for(int i = 0; i < holdingMaterials.Length; i++)
        {
            if (holdingMaterials[i]) holdingMaterials[i].Heat();
        }
    }

    public override void HoldMaterial(CraftingMaterial material)
    {
        for (int i = 0; i < holdingMaterials.Length; i++)
        {
            if (holdingMaterials[i]) continue;

            holdingMaterials[i] = material;
            holdingMaterials[i].transform.SetParent(itemSlots[i]);
            holdingMaterials[i].transform.position = itemSlots[i].position;
            return;
        }        
    }
    public override CraftingMaterial RemoveMaterial()
    {
        CraftingMaterial hottestMaterial = null;
        int materialIndex = -1;
        for( int i = 0; i < holdingMaterials.Length; i++)
        {
            if (!holdingMaterials[i]) continue;
            if(!hottestMaterial || (hottestMaterial && holdingMaterials[i].heatPercentage > hottestMaterial.heatPercentage))
            {
                hottestMaterial = holdingMaterials[i];
                materialIndex = i;
            }                
        }
        if (hottestMaterial)
        {
            hottestMaterial.transform.SetParent(null);
            holdingMaterials[materialIndex] = null;
        }
        return hottestMaterial;
    }

    public override bool CanHoldMaterial()
    {
        for(int i = 0; i < holdingMaterials.Length; i++)
        {
            if (!holdingMaterials[i]) return true;
        }
        return false;
    }
    public override bool CanRemoveMaterial()
    {
        for (int i = 0; i < holdingMaterials.Length; i++)
        {
            if (holdingMaterials[i]) return true;
        }
        return false;
    }
}
