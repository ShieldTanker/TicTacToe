using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class QuitPanelController : PanelController
{
    public void OnClickDenyButton()
    {
        Hide();
    }

    public void OnClickAcceptButton()
    {
        Debug.Log("게임 종료");
        GameManager.Instance.QuitGame();
    }
}
