using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeowArcherBehaviour : MonoBehaviour
{
    private Animator animator;
    float nextAnimTime = 0f;
    float frameRate = 2f;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void takeDamage()
    {
        if (Time.time >= nextAnimTime)
        {
            animator.SetTrigger("take_damage");
            nextAnimTime = Time.time + 1f / frameRate;
        }
    }

    public void onDeath()
    {
        this.gameObject.SetActive(false);
    }
}
