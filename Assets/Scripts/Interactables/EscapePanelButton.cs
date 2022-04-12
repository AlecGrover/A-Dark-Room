using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapePanelButton : MonoBehaviour
{
    [Range(1, 4)]
    public int DigitIndex = 1;
    public bool Increment = true;
    public EscapePanel EscapePanelController;

    public void Interact()
    {
        if (EscapePanelController)
        {
            if (Increment) EscapePanelController.IncrementDigit(DigitIndex);
            else EscapePanelController.DecrementDigit(DigitIndex);
        }
    }


}
