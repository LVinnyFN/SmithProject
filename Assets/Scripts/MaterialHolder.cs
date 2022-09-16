using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialHolder : MaterialManipulator
{
    public Transform[] materialSlots;
    public bool changeMaterialRotation;
    protected CraftingMaterial[] holdingMaterials;

    public virtual void Awake()
    {
        holdingMaterials = new CraftingMaterial[materialSlots.Length];
    }

    public override void HoldMaterial(CraftingMaterial material)
    {
        for (int i = 0; i < holdingMaterials.Length; i++)
        {
            if (holdingMaterials[i]) continue;

            holdingMaterials[i] = material;
            holdingMaterials[i].transform.SetParent(materialSlots[i]);
            holdingMaterials[i].transform.localPosition = Vector3.zero;
            if (changeMaterialRotation) holdingMaterials[i].transform.localRotation = Quaternion.identity;
            return;
        }
    }
    public override CraftingMaterial RemoveMaterial()
    {
        CraftingMaterial hottestMaterial = null;
        int materialIndex = -1;
        for (int i = 0; i < holdingMaterials.Length; i++)
        {
            if (!holdingMaterials[i]) continue;
            if (!hottestMaterial || (hottestMaterial && holdingMaterials[i].heatPercentage > hottestMaterial.heatPercentage))
            {
                hottestMaterial = holdingMaterials[i];
                materialIndex = i;
            }
        }
        if (hottestMaterial)
        {
            holdingMaterials[materialIndex] = null;
        }
        return hottestMaterial;
    }

    public override bool CanHoldMaterial()
    {
        for (int i = 0; i < holdingMaterials.Length; i++)
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
