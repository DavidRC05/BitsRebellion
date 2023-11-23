using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WelcomeController : MonoBehaviour
{

            [SerializeField]
        TextMeshProUGUI textbox;
    public void Play()
    {
        StateManager.Instance.setName(textbox.text);
        LevelManager.Instance.NextScene();

    }
}
