using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueOption
{
    public string optionText; // Текст варианта ответа
    public string nextDialogueID; // Идентификатор следующего диалога (строка или GUID)
}

[System.Serializable]
public class Dialogue
{
    public string characterName; // Имя персонажа
    [TextArea(3, 10)]
    public string[] sentences; // Массив предложений для диалога
    public List<DialogueOption> options; // Варианты ответов
}

