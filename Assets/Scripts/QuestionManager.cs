using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionManager : MonoBehaviour
{
    public List<QuestionManager> NextQ;
    public List<Button> Buttons;
    private void Start()
    {
        foreach (var button in Buttons) {
            button.interactable = false;
        }
    }
    public void EnableButtons()
    {
        foreach (var button in Buttons)
        {
            button.interactable = true;
        }
    }
}
