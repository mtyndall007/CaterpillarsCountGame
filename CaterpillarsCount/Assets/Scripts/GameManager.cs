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
    public static GameManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //Calls a utility method that selects the level for a given playthrough. Store these in an array of scenes.
            spawnedScenes = LevelSpawner.SpawnScenes();
            Debug.Log(spawnedScenes.Length);
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
        sceneIterator++;
        totalScore += calcTotalScore();

        if (sceneIterator == spawnedScenes.Length)
        {
            playerScore = ScoreScript.scoreValue;           

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
        //Simply resets the scene for now
        ScoreScript.scoreValue = 0;
        Destroy(gameObject);
        SceneManager.LoadScene(spawnedScenes[0]);
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
