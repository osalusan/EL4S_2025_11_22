using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class QuestionData
{
    public string questionText;        // 問題文
    public string[] choices = new string[4]; // 4択
    public Sprite image;
    public int correctIndex;           // 正解(1〜4)
}