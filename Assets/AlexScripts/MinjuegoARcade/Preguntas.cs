using UnityEngine;

[CreateAssetMenu(menuName = "SOs/Preguntas")]
public class Preguntas : ScriptableObject
{
    public int correctAnswer;

    public string question;

    public string answer1;
    public string answer2;

    public Preguntas(string question, string answer1, string answer2, int correctAnswer)
    {
        this.correctAnswer = correctAnswer;
        this.question = question;
        this.answer1 = answer1;
        this.answer2 = answer2;
    }
}
