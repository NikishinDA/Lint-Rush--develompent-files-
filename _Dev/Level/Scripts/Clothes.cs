using System;
using System.Collections;
using System.Collections.Generic;
using _Internal._Dev.Management.Scripts;
using UnityEngine;

public class Clothes : MonoBehaviour
{
    private void Start()
    {
        if (VarSaver.Clothless)
            GetComponent<MeshRenderer>().enabled = false;
    }
}
