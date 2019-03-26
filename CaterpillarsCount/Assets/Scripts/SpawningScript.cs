using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int numOfDesiredBugs = 6;

        int removeCount = transform.childCount - numOfDesiredBugs;
        HashSet<Transform> alreadyRemoved = new HashSet<Transform>();

        for (int i = 0; i < removeCount; i++)
        {
            int childToRemove = Random.Range(0, transform.childCount-1);

            Transform child = transform.GetChild(childToRemove);

            while(alreadyRemoved.Contains(child))
            { 
                childToRemove = Random.Range(0, transform.childCount - 1);
                child = transform.GetChild(childToRemove);
            }

            Destroy(child.gameObject);
            alreadyRemoved.Add(child);


        }

    }

  
}
