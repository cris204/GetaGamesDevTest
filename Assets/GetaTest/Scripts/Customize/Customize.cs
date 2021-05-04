using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customize : MonoBehaviour
{
    [Header("Color")]
    public Color[] colors;

    [Header("Chasis")]
    public Material chassisMaterial;
    private int chassisColorIndex;

    [Header("Wheels")]
    public Material wheelsMaterial;
    private int wheelsColorIndex;

    [Header("Player")]
    public Material playerMaterial;
    private int playerColorIndex;

    public void ChangeChasisMaterial()
    {
        ChangeMaterial(chassisMaterial, chassisColorIndex);
        chassisColorIndex++;
        chassisColorIndex = chassisColorIndex % colors.Length;
    }

    public void ChangeWheelsMaterial()
    {
        ChangeMaterial(wheelsMaterial, wheelsColorIndex);
        wheelsColorIndex++;
        wheelsColorIndex = wheelsColorIndex % colors.Length;
    }
    public void ChangePlayerMaterial()
    {
        ChangeMaterial(playerMaterial, playerColorIndex);
        playerColorIndex++;
        playerColorIndex = playerColorIndex % colors.Length;
    }

    public void ChangeMaterial(Material material, int index)
    {
        material.SetColor("_Color", colors[index]);

    }

}
