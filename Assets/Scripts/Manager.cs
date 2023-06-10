using RTLTMPro;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI Q1Input;
    [SerializeField] UnitySocketClient usc;
    public int Q1Ans;
    public int Q2Ans;
    public int Q3Ans;
    public int Q4Ans;
    public int Q5Ans;
    public int Q6Ans;
    public int Q7Ans;
    public int Q8Ans;
    public int Q9Ans;
    public int Q10Ans;
    public QuestionTurner qt;
    public void EndQuestions()
    {
        Debug.Log("Questions Ended");
        Request request = new()
        {
            Q1 = Q1Ans,
            Q2 = Q2Ans,
            Q3 = Q3Ans,
            Q4 = Q4Ans,
            Q5 = Q5Ans,
            Q6 = Q6Ans,
            Q7 = Q7Ans,
            Q8 = Q8Ans,
            Q9 = Q9Ans,
            Q10 = Q10Ans
        };
        usc.SendMessageToServer(request);
    }
    string RemoveLast(string name)
    {
        return name.Substring(0, name.Length - 1);
    }
    public void StartQuestioning()
    {
        qt.GoNextQuestion();
    }
    public void AnswerQ1()
    {
        try { Q1Ans = int.Parse(RemoveLast(Q1Input.text)); }
        catch { Debug.Log("WRONG INPUT "+ Q1Input.text.Length); }
        qt.GoNextQuestion();
    }
    public void AnswerQ2(int i)
    {
        Q2Ans = i;
        qt.GoNextQuestion();
    }
    public void AnswerQ3(int i)
    {
        Q3Ans = i;
        qt.GoNextQuestion();
    }
    public void AnswerQ4(int i)
    {
        Q4Ans = i;
        qt.GoNextQuestion();
    }
    public void AnswerQ5(int i)
    {
        Q5Ans = i;
        qt.GoNextQuestion();
    }
    public void AnswerQ6(int i)
    {
        Q6Ans = i;
        qt.GoNextQuestion();
    }
    public void AnswerQ7(int i)
    {
        Q7Ans = i;
        qt.GoNextQuestion();
    }
    public void AnswerQ8(int i)
    {
        Q8Ans = i;
        qt.GoNextQuestion();
    }
    public void AnswerQ9(int i)
    {
        Q9Ans = i;
        qt.GoNextQuestion();
    }
    public void AnswerQ10(int i)
    {
        Q10Ans = i;
        qt.GoNextQuestion();
    }
    public void RestartScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
    public void ExitApplication()
    {
        Application.Quit();
    }
}
