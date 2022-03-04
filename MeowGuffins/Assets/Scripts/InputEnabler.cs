using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputEnabler : MonoBehaviour
{
    void Start()
    {
        GetComponent<PlayerInput>().enabled = true;
        print(GetComponent<PlayerInput>().enabled);
    }
}
