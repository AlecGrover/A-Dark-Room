using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueSystem : MonoBehaviour
{

    public TextMeshProUGUI DialougeText;
    public float DialogueWriteTime;
    private string _nextDialogue = "Your flashlight flickers in the dark...";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TriggerDialogue(string dialogueString = "Your flashlight flickers in the dark...")
    {
        StopAllCoroutines();
        _nextDialogue = dialogueString;
        StartCoroutine(WriteDialogue());
    }

    private IEnumerator WriteDialogue()
    {
        float timeElapsed = 0f;
        while (timeElapsed < DialogueWriteTime)
        {
            if (DialougeText)
                DialougeText.text = _nextDialogue.Substring(0,
                    Mathf.CeilToInt(Mathf.Clamp(timeElapsed / DialogueWriteTime, 0f, 1f) * _nextDialogue.Length));
            yield return new WaitForEndOfFrame();
            timeElapsed += Time.deltaTime;
        }
    }




}
