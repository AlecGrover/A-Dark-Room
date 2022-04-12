using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLevelRunner : MonoBehaviour
{

    public GameObject BlackScreen;
    public GameObject EndOfLoopScreen;
    public GameObject GameWinScreen;

    private MusicPlayer _levelMusicPlayer;
    private PlayerInteractSystem _playerInteractSystem;
    private DialogueSystem _dialogueSystem;
    private FadingPulse _fadingPulse;
    private BatteryDisplay _batteryDisplay;


    // Start is called before the first frame update
    void Start()
    {
        _levelMusicPlayer = FindObjectOfType<MusicPlayer>();
        _playerInteractSystem = FindObjectOfType<PlayerInteractSystem>();
        _dialogueSystem = FindObjectOfType<DialogueSystem>();
        _fadingPulse = FindObjectOfType<FadingPulse>();
        _batteryDisplay = FindObjectOfType<BatteryDisplay>();
        
        if (_playerInteractSystem) _playerInteractSystem.enabled = false;
        StartCoroutine(StartLevel());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator StartLevel()
    {
        while (true)
        {
            if (Input.GetKeyDown(KeyCode.Space)) break;
            yield return null;
        }

        if (_dialogueSystem) _dialogueSystem.TriggerDialogue("Your flashlight flickers to life...");
        if (BlackScreen) BlackScreen.SetActive(false);
        if (_levelMusicPlayer) _levelMusicPlayer.PlayMusic();
        if (_playerInteractSystem) _playerInteractSystem.enabled = true;
        // if (_fadingPulse) _fadingPulse.StartPulse();
        StartCoroutine(RunLevel());
    }

    private IEnumerator RunLevel()
    {
        float levelTime = 0;
        while (levelTime < 60)
        {
            levelTime += Time.deltaTime;
            if (_batteryDisplay) _batteryDisplay.UpdateBatteryText((60 - levelTime)/60f);
            yield return new WaitForEndOfFrame();
        }

        if (_playerInteractSystem) _playerInteractSystem.enabled = false;
        if (EndOfLoopScreen) EndOfLoopScreen.SetActive(true);
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(1);
    }

    public void InformOfGameWin()
    {
        StopAllCoroutines();
        if (_playerInteractSystem) _playerInteractSystem.enabled = false;
        if (GameWinScreen) GameWinScreen.SetActive(true);
        StartCoroutine(EndGame());
    }

    private IEnumerator EndGame()
    {
        while (true)
        {
            if (Input.GetKeyDown(KeyCode.Space)) break;
            yield return null;
        }
        UnityEngine.Cursor.visible = true;
        SceneManager.LoadScene(0);
    }


}
