
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BranchScript : MonoBehaviour
{

    public GameObject branch;
    public float inGameWidth;
    public int widthInMM;

    private float millimetersToInGameUnits;

    // Start is called before the first frame update
    void Start()
    {
        initializeUnits();
        GameObject temp = GameObject.Find("Ruler");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public static void spawnBugs(int bugCount)
    {
        for (int i = 0; i < bugCount; i++)
        {

        }
    }

    private void initializeUnits()
    {
        branch = gameObject;
        SpriteRenderer temp = branch.GetComponentInChildren<SpriteRenderer>();
        Sprite tempSprite = temp.sprite;
        if (tempSprite)
        {
            inGameWidth = tempSprite.rect.width;
            millimetersToInGameUnits = widthInMM / inGameWidth;
        }
        else
        {
            Debug.Log("Need a branch sprite");
        }
    }

}