﻿
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour
{
    [SerializeField] private float startTime = 100;
    Text uiText;
    private static float currentTime;

    // Start is called before the first frame update
    void Start()
    {
        uiText = GetComponent<Text>();
        currentTime = startTime; 
    }

    // Update is called once per frame
    void Update()
    {
        if(currentTime > 0)
        {
            currentTime -= 1 * Time.deltaTime;
            int truncatedTime = Mathf.RoundToInt(currentTime);

            uiText.text = "Time: " + truncatedTime.ToString();
        }
        else
        {
            //GameManager.TimerSubmit();
        }
    }

    public static int GetCurrentTime()
    {
        return Mathf.RoundToInt(currentTime);
    } 

    public static void SetCurrentTime(int time)
    {
        currentTime = time;
    }
}
