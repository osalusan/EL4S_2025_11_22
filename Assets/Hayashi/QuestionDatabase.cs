using UnityEngine;

[CreateAssetMenu(fileName = "QuestionDatabase", menuName = "ScriptableObject/Database")]
public class QuestionDatabase : ScriptableObject
{
    [SerializeField] public QuestionData[] questions;
}
