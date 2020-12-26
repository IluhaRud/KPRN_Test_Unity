using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
/// <summary>
/// Класс, который содержит информацию по данному тесту
/// </summary>
public class TestData : MonoBehaviour
{
    /// <summary>
    /// Баллы, которые начисляются за правильные ответы
    /// </summary>
    public int points;
    /// <summary>
    /// Порядковый номер вопроса
    /// </summary>
    public int numberOfQuestion;
    /// <summary>
    /// Сколько всего вопросов в тесте
    /// </summary>
    public int maxQuestions;
    /// <summary>
    /// Представляет динамический список канвасов с вопросами
    /// </summary>
    public List<Canvas> questions;
    /// <summary>
    /// Представляет сущность авторизованного клиента
    /// </summary>
    public Person person;

    public Canvas results;

    private void Start()
    {
        int c = 0;

        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).gameObject.tag == "CanvasQuestion")
            {
                questions.Add(transform.GetChild(i).gameObject.GetComponent<Canvas>());
                transform.GetChild(i).gameObject.name = $"TestCanvas{c}";

                c++;
            }
        }

        if (questions.Count > 0)
        {
            LoadQuestion();
        }

        maxQuestions = questions.Count;
    }

    public void LoadQuestion()
    {
        if (numberOfQuestion == 0)
        {
            questions[numberOfQuestion].gameObject.SetActive(true);
            numberOfQuestion++;
            return;
        }

        if (numberOfQuestion < questions.Count)
        {
            questions[numberOfQuestion - 1].gameObject.SetActive(false);
            questions[numberOfQuestion].gameObject.SetActive(true);
            numberOfQuestion++;
            return;
        }

        if (numberOfQuestion == questions.Count)
        {
            questions[numberOfQuestion - 1].gameObject.SetActive(false);
            transform.Find("ResultsCanvas").gameObject.SetActive(true);
            transform.Find("ResultsCanvas").gameObject.transform.Find("Text").GetComponent<Text>().text = $"Ваш результат: {points} / {questions.Count}";
        }
    }
    public void ClickReturnToMainMenu()
    {
        points = 0;
        numberOfQuestion = 0;
        maxQuestions = 0;
        questions = null;

        SceneManager.LoadScene("MenuScene");
    }
}
