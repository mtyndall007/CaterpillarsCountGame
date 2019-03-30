using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningScript : MonoBehaviour
{

    //Lets you adjust how many bugs you want on that branch through inspector
    public int numOfDesiredBugs;

    void Start()
    {

        //keeps track of which bugs already removed
        HashSet<Transform> alreadyAdded = new HashSet<Transform>();
        Transform spawnedBugs = GameObject.Find("SpawnedBugs").transform;


        //For loop to removed excess bugs

        for (int i = 0; i < numOfDesiredBugs; i++)
        {
            //randomly picks a spawnPoint
            int point = Random.Range(0, transform.childCount);
            Transform spawnPoint = transform.GetChild(point);

            //while loop checks and see if that child has already been deleted
            while(alreadyAdded.Contains(spawnPoint))
            {
                point = Random.Range(0, transform.childCount);
                spawnPoint = transform.GetChild(point);
            }

            //adds it to Hashset
            alreadyAdded.Add(spawnPoint);

            //gets bug from that spawn point
            int bugIndex = Random.Range(0, spawnPoint.childCount);
            Transform bug = spawnPoint.GetChild(bugIndex);

            //duplicates it and adds it to the spawnBugs 
            Transform duplicate = Instantiate(bug);
            Vector3 bugPosition = spawnPoint.position + duplicate.position;

            duplicate.transform.position = bugPosition;
            duplicate.gameObject.SetActive(true);

            duplicate.parent = spawnedBugs;

        }

    }

  
}
