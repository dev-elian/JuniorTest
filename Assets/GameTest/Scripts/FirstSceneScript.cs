using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Doublsb.Dialog;
public class FirstSceneScript : MonoBehaviour
{
    [SerializeField] DialogManager _dialogManager;

    void Awake()
    {
        ShowFirstDialogsGroup();
    }

    void ShowFirstDialogsGroup() {
        var dialogTexts = new List<DialogData>();
        dialogTexts.Add(new DialogData("Wow! Another insignificant human who wants to talk to me.", "Bender"));
        var firstSelection = new DialogData("What brings you here?");
        firstSelection.SelectList.Add(PlayerEmotions.FRIENDLY, "A) I just want to meet you, Bender. You are a legend.");
        firstSelection.SelectList.Add(PlayerEmotions.HOSTILE, "B) I don't know if I want to talk to a lot of scrap.");
        firstSelection.SelectList.Add(PlayerEmotions.SARCASTIC, "C) Are you the famous Bender? I thought you would be brighter ... literally.");
        firstSelection.Callback = () => OnFirstSelectionResult();
        dialogTexts.Add(firstSelection);
        _dialogManager.Show(dialogTexts);
    }
    void OnFirstSelectionResult() {
        switch (_dialogManager.Result) {

            case PlayerEmotions.FRIENDLY:
                PlayerPrefs.SetString(PlayerPrefsKeys.FIRST_OPTION, "A");
                ShowDialog("I know, I know. I am the most amazing robot that has existed! Keep adulthood, I like it.", "Bender");
                break;
            case PlayerEmotions.HOSTILE:
                PlayerPrefs.SetString(PlayerPrefsKeys.FIRST_OPTION, "B");
                ShowDialog("Oh, what an offensive! I don't care, because we have no feelings ... except when they don't invite me to drink.", "Bender");
                break;
            case PlayerEmotions.SARCASTIC:
                PlayerPrefs.SetString(PlayerPrefsKeys.FIRST_OPTION, "C");
                ShowDialog("/emote:Angry/ Are you insinuating that I need a polishing? Listen, meat with eyes, Bender shines when he want!", "Bender");
                break;
            default:
                break;
        }
        StartCoroutine(ShowSencondDialogsGroup());
    }

    IEnumerator ShowSencondDialogsGroup() {
        while (_dialogManager.state != State.Deactivate) { yield return null; }
        var dialogTexts = new List<DialogData>();
        dialogTexts.Add(new DialogData("Hey, meatbag! I just had a brilliant idea… and by brilliant, I mean completely illegal.", "Bender"));
        dialogTexts.Add(new DialogData("How about a little competition? You and I steal as much junk as we can from the old crackpot, and whoever gets the most loot wins. The prize? Everything we stole, of course!", "Bender"));
        var secondSelection = new DialogData("What you choose?");
        secondSelection.SelectList.Add(PlayerEmotions.POSITIVE, "A) That sounds fun! I’m in.");
        secondSelection.SelectList.Add(PlayerEmotions.FEARFUL, "B) What if the Professor notices?");
        secondSelection.SelectList.Add(PlayerEmotions.NEGATIVE, "C) That’s a crime, Bender!");
        secondSelection.Callback = () => OnSecondSelectionResult();
        dialogTexts.Add(secondSelection);
        _dialogManager.Show(dialogTexts);
    }
    void OnSecondSelectionResult() {
        switch (_dialogManager.Result) {

            case PlayerEmotions.POSITIVE:
                PlayerPrefs.SetString(PlayerPrefsKeys.SECOND_OPTION, "A");
                ShowDialog("/emote:Happy/ Ha! I like your attitude, rookie thief. But just so you know, I’m a pro.", "Bender");
                break;
            case PlayerEmotions.FEARFUL:
                PlayerPrefs.SetString(PlayerPrefsKeys.SECOND_OPTION, "B");
                ShowDialog("/emote:Resigned/ Pfft! That fossil barely remembers what he had for breakfast. Besides, I’ll just say it was you.", "Bender");
                break;
            case PlayerEmotions.NEGATIVE:
                PlayerPrefs.SetString(PlayerPrefsKeys.SECOND_OPTION, "C");
                ShowDialog("/emote:Waiting/ Oh wow, a goody-two-shoes. What’s next? Recycling and using eco-friendly batteries? Lame!", "Bender");
                break;
            default:
                break;
        }
        StartCoroutine(ShowThirdDialogsGroup());
    }

    IEnumerator ShowThirdDialogsGroup() {
        while (_dialogManager.state != State.Deactivate) { yield return null; }
        var dialogTexts = new List<DialogData>();
        dialogTexts.Add(new DialogData("Anyway, it doesn't matter what you choose, as I am a character of a video game, you will have to play with me anyway.", "Bender"));
        dialogTexts.Add(new DialogData("Click the objects that appear on the screen as quickly as possible, if you think you are faster than me, human.", "Bender"));
        _dialogManager.Show(dialogTexts);
        StartCoroutine(GoToSecondScene());
    }

    IEnumerator GoToSecondScene() {
        while (_dialogManager.state != State.Deactivate) { yield return null; }
        GameManager.Instance.GoToNextScene();
    }

    void ShowDialog(string text, string character) {
        _dialogManager.Show(new DialogData(text, character));
    }
}
