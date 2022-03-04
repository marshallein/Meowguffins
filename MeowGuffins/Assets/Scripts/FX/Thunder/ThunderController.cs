using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderController : MonoBehaviour
{

    public void OnAnimationIsDone()
    {
        this.gameObject.SetActive(false);
    }



}
