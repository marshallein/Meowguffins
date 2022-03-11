using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameItemScript : MonoBehaviour
{
    public GameObject spike;

    public void ObtainTheSword()
    {
        Debug.Log("get the s");
        if(spike != null)
        {
            if (spike.activeInHierarchy)
            {
                spike.SetActive(false);
            }
        }
    }
}
