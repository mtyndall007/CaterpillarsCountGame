using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningScript : MonoBehaviour
{

    //Lets you adjust how many bugs you want on that branch through inspector
    public int numOfBugs;

    void Start()
    {
        //GameObject holding spawned bugs
        Transform spawnedBugs = GameObject.Find("SpawnedBugs").transform;

        //keeps track of which bugs are already added to scene
        HashSet<Transform> alreadyAdded = new HashSet<Transform>();


        //Loop to add bugs
        for (int i = 0; i < numOfBugs; i++)
        {
            //randomly picks a spawnPoint
            int point = Random.Range(0, transform.childCount);
            Transform spawnPoint = transform.GetChild(point);

            //while loop checks if spawn point has been used
            //IF has been used, finds one that hasnt
            while(alreadyAdded.Contains(spawnPoint))
            {
                point = Random.Range(0, transform.childCount);
                spawnPoint = transform.GetChild(point);
            }

            //adds spawnpoint to hashset
            alreadyAdded.Add(spawnPoint);

            //randomly picks bug that can spawn from that spawn point
            int bugIndex = Random.Range(0, spawnPoint.childCount);
            Transform bug = spawnPoint.GetChild(bugIndex);

            //duplicates it and adds it to the spawnBugs 
            Transform newBug = Instantiate(bug);

            //sets the bugs position
            Vector3 bugPosition = spawnPoint.position + newBug.position;
            newBug.transform.position = bugPosition;
            newBug.gameObject.SetActive(true);

            //adds the bug to scene and makes it visible
            newBug.parent = spawnedBugs;
            newBug.gameObject.SetActive(true);
        }

    }

    //Getters and Setters
    public int getNumOfBugs() { return numOfBugs; }
    public void setNumOfBugs(int value) { numOfBugs = value; }

    //Sets the number of bugs based off the difficulty of the level
    public void setNumWithDifficulty(string difficulty)
    {
        if(difficulty == "Easy")
        {
            setNumOfBugs(3);

        }else if(difficulty == "Medium")
        {
            setNumOfBugs(4);
        }
        else
        {
            setNumOfBugs(5);
        }
    }




}
