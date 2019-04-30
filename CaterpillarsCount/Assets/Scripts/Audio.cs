using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    bool timerStart;

    public AudioSource ClickingSource;

    public AudioClip ClickingClip;
  

    // Start is called before the first frame update
    void Start()
    {
        timerStart = false;

        ClickingSource.clip = ClickingClip;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ClickingSource.Play();
        }

    }
}
