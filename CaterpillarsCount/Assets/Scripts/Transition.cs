using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;


public class Transition : MonoBehaviour
{
    Text playerScoreText;
    Text maxScoreText;
    Text bugsClickedText;
    Text bugsIdentifiedText;
    Text measurementAccuracyText;

    Button continueButton;
    UnityAction continueAction;

    int playerScore;
    int maxScore;
    // Start is called before the first frame update
    void Start()
    {
        continueButton = GameObject.Find("Continue").GetComponent<Button>();
        continueAction += continueFunction;
        continueButton.onClick.AddListener(continueAction);

        bugsClickedText = GameObject.Find("BugsClicked").GetComponent<Text>();

        bugsIdentifiedText = GameObject.Find("BugsIdentified").GetComponent<Text>();

        measurementAccuracyText = GameObject.Find("MeasurementAccuracy").GetComponent<Text>();
        if(GameManager.instance.bugsClicked > 0){
          bugsClickedText.text = "Arthropods found: " + Mathf.Round(100 * (float)GameManager.instance.bugsClicked/GameManager.instance.totalBugs) + "%";
          bugsIdentifiedText.text = "Correctly identified: " + Mathf.Round(100 * (float)GameManager.instance.bugsCorrectlyIdentified/GameManager.instance.totalBugs) + "%";
          measurementAccuracyText.text = "Measurement error: " + (float)(GameManager.instance.measurementDistance / GameManager.instance.bugsClicked) + "mm";

        } else {
          bugsClickedText.text = "";
          bugsIdentifiedText.text = "";
          measurementAccuracyText.text = "No bugs were clicked on!";
        }

        playerScoreText = GameObject.Find("UserScore").GetComponent<Text>();
        playerScoreText.text = "Your Score: " + ScoreScript.levelScore + "/" + GameManager.instance.levelScore;

    }

    private void continueFunction(){
        GameManager.instance.ResetBugCounts();
        ScoreScript.ResetScore();
        //SceneManager.LoadScene(GameManager.instance.sceneIterator);
        SceneManager.LoadScene(GameManager.instance.levelSelector(GameManager.instance.sceneIterator));
    }


}
