using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MinigameManager : MonoBehaviour
{
    private bool blocked;

    private List<Question> questions;
    private int current;

    [SerializeField] private TextMeshPro gameText;

    private void Awake()
    {
        questions = new List<Question>()
        {
            new Question("¿En qué año comienza la primera temporada de Stranger Things", "1983", "1985", 0),
            new Question("¿Cómo se llama el pueblo en el que transcurre la historia?", "Mystic Falls", "Hawkins", 1),
            new Question("¿Cuál es el nombre del centro comercial del pueblo?", "Starcourt Mall", "Starstruck Mall", 0),
            new Question("¿Cuál es la canción que toca Eddie Munson en el Upside Down?", "Thunderstruck", "Master of Puppets", 1),

        };

        UpdateMinigame();
    }

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.A))
    //    {
    //        ChooseA();
    //    }

    //    if (Input.GetKeyDown(KeyCode.B))
    //    {
    //        ChooseB();
    //    }
    //}

    private void UpdateMinigame()
    {
        gameText.text = questions[current].question.ToString() + "\n \n A." + questions[current].answer1.ToString() + "\n B." + questions[current].answer2.ToString() + "\n";
        blocked = false;
    }

    public void ChooseA()
    {
        if(blocked) return;

        if(questions[current].correctAnswer == 0) StartCoroutine(CorrectAnswer());
        else StartCoroutine(FailAnswer());
    }

    public void ChooseB()
    {
        if (blocked) return;

        if (questions[current].correctAnswer == 0) StartCoroutine(FailAnswer());
        else StartCoroutine(CorrectAnswer());

    }

    private IEnumerator FailAnswer()
    {
        blocked = true;
        gameText.text = "HAS FALLADO";

        yield return new WaitForSeconds(3);

        current = 0;
        UpdateMinigame();
    }

    private IEnumerator CorrectAnswer()
    {
        blocked = true;
        gameText.text = "RESPUESTA CORRECTA";

        if(current == questions.Count-1)
        {
            SceneLoadingManagement.instance.LoadNextScene();
        }
        else
        {
            yield return new WaitForSeconds(3);
            current++;
            UpdateMinigame();
        }
           
    }
}

[System.Serializable]
public class Question
{
    public int correctAnswer;

    public string question;

    public string answer1;
    public string answer2;

    public Question(string question, string answer1, string answer2, int correctAnswer)
    {
        this.correctAnswer = correctAnswer;
        this.question = question;
        this.answer1 = answer1;
        this.answer2 = answer2;
    }
}
