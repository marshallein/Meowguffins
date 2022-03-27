using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuildeButton : MonoBehaviour
{
    public GameObject guildline;
    // Start is called before the first frame update
    public GameObject current;
    public void Onclick()
    {
        current.SetActive(false);
        guildline.SetActive(true);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
