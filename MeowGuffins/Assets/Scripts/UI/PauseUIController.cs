using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseUIController : MonoBehaviour
{
    public string mainMenu;
    public GameObject PausePanel;

    public void PauseGame()
    {
        Time.timeScale = 0f;
        PausePanel.SetActive(true);
    }
    public void Resume()
    {
        Time.timeScale = 1f;
        PausePanel.SetActive(false);
    }
    public void ReturnToCharacterSelect()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(mainMenu);
    }
}
