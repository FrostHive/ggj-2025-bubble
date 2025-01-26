using System.Collections;
using TMPro;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    [SerializeField] private PlayerInputHandler inputHandler;
    [SerializeField] private TextMeshProUGUI textComponent;
    [SerializeField] private bool currentlySpeaking;
    [SerializeField] private string[] dialogueTexts;
    [SerializeField] private float textSpeed;

    private int index;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        textComponent.text = string.Empty;
        StartDialogue();
    }

    // Update is called once per frame
    private void Update()
    {
        if(inputHandler.attackTriggered && !currentlySpeaking)
        {
            if (textComponent.text == dialogueTexts[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = dialogueTexts[index];
            }
        }
    }

    private void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    private IEnumerator TypeLine()
    {
        currentlySpeaking = true;
        foreach (char c in dialogueTexts[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
        currentlySpeaking = false;
    }

    public void NextLine()
    {
        if(index < dialogueTexts.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
