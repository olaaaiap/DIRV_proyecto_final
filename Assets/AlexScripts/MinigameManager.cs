using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MinigameManager : MonoBehaviour
{
    private bool blocked;
    private bool chosenDiff;

    private List<Preguntas> chosenQuestions;

    [SerializeField] private List<Preguntas> questions;
    [SerializeField] private List<Preguntas> questionsHard;
    private int current;

    [SerializeField] private TextMeshPro gameText;

    private void Awake()
    {
        ChooseDifficulty();
        //UpdateMinigame();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            ChooseA();
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            ChooseB();
        }
    }

    private void ChooseDifficulty()
    {

        gameText.text = "ELIGE DIFICULTAD \n \n A. FACIL \n B. DIFICIL \n";
    }

    private void UpdateMinigame()
    {
        gameText.text = chosenQuestions[current].question.ToString() + "\n \n A." + chosenQuestions[current].answer1.ToString() + "\n B." + chosenQuestions[current].answer2.ToString() + "\n";
        blocked = false;
    }

    public void ChooseA()
    {
        if(blocked) return;

        if (!chosenDiff)
        {
            chosenDiff = true;
            chosenQuestions = questions;
            UpdateMinigame();
            return;
        }

        if(chosenQuestions[current].correctAnswer == 0) StartCoroutine(CorrectAnswer());
        else StartCoroutine(FailAnswer());
    }

    public void ChooseB()
    {
        if (blocked) return;

        if (!chosenDiff)
        {
            chosenDiff = true;
            chosenQuestions = questionsHard;
            UpdateMinigame();
            return;
        }

        if (chosenQuestions[current].correctAnswer == 0) StartCoroutine(FailAnswer());
        else StartCoroutine(CorrectAnswer());

    }

    private IEnumerator FailAnswer()
    {
        blocked = true;
        gameText.text = "HAS FALLADO";

        yield return new WaitForSeconds(3);

        current = 0;
        chosenDiff = false;
        ChooseDifficulty();
        blocked = false;
    }

    private IEnumerator CorrectAnswer()
    {
        blocked = true;
        gameText.text = "RESPUESTA CORRECTA";

        if(current == chosenQuestions.Count-1)
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
