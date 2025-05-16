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
        Debug.Log("���� ����");
        GameManager.Instance.QuitGame();
    }
}
