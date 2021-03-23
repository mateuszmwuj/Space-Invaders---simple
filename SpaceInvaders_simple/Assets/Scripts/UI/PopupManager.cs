using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupManager : MonoBehaviour
{
    [SerializeField]
    private EndPopup _endPopupPrefab;
    private EndPopup _endPopupObject;

    [SerializeField]
    private Canvas _canvas;

    public void OpenEndPopup(string titleText, Color titleTextColor, int score)
    {
        _endPopupObject = Instantiate(_endPopupPrefab, _canvas.transform);

        _endPopupObject.Init(titleText, titleTextColor, score);
    }
}
