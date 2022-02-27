using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrankController : MonoBehaviour
{
    public bool isOn;
    public Animator animator;

    private GameObject[] m_Barrier;

    public void OnCrankTurnOn()
    {
        isOn = true;
        if (isOn)
        {
            animator.SetBool("isOn", true);
            if (m_Barrier == null)
            {
                m_Barrier = GameObject.FindGameObjectsWithTag("Barrier");
                foreach (var barrier in m_Barrier)
                {
                    if (barrier != null)
                    {
                        barrier.SetActive(false);
                    }
                }
            }
        }
    }
}
