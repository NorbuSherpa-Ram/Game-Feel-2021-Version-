using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Discoball : MonoBehaviour,IHitable
{
    [SerializeField] EntityFX myFx;

    public void OnGetHit(int _dmageAmount, int _knockDirection)
    {
        myFx.Flash();
        DiscoballManager.instance.TriggerDiscoParty();
    }
}
