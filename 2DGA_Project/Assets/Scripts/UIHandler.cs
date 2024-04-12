using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// This class shows how to change the functionality of UI Elements programatically
/// </summary>
/// 
public class UIHandler : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI loadLevelButton;

    // Start is called before the first frame update
    void Start()
    {
        //loadLevelButton.onClick.AddListener(LoadLevel);
    }

    private void LoadLevel() {
        ScenesManager.Instance.LoadScene(ScenesManager.Scene.SpineSkinChange);
    }
}
