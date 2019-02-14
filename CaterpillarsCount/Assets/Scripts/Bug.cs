using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Generic script for bugs. Can be used as a generic parent class for more specific bugs, but not sure if it's necessary yet.
//Public variables so that we can tweak what color each bug is, their point value, etc.

public class Bug : MonoBehaviour
{
    public GameObject bug;
    bool hasBeenClicked = false;
    public Color defaultColor;
    public int points;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        checkForClick();

    }

    private void checkForClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 pointClicked = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D coll = bug.GetComponent<Collider2D>();

            if (coll.OverlapPoint(pointClicked) && !hasBeenClicked)
            {
                ScoreScript.scoreValue += points;
                hasBeenClicked = true;
                SpriteRenderer spriteRenderer = bug.GetComponent<SpriteRenderer>();
                spriteRenderer.color = defaultColor;

            }
        }
    }
}
