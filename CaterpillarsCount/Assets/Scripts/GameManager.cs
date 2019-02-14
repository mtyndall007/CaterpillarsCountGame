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

        playAgainButton = GetComponentInChildren<Button>();
        playAgainAction += PlayAgain;
        playAgainButton.onClick.AddListener(playAgainAction);

    }

    void PlayAgain()
    {
        //Simply resets the scene for now
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
}
