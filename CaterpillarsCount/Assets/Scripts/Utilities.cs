﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Utilities : MonoBehaviour
{

    public static void ScaleRuler(GameObject ruler, Bug bug){
        RectTransform bugRect = bug.GetComponent<RectTransform>();
        float bugImageWidthInGame = bugRect.rect.width / bug.lengthAsProportionOfImageWidth;
        Debug.Log("bugImageWidthInGame" + bugImageWidthInGame);
        float bugImageWidthInMM = bugImageWidthInGame * bug.lengthInMM;
        Debug.Log("bugImageWidthInMM" + bugImageWidthInMM);

        float bugToRulerRatio = bugImageWidthInMM / 35; //Hardcoded ruler length
        Debug.Log("bugToRulerRatio" + bugToRulerRatio);
        float tempWidth = (ruler.GetComponent<RectTransform>().rect.width / bugToRulerRatio);
        Debug.Log("tempWidth" + tempWidth);
        ruler.GetComponent<RectTransform>().sizeDelta = new Vector2(tempWidth, ruler.GetComponent<RectTransform>().rect.height);
    }

    //Pause all bugs so they are unclickable
    public static void PauseBugs()
    {
        Bug[] bugs = GameObject.FindObjectsOfType<Bug>();
        foreach (Bug bug in bugs)
        {
            bug.PauseBug();
        }

    }

    //Resume bugs so that those that haven't been found are clickable
    public static void ResumeBugs()
    {
        Bug[] bugs = GameObject.FindObjectsOfType<Bug>();
        foreach (Bug bug in bugs)
        {
            bug.ResumeBug();
        }

    }

    //Turn bugs white when not found, input how long to show the bugs
    public static IEnumerator HighlightUnfoundBugs(float delay)
    {
        bool unfoundBugs = false;
        Bug[] bugs = GameObject.FindObjectsOfType<Bug>();
        if(bugs.Length != 0){
          foreach (Bug bug in bugs)
          {
              if(bug.isClickable()){
                unfoundBugs = true;
                SpriteRenderer spriteRenderer = bug.GetComponent<SpriteRenderer>();
                spriteRenderer.color = Color.white;
              }
          }
        }
        yield return new WaitForSeconds(delay);
    }

    public static void ScaleBug(BranchScript branch, GameObject bugObject){

      Bug bug = bugObject.GetComponent<Bug>();
      bugObject.transform.localScale = new Vector3(1.15f, 1.15f, 1.15f);
      RectTransform rectT = bugObject.AddComponent(typeof(RectTransform)) as RectTransform;
      // = bugObject.GetComponent<RectTransform>();

      GameObject branchObject = GameObject.Find("Branch");
      //branchObject.transform.localScale = new Vector3(1, 1, 1);

      //RectTransform branchRect = branchObject.GetComponent<RectTransform>();
      //rectT.sizeDelta = new Vector2(12, 9);


      bug.lengthInMM = RandomBugLength(bug);
      float bugLength = bug.lengthInMM;

      float scaleRatio = (bugLength/branch.branchWidthInMM) * 4;



      Vector3 scaleFactor = new Vector3(scaleRatio / bugObject.transform.localScale.x  , scaleRatio / bugObject.transform.localScale.y, bugObject.transform.localScale.z);
      Debug.Log(scaleRatio);

      Transform branchTransform = branch.gameObject.transform;
      bugObject.transform.localScale = scaleFactor;//branchTransform.localScale.x * scaleRatio;


    }

    private static float RandomBugLength(Bug bug){
      float desired = bug.desiredLengthInMM;
      float max = bug.maxLengthInMM;
      float min = bug.minLengthInMM;

      float lower = Random.Range(min, desired);
      float upper = Random.Range(desired, max);

      return (lower+upper)/2;
    }


    //Creates a popup message, run it via a coroutine
    public static IEnumerator PopupMessage(string message, float delay)
    {
        Text popupText;

        #region Text Object Scripting
        // Load the Arial font from the Unity Resources folder.
        Font arial;
        arial = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");

        // Create Canvas GameObject.
        GameObject canvasGO = new GameObject();
        canvasGO.name = "Canvas";
        canvasGO.AddComponent<Canvas>();
        canvasGO.AddComponent<CanvasScaler>();
        canvasGO.AddComponent<GraphicRaycaster>();

        // Get canvas from the GameObject.
        Canvas canvas;
        canvas = canvasGO.GetComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;

        // Create the Text GameObject.
        GameObject textGO = new GameObject();
        textGO.transform.parent = canvasGO.transform;
        textGO.AddComponent<Text>();

        // Set Text component properties.
        popupText = textGO.GetComponent<Text>();
        popupText.font = arial;
        popupText.text = message;
        popupText.fontSize = 48;
        popupText.alignment = TextAnchor.MiddleCenter;

        // Provide Text position and size using RectTransform.
        RectTransform rectTransform;
        rectTransform = popupText.GetComponent<RectTransform>();
        rectTransform.localPosition = new Vector3(0, 0, 0);
        rectTransform.sizeDelta = new Vector2(600, 200);
        #endregion

        //popupText.text = message;
        popupText.enabled = true;
        yield return new WaitForSeconds(delay);
        popupText.enabled = false;
    }
}
