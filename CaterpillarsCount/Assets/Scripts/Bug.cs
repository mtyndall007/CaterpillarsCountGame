using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEditor;

//Generic script for bugs. Can be used as a generic parent class for more specific bugs, but not sure if it's necessary yet.
//Public variables so that we can tweak what color each bug is, their point value, etc.

[System.Serializable]
public class BugClickedEvent : UnityEvent<GameObject>
{
}

public class Bug : MonoBehaviour
{
    public GameObject bug;
    private bool clickable;
    public bool paused;
    public Color defaultColor;
    public int points;
    public string classification;
    public float lengthInMM;
    public BugClickedEvent bugClicked;

    // Start is called before the first frame update
    void Start()
    {
        bug = gameObject;
        clickable = true;
        paused = false;

        //Will need something like this eventually, but also need to scale based on branch size
          //lengthInMM = lengthInMM * bug.transform.localScale.x;
          //Debug.Log(lengthInMM);

        if (bugClicked == null)
            bugClicked = new BugClickedEvent();

        bugClicked.AddListener(GameManager.instance.bugClicked);

        adjustForBug();
    }

    // Update is called once per frame
    void Update()
    {

        checkForClick();

    }

    private void adjustForBug()
    {
        SpriteRenderer spriteRenderer = bug.GetComponent<SpriteRenderer>();
        string name = "";
        string[] names = { "Caterpillar", "Ant", "Ladybug" };
        classification = names[Random.Range(0, 3)];
        switch (classification)
        {
            case ("Caterpillar"):
                name = "Caterpillar.JPG";
                lengthInMM = 2;
                break;
            case "Ant":
                name = "Ant.png";
                lengthInMM = 3;
                break;
            case "Ladybug":
                name = "Ladybug.png";
                lengthInMM = 4;
                break;
        }
        spriteRenderer.sprite = (Sprite)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Bugs/" + name, typeof(Sprite));
    } 

    public void PauseBug()
    {
        paused = true;
    }

    public void ResumeBug()
    {
        paused = false;
    }

    public void SetCorrectColor()
    {
        SpriteRenderer spriteRenderer = bug.GetComponent<SpriteRenderer>();
        spriteRenderer.color = Color.blue;
    }

    public void SetIncorrectColor()
    {
        SpriteRenderer spriteRenderer = bug.GetComponent<SpriteRenderer>();
        spriteRenderer.color = Color.red;
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
                //Send game object to the game manager so it has a reference
                bugClicked.Invoke(bug);

                ScoreScript.scoreValue += points;
                clickable = false;

            }
        }
    }
}
