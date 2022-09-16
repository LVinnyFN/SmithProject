using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialHeater : MaterialHolder
{
    private void Update()
    {
        for(int i = 0; i < holdingMaterials.Length; i++)
        {
            if (holdingMaterials[i]) holdingMaterials[i].Heat();
        }
    } 
}
