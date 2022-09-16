using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingMaterial : MonoBehaviour
{
    public Renderer render;

    [Header("Heat")]
    private Color baseColor;
    public Color fullHeatColor;

    public float heatValue;
    public float maxHeat;
    [Range(0,1)]public float heatLose;    
    public float heatPercentage => heatValue / maxHeat;


    private void Awake()
    {
        baseColor = render.material.color;
    }

    public void Update()
    {
        heatValue -= Time.deltaTime * heatLose; 
        if(heatValue < 0) heatValue = 0;
        SetColor();
    }

    public void Heat()
    {
        if (heatValue >= maxHeat)
        {
            heatValue = maxHeat;
            return;
        }
        heatValue += Time.deltaTime;
        heatValue += Time.deltaTime * heatLose;
        SetColor();
    }

    private void SetColor()
    {
        render.material.color = Color.Lerp(baseColor, fullHeatColor, heatPercentage * heatPercentage);
    }
}