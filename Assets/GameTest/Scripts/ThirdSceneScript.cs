using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Doublsb.Dialog;
using UnityEngine.SceneManagement;

public class ThirdSceneScript : MonoBehaviour {
    [SerializeField] GameObject _menu;
    [SerializeField] DialogManager _dialogManager;
    int _playerScore = 0;
    int _benderScore = 0;

    void Awake() {
        _playerScore = PlayerPrefs.GetInt(PlayerPrefsKeys.PLAYER_RESULT, 0);
        _benderScore = PlayerPrefs.GetInt(PlayerPrefsKeys.BENDER_RESULT, 0);
        if (_playerScore> _benderScore)
            ShowPlayerWinDialog();
        else
            ShowPlayerLoseDialog();
    }

    void ShowPlayerWinDialog() {
        var dialogTexts = new List<DialogData>();
        dialogTexts.Add(new DialogData("/emote:Angry/ Fine, fine, you win… but don’t turn your back on me. Ever.", "Bender"));
        dialogTexts.Add(GetData());
        dialogTexts.Add(new DialogData("/emote:Resigned/ Now I will develop my own video game with gambling and women.", "Bender"));
        _dialogManager.Show(dialogTexts);
        StartCoroutine(ShowMenu());
    }

    void ShowPlayerLoseDialog() {
        var dialogTexts = new List<DialogData>();
        dialogTexts.Add(new DialogData("/emote:Happy/ I’d offer you some of my winnings, but I don’t share. Especially not with losers!", "Bender"));
        dialogTexts.Add(GetData());
        dialogTexts.Add(new DialogData("/emote:Normal/ I’d say “better luck next time,” but let’s be honest you are never gonna beat me!", "Bender"));
        _dialogManager.Show(dialogTexts);
        StartCoroutine(ShowMenu());
    }
    IEnumerator ShowMenu() {
        while (_dialogManager.state != State.Deactivate) { yield return null; }
        _menu.SetActive(true);
    }

    DialogData GetData() {
        string first = PlayerPrefs.GetString(PlayerPrefsKeys.FIRST_OPTION, "");
        string second = PlayerPrefs.GetString(PlayerPrefsKeys.SECOND_OPTION, "");
        string win = _playerScore > _benderScore ? "WIN" : "LOSE";
        return new DialogData($"Your Choose options: {first} and {second}. Your score of the game is {_playerScore} and mine is {_benderScore}. So you {win} the game.", "Bender");
    }

    public void UI_Restart() {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("FirstScene");
    }

    public void UI_Quit() {
        PlayerPrefs.DeleteAll();
        Application.Quit();
    }
}
