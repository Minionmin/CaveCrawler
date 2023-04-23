using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System.Linq;
using Unity.VisualScripting;

public class PauseUI : VisualElement
{
    VisualElement mainContainer;
    public bool isPaused = false;

    public new class UxmlFactory : UxmlFactory<PauseUI> { }

    private const string styleSource = "USS/Sheet_PauseUI";
    private const string usspauseui = "pauseui";
    private const string ussmaincontainer = "maincontainer";
    private const string usspausecontainer = "pausecontainer";
    private const string ussresumecontainer = "resumecontainer";
    private const string ussexitcontainer = "exitcontainer";
    private const string ussresumelabel = "resumelabel";
    private const string ussexitlabel = "exitlabel";

    public PauseUI()
    {

        styleSheets.Add(Resources.Load<StyleSheet>(styleSource));
        AddToClassList(usspauseui);

        mainContainer = new VisualElement();
        mainContainer.name = "MainContainer";
        mainContainer.AddToClassList(ussmaincontainer);
        Add(mainContainer);

        VisualElement pauseContainer = new VisualElement();
        pauseContainer.name = "PauseContainer";
        pauseContainer.AddToClassList(usspausecontainer);
        mainContainer.Add(pauseContainer);

        VisualElement resumeContainer = new VisualElement();
        resumeContainer.name = "ResumeContainer";
        resumeContainer.AddToClassList(ussresumecontainer);
        pauseContainer.Add(resumeContainer);
        resumeContainer.RegisterCallback<MouseDownEvent>(OnResumeClicked);

        Label resumeLabel = new Label();
        resumeLabel.name = "ResumeLabel";
        resumeLabel.text = "Resume Game";
        resumeLabel.AddToClassList(ussresumelabel);
        resumeContainer.Add(resumeLabel);

        VisualElement exitContainer = new VisualElement();
        exitContainer.name = "ExitContainer";
        exitContainer.AddToClassList(ussexitcontainer);
        pauseContainer.Add(exitContainer);
        exitContainer.RegisterCallback<MouseDownEvent>(OnExitClicked);

        Label exitLabel = new Label();
        exitLabel.name = "ExitLabel";
        exitLabel.text = "Exit Game";
        exitLabel.AddToClassList(ussexitlabel);
        exitContainer.Add(exitLabel);

        pickingMode = PickingMode.Ignore;
        style.visibility = Visibility.Hidden;
    }

    public void ActivatePauseUI()
    {
        Time.timeScale = 0f;
        pickingMode = PickingMode.Position;
        style.visibility = Visibility.Visible;
        isPaused = true;
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        pickingMode = PickingMode.Ignore;
        style.visibility = Visibility.Hidden;
        isPaused = false;
    }

    void OnResumeClicked(MouseDownEvent evt)
    {
        Resume();
        /*
        this.Query(className: "pauseui")
            .Descendents<VisualElement>()
            .ForEach(elem => { elem.pickingMode = PickingMode.Ignore; elem.style.visibility = Visibility.Hidden;});*/
    }

    private void OnExitClicked(MouseDownEvent evt)
    {
        Application.Quit();
    }
}
