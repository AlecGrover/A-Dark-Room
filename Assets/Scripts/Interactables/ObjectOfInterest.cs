using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectOfInterest : MonoBehaviour
{

    public string DialogueFlavorText = "A curious object";
    private DialogueSystem _dialogueSystem;

    // Start is called before the first frame update
    void Start()
    {
        _dialogueSystem = FindObjectOfType<DialogueSystem>();
    }

    public void FlavorDialogue()
    {
        if (_dialogueSystem) _dialogueSystem.TriggerDialogue(DialogueFlavorText);
    }

}
