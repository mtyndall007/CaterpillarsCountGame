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


        playerScoreText = GameObject.Find("UserScore").GetComponent<Text>();
        playerScoreText.text = "Your Score: " + ScoreScript.levelScore;
        maxScoreText = GameObject.Find("MaxScore").GetComponent<Text>();
        maxScoreText.text = "Maximum Score: " + GameManager.instance.levelScore;
    }

    private void continueFunction(){
        ScoreScript.ResetScore();
        SceneManager.LoadScene(GameManager.instance.sceneIterator);
    }


}
