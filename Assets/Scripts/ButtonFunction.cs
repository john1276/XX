using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonFunction : MonoBehaviour
{
    public GameObject tutorialPanel;
    public void openTutorial(){
        tutorialPanel.SetActive(true);
    }
}
