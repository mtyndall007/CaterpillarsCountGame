﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

//Script for controlling levels, scores, etc. 
//Has the UI as a child object

public class GameManager : MonoBehaviour
{

    //An instance of the game manager that can be invoked. Should only be one instance at a time
    #region Singleton
    public static GameManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //Calls a utility method that selects the level for a given playthrough. Store these in an array of scenes.
            spawnedScenes = LevelSpawner.SpawnScenes();
            sceneIterator = 0;
            SceneManager.LoadScene(spawnedScenes[sceneIterator]);
        }
        //If instance already exists and it's not this:
        else if (instance != this)
        {
            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);
        }         

        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);
    }
    #endregion

    private string[] spawnedScenes;
    private int sceneIterator;

    private UnityAction submitAction;
    private UnityAction playAgainAction;
    private UnityAction returnAction;
    public UnityAction bugClicked;
    public UnityAction returnZoom;

    Button submitButton;
    Button playAgainButton;
    Button returnButton;

    private int playerScore;
    private int totalScore;

    private float defaultFOV;
    private float zoomedFOV;

    GameObject gameOver;
    GameObject returnObject;
    GameObject bugSelectionUI;


    // Start is called before the first frame update
    void Start()
    {

        returnObject = GameObject.Find("Return");
        returnObject.SetActive(false);

        defaultFOV = Camera.main.fieldOfView;
        zoomedFOV = 40f;

        //Finds the submit button from the scene and adds an event listener
        submitButton = GameObject.Find("Submit").GetComponent<Button>();
        submitAction += Submit;
        submitButton.onClick.AddListener(submitAction);

        bugClicked += BugClicked;

       //Find the gameover UI
       gameOver = GameObject.Find("GameOver");
       //Make the gameover screen invisible
       gameOver.SetActive(false);

       bugSelectionUI = GameObject.Find("BugSelectionUI");
       bugSelectionUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(TimerScript.GetCurrentTime() <= 0)
        {
            Submit();
        }
    }

    void Submit()
    {
        //Iterate to get the next scene
        sceneIterator++;
        //Score is persistant between levels for now, but might want to change this
        totalScore += calcTotalScore();

        TimerScript.SetCurrentTime(80);

        //If we're on the last level, display the game over screen. Otherwise go to next level
        if (sceneIterator == spawnedScenes.Length)
        {
            playerScore = ScoreScript.scoreValue;           

            //Hide the game interface
            GameObject mainInterface = GameObject.Find("LevelUI");
            mainInterface.SetActive(false);

            //Make the gameover screen visible
            gameOver.SetActive(true);

            //Update the score value and display it to the game over screen
            Text scoreText = GameObject.Find("YourScore").GetComponent<Text>();
            scoreText.text += playerScore.ToString();

            Text totalScoreText = GameObject.Find("TotalScore").GetComponent<Text>();
            totalScoreText.text += totalScore.ToString() + " possible points";

            //Finds the play again button from the scene and adds an event listener
            playAgainButton = GetComponentInChildren<Button>();
            playAgainAction += PlayAgain;
            playAgainButton.onClick.AddListener(playAgainAction);
        }
        else
        {
            SceneManager.LoadScene(spawnedScenes[sceneIterator]);
        }

        
    }

    void PlayAgain()
    {
        //Resets the score and goes back to the first scene. 
        //Creates new instance of the game manager. Levels should be random again on replay
        ScoreScript.scoreValue = 0;
        Destroy(gameObject);
        SceneManager.LoadScene(spawnedScenes[0]);
    }

    private int calcTotalScore()
    {
        int tempScore = 0;
        Bug[] bugs = GameObject.FindObjectsOfType<Bug>();
        foreach (Bug bug in bugs) {
            tempScore += bug.points;
        }
        return tempScore;
    }

    public void BugClicked()
    {

        //Zooms camera in on bug
        //Camera.main.orthographic = true;
        //Camera.main.orthographicSize = Camera.main.orthographicSize / 5.0f;

        bugSelectionUI.SetActive(true);
        submitButton.gameObject.SetActive(false);

        returnObject.SetActive(true);
        returnButton = returnObject.GetComponent<Button>();
        returnAction += ReturnFromClick;
        returnButton.onClick.AddListener(returnAction);
    }


    //Can be used for return button as well as after submitting a bug
    private void ReturnFromClick()
    {
        Camera.main.fieldOfView = defaultFOV;
        bugSelectionUI.SetActive(false);
        submitButton.gameObject.SetActive(true);
        returnObject.SetActive(false);
    }

    public static void TimerSubmit() => GameManager.instance.Submit();

}
