using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MainUIController : MonoBehaviour
{
    VisualElement root;
    public PauseUI pauseUI;

    private void OnEnable()
    {
        root = GetComponent<UIDocument>().rootVisualElement;

        pauseUI = new PauseUI();
        root.Add(pauseUI);
    }

    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape)) 
        {
            if(!pauseUI.isPaused)
            {
                pauseUI.ActivatePauseUI();
            }
            else
            {
                pauseUI.Resume();
            }
        }
    }

}
