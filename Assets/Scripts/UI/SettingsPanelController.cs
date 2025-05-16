using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsPanelController : PanelController
{
    string sfxToggle = "SFXToggle";
    string vibrationToggle = "VibrationToggle";

    [SerializeField] SwitchController sfxSwitch;
    [SerializeField] SwitchController vibrationSwitch;

    private void Start()
    {
        int vibration = PlayerPrefs.GetInt(vibrationToggle, 1);
        int sfx = PlayerPrefs.GetInt(sfxToggle, 1);
        
        bool sfxBool;
        bool vibrationBool;

        if (sfx > 0)    sfxBool = true;
        else            sfxBool = false;

        if (vibration > 0)  vibrationBool = true;
        else                vibrationBool = false;

        sfxSwitch.IsOn = sfxBool;
        sfxSwitch.SetOn(sfxBool);
        
        vibrationSwitch.IsOn = vibrationBool;
        vibrationSwitch.SetOn(vibrationBool);

        GameManager.Instance.IsSFX = sfxBool;
        GameManager.Instance.IsVibration = vibrationBool;
    }

    /// <summary>
    /// SFX On/Off시 호출되는 함수
    /// </summary>
    /// <param name="value">On/Off 값</param>
    public void OnSFXToggleValueChanged()
    {
        int value = PlayerPrefs.GetInt(sfxToggle, 1) * -1;
        bool boolValue;

        if (value > 0)  boolValue = true;
        else            boolValue = false;
        
        sfxSwitch.IsOn = boolValue;
        sfxSwitch.SetOn(boolValue);

        GameManager.Instance.IsSFX = boolValue;
        
        PlayerPrefs.SetInt(sfxToggle, value);
    }

    public void OnVibrationToggleValueChanged()
    {
        int value = PlayerPrefs.GetInt(vibrationToggle, 1) * -1;
        bool boolValue;

        if (value > 0)  boolValue = true;
        else            boolValue = false;

        vibrationSwitch.IsOn = boolValue;
        vibrationSwitch.SetOn(boolValue);

        GameManager.Instance.IsVibration = boolValue;

        PlayerPrefs.SetInt(vibrationToggle, value);
    }

    /// <summary>
    /// BGM On/Off시 호출되는 함수
    /// </summary>
    /// <param name="value">On/Off 값</param>
    public void OnBGMToggleValueChanged(bool value)
    {

    }

    /// <summary>
    /// X 버튼 클릭시 호출되는 함수
    /// </summary>
    public void OnClickCloseButton()
    {
        Hide();
    }
}
