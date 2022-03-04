using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInputManager : MonoBehaviour
{

    public void OnReadCharacterMove(InputAction.CallbackContext context)
    {
        print("move");
        MeowObjectManager.Instance.ActiveMeow.OnReadCharacterMove(context);
    }

    public void OnCharacterJump(InputAction.CallbackContext context)
    {
        print("Jump");
        MeowObjectManager.Instance.ActiveMeow.OnCharacterJump(context);
    }

    public void OnCharacterDodge(InputAction.CallbackContext context)
    {
        MeowObjectManager.Instance.ActiveMeow.OnCharacterDodge(context);
    }

    public virtual void OnCharacterAttack(InputAction.CallbackContext context)
    {
        MeowObjectManager.Instance.ActiveMeow.OnCharacterAttack(context);
    }

    public virtual void OnCharacterAttack2(InputAction.CallbackContext context)
    {
        MeowObjectManager.Instance.ActiveMeow.OnCharacterAttack2(context);
    }

    public virtual void OnCharacterAttack3(InputAction.CallbackContext context)
    {
        MeowObjectManager.Instance.ActiveMeow.OnCharacterAttack3(context);
    }

    public virtual void OnCharacterAttack4(InputAction.CallbackContext context)
    {
        MeowObjectManager.Instance.ActiveMeow.OnCharacterAttack4(context);
    }

    public void testSwitchCharacter(InputAction.CallbackContext context)
    {
        MeowObjectManager.Instance.Switch();
    }

    public void Test()
    {
        print("test");
    }
}
