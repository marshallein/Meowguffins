using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCharacter : MonoBehaviour
{

    public void testSwitchCharacter()
    {
        MeowObjectManager.Instance.Switch();
    }


    // Update is called once per frame
    [System.Obsolete]
    void Update()
    {
        testSwitchCharacter();
    }
}
