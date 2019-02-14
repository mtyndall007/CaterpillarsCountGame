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

    private UnityAction submitAction;
    private UnityAction playAgainAction;
    Button submitButton;
    Button playAgainButton;

    private int playerScore;
    private int totalScore;

    GameObject gameOver;

    // Start is called before the first frame update
    void Start()
    {
        //Finds the submit button from the scene and adds an event listener
        submitButton = GetComponentInChildren<Button>();
        submitAction += Submit;
        submitButton.onClick.AddListener(submitAction);


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
    
}
