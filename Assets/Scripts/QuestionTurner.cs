using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestionTurner : MonoBehaviour
{
    public QuestionManager PrevQuestion;
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
    public void GoPreviousQuestion()
    {
        if (CurrentQuestion == null)
        {
            Debug.Log("no prev question");
        }
        else
        {
            Debug.Log("Going prev question " + CurrentQuestion.name);
            if (CurrentQuestion == FirstQuestion)
            {
                Debug.Log("No Prev Question");
                return;
            }
            PrevQuestion = CurrentQuestion.PrevQ[0];
            if (CurrentQuestion.name == "Q8-1"|| CurrentQuestion.name == "Q8-2")
            {
                if (manager.Q2Ans == 2)
                {
                    PrevQuestion = CurrentQuestion.PrevQ[1];
                }
                else
                {
                    PrevQuestion = CurrentQuestion.PrevQ[0];
                }
            }
            if (CurrentQuestion.name == "Q9")
            {
                if (manager.Q2Ans == 1)
                {
                    PrevQuestion = CurrentQuestion.PrevQ[0];
                }
                else
                {
                    PrevQuestion = CurrentQuestion.PrevQ[1];
                }
            }
        }
        PrevQuestion.transform.position = new Vector3(50, 0, 0);
        PrevQuestion.gameObject.SetActive(true);
        if (CurrentQuestion != null)
        {
            CurrentQuestion.transform.DOMove(new Vector3(-50, 0, 0), 0.5f).OnComplete(() =>
            {
                CurrentQuestion.gameObject.SetActive(false);
            });
        }
        PrevQuestion.transform.DOMove(new Vector3(0, 0, 0), 1f).OnComplete(() =>
        {
            CurrentQuestion = PrevQuestion;
            CurrentQuestion.EnableButtons();
        });
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
