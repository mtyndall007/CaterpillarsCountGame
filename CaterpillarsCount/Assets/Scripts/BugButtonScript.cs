using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BugButtonScript : MonoBehaviour
{

    public Button bugButton;

    void Start()
    {
        bugButton.onClick.AddListener(GameManager.instance.bugIdentified);
    }
}
