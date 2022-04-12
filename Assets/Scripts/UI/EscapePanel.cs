using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EscapePanel : MonoBehaviour
{

    public TextMeshProUGUI CodeText1;
    public TextMeshProUGUI CodeText2;
    public TextMeshProUGUI CodeText3;
    public TextMeshProUGUI CodeText4;

    private int _codeValue1 = 0;
    private int _codeValue2 = 0;
    private int _codeValue3 = 0;
    private int _codeValue4 = 0;

    [SerializeField]
    private string TargetCode = "3719";

    public Door ExitDoor;

    public OneShotPlayer SoundPlayer;
    public AudioClip UnlockAudio;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if ($"{_codeValue1}{_codeValue2}{_codeValue3}{_codeValue4}" == TargetCode)
        {
            if (ExitDoor) ExitDoor.Unlock();
            if (SoundPlayer && UnlockAudio) SoundPlayer.PlayOneShot(UnlockAudio);
            Destroy(this);
        }

        CodeText1.text = _codeValue1.ToString();
        CodeText2.text = _codeValue2.ToString();
        CodeText3.text = _codeValue3.ToString();
        CodeText4.text = _codeValue4.ToString();
    }

    public void IncrementDigit(int spaceNumber)
    {
        switch (spaceNumber)
        {
            case (1):
                _codeValue1++;
                _codeValue1 %= 10;
                break;
            case (2):
                _codeValue2++;
                _codeValue2 %= 10;
                break;
            case (3):
                _codeValue3++;
                _codeValue3 %= 10;
                break;
            case (4):
                _codeValue4++;
                _codeValue4 %= 10;
                break;
        }
    }

    public void DecrementDigit(int spaceNumber)
    {
        switch (spaceNumber)
        {
            case (1):
                _codeValue1 = (_codeValue1 + 9) % 10;
                break;
            case (2):
                _codeValue2 = (_codeValue2 + 9) % 10;
                break;
            case (3):
                _codeValue3 = (_codeValue3 + 9) % 10;
                break;
            case (4):
                _codeValue4 = (_codeValue4 + 9) % 10;
                break;
        }
    }




}
