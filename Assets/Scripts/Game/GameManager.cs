using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject confirmPanel;
    [SerializeField] private GameObject signinPanel;
    [SerializeField] private GameObject signupPanel;
    [SerializeField] private GameObject quitPanel;

    string sfxToggle = "SFXToggle";
    string vibrationToggle = "VibrationToggle";

    private GameUIController _gameUIController;
    private Canvas _canvas;
    
    private Constants.GameType _gameType;
    
    private GameLogic _gameLogic;

    [Space]
    public bool IsSFX;
    public bool IsVibration;

    private void Start()
    {
        // 로그인
        // OpenSigninPanel();

        int vibration = PlayerPrefs.GetInt(vibrationToggle, 1);
        int sfx = PlayerPrefs.GetInt(sfxToggle, 1);

        bool sfxBool;
        bool vibrationBool;

        if (sfx > 0) sfxBool = true;
        else sfxBool = false;

        if (vibration > 0) vibrationBool = true;
        else vibrationBool = false;

        GameManager.Instance.IsSFX = sfxBool;
        GameManager.Instance.IsVibration = vibrationBool;
    }

    public void ChangeToGameScene(Constants.GameType gameType)
    {
        _gameType = gameType;
        SceneManager.LoadScene("Game");
    }

    public void ChangeToMainScene()
    {
        _gameLogic?.Dispose();
        _gameLogic = null;
        SceneManager.LoadScene("Main");
    }


    public void OpenSettingsPanel()
    {
        if (_canvas != null)
        {
            var settingsPanelObject = Instantiate(settingsPanel, _canvas.transform);
            settingsPanelObject.GetComponent<PanelController>().Show();
        }
    }

    public void OpenQuitPanel()
    {
        if (_canvas != null)
        {
            var quitPanelObject = Instantiate(quitPanel, _canvas.transform);
            quitPanelObject.GetComponent<PanelController>().Show();
        }
    }

    public void OpenConfirmPanel(string message, ConfirmPanelController.OnConfirmButtonClick onConfirmButtonClick)
    {
        if (_canvas != null)
        {
            var confirmPanelObject = Instantiate(confirmPanel, _canvas.transform);
            confirmPanelObject.GetComponent<ConfirmPanelController>()
                .Show(message, onConfirmButtonClick);
        }
    }

    public void OpenSigninPanel()
    {
        if (_canvas != null)
        {
            var signinPanelObject = Instantiate(signinPanel, _canvas.transform);
        }
    }

    public void OpenSignupPanel()
    {
        if (_canvas != null)
        {
            var signupPanelObject = Instantiate(signupPanel, _canvas.transform);
        }
    }

    public void OpenGameOverPanel()
    {
        _gameUIController.SetGameUIMode(GameUIController.GameUIMode.GameOver);
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }

    protected override void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Game")
        {
            // 씬에 배치된 오브젝트 찾기 (BlockContorller, GameUIController)
            var blockController = GameObject.FindObjectOfType<BlockController>();
            _gameUIController = GameObject.FindObjectOfType<GameUIController>();
            
            // Block Controller 초기화
            blockController.InitBlocks();
            
            // Game UI 초기화
            _gameUIController.SetGameUIMode(GameUIController.GameUIMode.Init);
            
            // Game Logic 객체 생성
            if (_gameLogic != null) _gameLogic.Dispose();
            _gameLogic = new GameLogic(blockController, _gameType);
        }
        
        _canvas = GameObject.FindObjectOfType<Canvas>();
    }

    private void OnApplicationQuit()
    {
        _gameLogic?.Dispose();
        _gameLogic = null;
    }
}