using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Doublsb.Dialog;

public class SecondSceneScript : MonoBehaviour
{
    public static SecondSceneScript Instance { get; private set; }
    [SerializeField] DialogManager _dialogManager;

    void Awake() {
        if (Instance == null)
            Instance = this;
        ShowFirstDialogsGroup();
    }
    void ShowFirstDialogsGroup() {
        var dialogTexts = new List<DialogData>();
        dialogTexts.Add(new DialogData("Oh wow, what an honor! I get to waste my precious time playing with you!", "Bender"));
        dialogTexts.Add(new DialogData("Let’s get this over with initiating game sequence… or whatever.", "Bender"));
        _dialogManager.Show(dialogTexts);
        StartCoroutine(WaitUntilStartGame());
    }

    IEnumerator WaitUntilStartGame() {
        while (_dialogManager.state != State.Deactivate) { yield return null; }
        Debug.Log(MinigameController.Instance);
        MinigameController.Instance.StartGame();
    }

    public void ShowDialog(string text, string character) {
        _dialogManager.Show(new DialogData(text, character));
        _dialogManager.Click_Window();
    }
}
