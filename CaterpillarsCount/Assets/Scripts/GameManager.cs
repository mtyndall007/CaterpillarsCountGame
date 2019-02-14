using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

//Script for controlling levels, scores, etc. 
//Has the UI as a child object

public class GameManager : MonoBehaviour
{

    private UnityAction submitAction;
    Button submitButton;
    private int totalScore;

    GameObject gameover;

    // Start is called before the first frame update
    void Start()
    {
        submitButton = GetComponentInChildren<Button>();
        submitAction += Submit;
        submitButton.onClick.AddListener(submitAction);


       gameover = GameObject.Find("GameOver");
        //Make the gameover screen invisible
       gameover.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Submit()
    {
        Debug.Log("Button Pressed");
        totalScore = ScoreScript.scoreValue;
        Bug[] bugs = GameObject.FindObjectsOfType<Bug>();

        GameObject mainInterface = GameObject.Find("LevelUI");
        mainInterface.SetActive(false);


        //Make the gameover screen visible
        gameover.SetActive(true);

        Text scoreText = GameObject.Find("YourScore").GetComponent<Text>();
        scoreText.text += totalScore.ToString(); 

    }
    
}
