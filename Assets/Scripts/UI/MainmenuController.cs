using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class MainmenuController : MonoBehaviour
{
    LevelExit levelExit;
    VisualElement root;

    private void OnEnable()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        
        VisualElement playContainer = root.Q<VisualElement>("PlayContainer");
        VisualElement exitContainer = root.Q<VisualElement>("ExitContainer");

        playContainer.RegisterCallback<MouseDownEvent>(OnPlayClicked);
        exitContainer.RegisterCallback<MouseDownEvent>(OnExitClicked);
    }

    private void OnPlayClicked(MouseDownEvent evt)
    {
        int SceneIndex = SceneManager.GetActiveScene().buildIndex;
        int NextSceneIndex = SceneIndex + 1;
        SceneManager.LoadScene(NextSceneIndex);
    }

    private void OnExitClicked(MouseDownEvent evt)
    {
        Application.Quit();
    }


}
