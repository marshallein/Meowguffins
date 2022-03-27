using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    public GameObject currentObject;
    public GameObject gameCanvas;
    public void OnClick()
    {
        if(SelectionScript.SelectedCards.Count < 2)
        {
            return;
        }
        SceneManager.LoadScene("Scene1_Jungle");
    }

    public void Back()
    {
        currentObject.SetActive(false);
        gameCanvas.SetActive(true);
    }

    public void OnClickBossScene()
    {
        SceneManager.LoadScene("Scene2_Boss");
    }
}
