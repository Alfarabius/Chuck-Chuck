using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI nameText; // UI Text для имени персонажа
    public TextMeshProUGUI dialogueText; // UI Text для текста диалога
    public GameObject dialoguePanel; // Панель для показа/скрытия UI диалога
    public GameObject optionsPanel; // Панель для показа вариантов ответов
    public Button optionButtonPrefab; // Префаб кнопки для варианта ответа

    private Queue<string> sentences; // Очередь для управления предложениями
    private bool isDialogueActive;

    void Start()
    {
        sentences = new Queue<string>();
        dialoguePanel.SetActive(false); // Скрыть панель в начале
        optionsPanel.SetActive(false); // Скрыть панель вариантов в начале
    }

    public void StartDialogue(Dialogue dialogue)
    {
        isDialogueActive = true;
        dialoguePanel.SetActive(true); // Показать панель диалога

        nameText.text = dialogue.characterName; // Установить имя персонажа
        sentences.Clear(); // Очистить предыдущие предложения

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence); // Добавить предложения в очередь
        }

        DisplayNextSentence(); // Показать первое предложение
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            ShowOptions(); // Показать варианты, если предложения закончились
            return;
        }

        string sentence = sentences.Dequeue(); // Получить следующее предложение
        dialogueText.text = sentence; // Отобразить его
    }

    private void ShowOptions()
    {
        optionsPanel.SetActive(true); // Показать панель вариантов

        foreach (Transform child in optionsPanel.transform)
        {
            Destroy(child.gameObject); // Удалить старые кнопки вариантов, если есть
        }

        Dialogue currentDialogue = GetCurrentDialogue(); // Получаем текущий диалог (это нужно реализовать)

        foreach (DialogueOption option in currentDialogue.options)
        {
            Button button = Instantiate(optionButtonPrefab, optionsPanel.transform);
            button.GetComponentInChildren<Text>().text = option.optionText; // Установить текст кнопки
            button.onClick.AddListener(() => SelectOption(option)); // Добавить обработчик нажатия на кнопку
        }
    }

    private void SelectOption(DialogueOption option)
    {
        optionsPanel.SetActive(false); // Скрыть панель вариантов

		var _nextId = option.nextDialogueID;

		if (_nextId == "EndDialogue") // Завершение диалога при отправки EndDialogue Id
		{
			EndDialogue();
			return;
		}

        StartDialogue(GetDialogueByID(_nextId)); // Начать следующий диалог в зависимости от выбранного варианта
    }

    private void EndDialogue()
    {
        isDialogueActive = false;
        dialoguePanel.SetActive(false); // Скрыть панель по окончании диалога
    }

    void Update()
    {
        if (isDialogueActive && Input.GetKeyDown(KeyCode.Space)) // Нажмите пробел, чтобы продолжить
        {
            DisplayNextSentence();
        }
    }

	public Dialogue GetDialogueByID(string id)
	{
		// Здесь вы должны реализовать логику поиска диалога по ID.
		// Например, хранить все ваши DialogueData в списке и искать по идентификатору.
		return null;
	}

    private Dialogue GetCurrentDialogue()
    {
        // Здесь вы должны вернуть текущий диалог. Это может быть реализовано через отдельную переменную.
        return null;
    }
}