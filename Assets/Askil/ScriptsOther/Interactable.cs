using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;


public class Interactable : MonoBehaviour
{
    public List<GameObject> ToDisable = new List<GameObject>();
    public List<GameObject> ToEnable = new List<GameObject>();
    public string PlayerAnimBool;
    public UnityEvent onEnable;

    public bool resetAfterTime = true;
    public float TimeBeforeReset;


    public bool canBeHeldInHand = false;

    public string hoverMessage = "E";

    public string heldInHandName = "Junk";
    public string heldInHandTossMessage = "Drop";
    public bool showUseMessage = false;
    public string heldInHandUseMessage = "Use";

    public bool throwFromCenterOfScreen = false;
    public float throwForce = 3.0f;

}
