using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Rendering;

public class Interactable : MonoBehaviour
{
    public List<GameObject> ToDisable = new List<GameObject>();
    public List<GameObject> ToEnable = new List<GameObject>();
    public string ArmAnimName;

}
