using System.Collections;
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
    
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of game manager found!");
            return;
        }
        instance = this;
    }
    #endregion

    public static GameManager instance;

    private UnityAction submitAction;
    private UnityAction playAgainAction;
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

    private int sceneIterator;

    // Start is called before the first frame update
    void Start()
    {
        //Idea: call a utility method that selects the level for a given playthrough. Store these in an array of scenes.
        string[] spawnedScenes = LevelSpawner.SpawnScenes();
        sceneIterator = 0;
        //SceneManager.LoadScene(spawnedScenes[sceneIterator]);

        Awake();

        returnObject = GameObject.Find("Return");
        returnObject.SetActive(false);

        defaultFOV = Camera.main.fieldOfView;
        zoomedFOV = 40f;

        //Finds the submit button from the scene and adds an event listener
        //submitButton = GetComponentInChildren<Button>();
        submitButton = GameObject.Find("Submit").GetComponent<Button>();
        submitAction += Submit;
        submitButton.onClick.AddListener(submitAction);

        bugClicked += BugClicked;

       //Find the gameover UI
       gameOver = GameObject.Find("GameOver");
       //Make the gameover screen invisible
       gameOver.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Submit()
    {
        
        playerScore = ScoreScript.scoreValue;
        totalScore = calcTotalScore();

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
        
        //SceneManager.LoadScene("Test");

    }

    void PlayAgain()
    {
        //Simply resets the scene for now
        ScoreScript.scoreValue = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private int calcTotalScore()
    {
        int tempScore = 0;
        Bug[] bugs = GameObject.FindObjectsOfType<Bug>();
        foreach (Bug bug in bugs) {
            Debug.Log(bug);
            tempScore += bug.points;
        }
        return tempScore;
    }

    public void BugClicked()
    {
      
        Debug.Log("Bug clicked");
        Camera.main.fieldOfView = zoomedFOV;

        //Finds the submit button from the scene and adds an event listener
        returnObject.SetActive(true);

        returnButton = returnObject.GetComponent<Button>();
        
        returnZoom += ReturnFromClick;
        returnButton.onClick.AddListener(returnZoom);
    }

    private void ReturnFromClick()
    {
        Camera.main.fieldOfView = defaultFOV;

    }
    
}
