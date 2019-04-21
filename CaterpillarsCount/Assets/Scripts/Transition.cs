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
        bugsClickedText.text = "You found " + Mathf.Round(100 * (float)GameManager.instance.bugsClicked/GameManager.instance.totalBugs) + "% of the rthropods";
        bugsIdentifiedText = GameObject.Find("BugsIdentified").GetComponent<Text>();
        bugsIdentifiedText.text = "You correctly identified " + Mathf.Round(100 * (float)GameManager.instance.bugsCorrectlyIdentified/GameManager.instance.totalBugs) + "% of the arthropods";
        measurementAccuracyText = GameObject.Find("MeasurementAccuracy").GetComponent<Text>();
        measurementAccuracyText.text = "Your average measurement error was " + (float)(GameManager.instance.measurementDistance / GameManager.instance.bugsClicked) + "mm";
        playerScoreText = GameObject.Find("UserScore").GetComponent<Text>();
        playerScoreText.text = "Your Score: " + ScoreScript.levelScore;
        maxScoreText = GameObject.Find("MaxScore").GetComponent<Text>();
        maxScoreText.text = "Maximum Score: " + GameManager.instance.levelScore;
    }

    private void continueFunction(){
        GameManager.instance.ResetBugCounts();
        ScoreScript.ResetScore();
        SceneManager.LoadScene(GameManager.instance.sceneIterator);
    }


}
