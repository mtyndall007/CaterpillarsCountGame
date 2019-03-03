using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//Generic script for bugs. Can be used as a generic parent class for more specific bugs, but not sure if it's necessary yet.
//Public variables so that we can tweak what color each bug is, their point value, etc.

[System.Serializable]
public class BugClickedEvent : UnityEvent<string>
{
}

public class Bug : MonoBehaviour
{
    public GameObject bug;
    public bool clickable;
    public bool paused;
    public Color defaultColor;
    public int points;
    public string classification;



    //UnityEvent<string> bugClicked;
    public BugClickedEvent bugClicked;

    // Start is called before the first frame update
    void Start()
    {
        clickable = true;
        paused = false;

        if (bugClicked == null)
            bugClicked = new BugClickedEvent();

        bugClicked.AddListener(GameManager.instance.bugClicked);
    }

    // Update is called once per frame
    void Update()
    {
        
        checkForClick();

    }

    public void PauseBug()
    {
        paused = true;
    }

    public void ResumeBug()
    {
        paused = false;
    }

    private void checkForClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 pointClicked = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D coll = bug.GetComponent<Collider2D>();

            if (coll.OverlapPoint(pointClicked) && clickable && !paused)
            {
                //Sends the bug name to game manager when clicked
                bugClicked.Invoke(classification);

                ScoreScript.scoreValue += points;
                clickable = false;
                SpriteRenderer spriteRenderer = bug.GetComponent<SpriteRenderer>();
                spriteRenderer.color = defaultColor;

            }
        }
    }
}
