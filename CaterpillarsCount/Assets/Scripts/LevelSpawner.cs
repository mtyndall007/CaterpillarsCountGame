using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LevelSpawner : MonoBehaviour
{
    

    public static string[] SpawnScenes()
    {
        //Find scenes in the level 1 folder
        DirectoryInfo dir = new DirectoryInfo("Assets/Scenes/Level1");
        FileInfo[] info = dir.GetFiles("*.unity");
        
        //Array for scene names
        string[] returnArray = new string[info.Length];

        //Add each scene name into a list to that we can display in a random order
        List<string> tmp = new List<string>();
        foreach (FileInfo filename in info)
        {
            string name = Path.GetFileNameWithoutExtension(filename.ToString());
            tmp.Add(name);
        }

        //Add scenes to the return array in a random order
        for (int i = 0; i < returnArray.Length; i++)
        {
            int r = Random.Range(0, tmp.Count-1);
            returnArray[i] = tmp[r];
            tmp.RemoveAt(r);
        }

        return returnArray;
      
    }
}
