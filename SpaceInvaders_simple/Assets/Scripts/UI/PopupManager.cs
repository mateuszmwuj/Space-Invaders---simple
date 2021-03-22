using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupManager : MonoBehaviour
{
    [SerializeField]
    private EndPopup endPopupPrefab;
    private EndPopup endPopupObject;

    [SerializeField]
    private Canvas canvas;

    public void OpenEndPopup(string titleText, Color titleTextColor, int score)
    {
        endPopupObject = Instantiate(endPopupPrefab, canvas.transform);

        endPopupObject.Init(titleText, titleTextColor, score);
    }
}
