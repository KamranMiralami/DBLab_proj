using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestionTurner : MonoBehaviour
{
    public QuestionManager CurrentQuestion;
    public QuestionManager NextQuestion;
    public QuestionManager FirstQuestion;
    public QuestionManager LastQuestion;
    public GameObject WelcomeSign;
    public GameObject ResultSign;
    public Manager manager;
    private void Start()
    {
    }
    public void GoNextQuestion()
    {
        if (CurrentQuestion == null)
        {
            NextQuestion = FirstQuestion;
            WelcomeSign.SetActive(false);
            Debug.Log("Going next question " + NextQuestion.name);
        }
        else
        {
            Debug.Log("Going next question " + CurrentQuestion.name);
            if (CurrentQuestion == LastQuestion)
            {
                manager.EndQuestions();
                CurrentQuestion.gameObject.SetActive(false);
                ResultSign.SetActive(true);

                return;
            }
            NextQuestion = CurrentQuestion.NextQ[0];
            if (CurrentQuestion.name == "Q6")
            {
                if (manager.Q2Ans == 2)
                {
                    NextQuestion = CurrentQuestion.NextQ[1];
                }
                else
                {
                    NextQuestion = CurrentQuestion.NextQ[0];
                }
            }
            if (CurrentQuestion.name == "Q7-1" || CurrentQuestion.name == "Q7-2")
            {
                if (manager.Q2Ans == 1)
                {
                    NextQuestion = CurrentQuestion.NextQ[0];
                }
                else
                {
                    NextQuestion = CurrentQuestion.NextQ[1];
                }
            }
        }
        NextQuestion.transform.position = new Vector3(-50, 0, 0);
        NextQuestion.gameObject.SetActive(true);
        if (CurrentQuestion != null)
        {
            CurrentQuestion.transform.DOMove(new Vector3(50, 0, 0), 0.5f).OnComplete(() =>
            {
                CurrentQuestion.gameObject.SetActive(false);
            });
        }
        NextQuestion.transform.DOMove(new Vector3(0, 0, 0), 1f).OnComplete(() =>
        {
            CurrentQuestion = NextQuestion;
            CurrentQuestion.EnableButtons();
        });
    }
}
