using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialManipulator : MonoBehaviour
{
    public virtual void HoldMaterial(CraftingMaterial material) { }
    public virtual CraftingMaterial RemoveMaterial() { return null; }
    public virtual bool CanHoldMaterial () { return false; }
    public virtual bool CanRemoveMaterial () { return true; }
}