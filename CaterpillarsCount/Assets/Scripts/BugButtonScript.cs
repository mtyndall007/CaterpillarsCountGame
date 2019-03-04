using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[System.Serializable]
public class BugIDEvent : UnityEvent<string>
{
}

public class BugButtonScript : MonoBehaviour
{

    public Button bugButton;
    public BugClickedEvent bugClicked;

    void Start()
    {
        bugButton.onClick.AddListener(() => ButtonClicked(EventSystem.current.currentSelectedGameObject.name));
    
    }

    private void ButtonClicked(string bugType)
    {
        GameManager.instance.BugSelectionUI(bugType);
    }
}
