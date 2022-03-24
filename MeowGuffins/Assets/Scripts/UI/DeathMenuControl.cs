using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathMenuControl : MonoBehaviour
{
    public AudioSource gameOver;

    private void OnEnable()
    {
        Time.timeScale = 0f;
        gameOver.Play();
    }
}
