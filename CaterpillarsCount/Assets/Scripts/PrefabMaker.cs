using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabMaker : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        TextAsset bugData = Resources.Load<TextAsset>("bugData.csv");
        Debug.Log("--");
        Debug.Log(bugData);
        Debug.Log("--");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
