using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialDispenser : MaterialManipulator
{
    public CraftingMaterial materialPrefab;
    public override CraftingMaterial RemoveMaterial() => Instantiate(materialPrefab);
}