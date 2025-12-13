using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Rendering;

public class Interactable : MonoBehaviour
{
    public List<GameObject> ToDisable = new List<GameObject>();
    public List<GameObject> ToEnable = new List<GameObject>();
    public List<Component> ComponentToEnable = new List<Component>();
    public List<Component> ComponentToDisable = new List<Component>();
    public string ArmAnimName;

    public float timeActive;
}
