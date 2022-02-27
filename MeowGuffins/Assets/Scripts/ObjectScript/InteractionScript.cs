using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InteractionScript : MonoBehaviour
{
    [SerializeField]
    private bool m_isInRange = false;

    public UnityEvent interactAction;

    public void OnInteractionPress(InputAction.CallbackContext context)
    {
        if (m_isInRange)
        {
            if (context.performed)
            {
                interactAction.Invoke();
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            m_isInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        m_isInRange = false;
    }

}
