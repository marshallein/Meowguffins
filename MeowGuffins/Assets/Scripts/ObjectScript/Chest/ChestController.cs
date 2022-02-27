using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestController : MonoBehaviour
{

    public bool isOpen;
    public Animator animator;



    public void OpenChest()
    {
        isOpen = true;
        if (isOpen == true)
        {
            Debug.Log("Chest is open");
            animator.SetBool("isOpen", isOpen);
        }
    }
}
