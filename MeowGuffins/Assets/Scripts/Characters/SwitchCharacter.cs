using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCharacter : MonoBehaviour
{

    public GameObject inActiveGameObjects;
    public GameObject currentGameOject;
    public CinemachineVirtualCamera VirtualCamera;
    private GameObject temp;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void testSwitchCharacter()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            inActiveGameObjects.SetActive(true);
            currentGameOject.SetActive(false);
            VirtualCamera.m_Follow = inActiveGameObjects.transform;
        }
    }

    // Update is called once per frame
    [System.Obsolete]
    void Update()
    {
        testSwitchCharacter();
        if (!currentGameOject.active)
        {
            temp = currentGameOject;
            currentGameOject = inActiveGameObjects;
            inActiveGameObjects = temp;
        }
    }
}
