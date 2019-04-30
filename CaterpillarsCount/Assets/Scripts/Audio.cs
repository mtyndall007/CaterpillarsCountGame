﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{

    public AudioClip ClickingClip;
    public AudioSource ClickingSource;

    // Start is called before the first frame update
    void Start()
    {
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
