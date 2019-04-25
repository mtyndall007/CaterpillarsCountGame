using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class PrefabGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(PrefabUtility.SaveAsPrefabAsset(new GameObject(), GetPrefabPath("Test")));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private static string GetPrefabPath(string path)
    {
        return "Assets/Prefabs/" + path + ".prefab";
    }
}
