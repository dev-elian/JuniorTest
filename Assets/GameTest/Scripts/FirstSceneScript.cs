using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Doublsb.Dialog;
using System;
public static class PlayerEmotions {
    public const string FRIENDLY = "Friendly";
    public const string HOSTILE = "Hostile";
    public const string SARCASTIC = "Sarcastic";
    public const string POSITIVE = "Positive";
    public const string FEARFUL = "Fearful";
    public const string NEGATIVE = "Negative";
}
public class FirstSceneScript : MonoBehaviour
{
    public DialogManager DialogManager;

    private void Awake()
    {
        var dialogTexts = new List<DialogData>();
        dialogTexts.Add(new DialogData("Wow, go! Another insignificant human who wants to talk to me.", "Bender"));
        var firstSelection = new DialogData("What brings you here?");
        firstSelection.SelectList.Add(PlayerEmotions.FRIENDLY, "A) I just want to meet you, Bender. You are a legend.");
        firstSelection.SelectList.Add(PlayerEmotions.HOSTILE, "B) I don't know if I want to talk to a lot of scrap.");
        firstSelection.SelectList.Add(PlayerEmotions.SARCASTIC, "C) Are you the famous Bender? I thought you would be brighter ... literally.");
        firstSelection.Callback = () => OnFirstSelectionResult();
        dialogTexts.Add(firstSelection);
        DialogManager.Show(dialogTexts);
    }

    void OnFirstSelectionResult() {
        switch (DialogManager.Result) {

            case PlayerEmotions.FRIENDLY:
                ShowDialog("I know, I know. I am the most amazing robot that has existed! Keep adulthood, I like it.", "Bender");
                break;
            case PlayerEmotions.HOSTILE:
                ShowDialog("Oh, what an offensive! I don't care, because we have no feelings ... except when they don't invite me to drink.", "Bender");
                break;
            case PlayerEmotions.SARCASTIC:
                ShowDialog("/emote:Angry/ Are you insinuating that I need a polishing? Listen, meat with eyes, Bender shines when you want!", "Bender");
                break;
            default:
                break;
        }
        StartCoroutine(ShowSencondDialogsGroup());
    }

    IEnumerator ShowSencondDialogsGroup() {
        while (DialogManager.state != State.Deactivate) { yield return null; }
        var dialogTexts = new List<DialogData>();
        dialogTexts.Add(new DialogData("Hey, meatbag! I just had a brilliant idea… and by brilliant, I mean completely illegal.", "Bender"));
        dialogTexts.Add(new DialogData("How about a little competition? You and I steal as much junk as we can from the old crackpot, and whoever gets the most loot wins. The prize? Everything we stole, of course!", "Bender"));
        var secondSelection = new DialogData("What you choose?");
        secondSelection.SelectList.Add(PlayerEmotions.POSITIVE, "A) That sounds fun! I’m in.");
        secondSelection.SelectList.Add(PlayerEmotions.FEARFUL, "B) What if the Professor notices?");
        secondSelection.SelectList.Add(PlayerEmotions.NEGATIVE, "C) That’s a crime, Bender!");
        secondSelection.Callback = () => OnSecondSelectionResult();
        dialogTexts.Add(secondSelection);
        DialogManager.Show(dialogTexts);
    }

    void OnSecondSelectionResult() {
        switch (DialogManager.Result) {

            case PlayerEmotions.POSITIVE:
                ShowDialog("/emote:Happy/ Ha! I like your attitude, rookie thief. But just so you know, I’m a pro.", "Bender");
                break;
            case PlayerEmotions.FEARFUL:
                ShowDialog("/emote:Resigned/ Pfft! That fossil barely remembers what he had for breakfast. Besides, I’ll just say it was you.", "Bender");
                break;
            case PlayerEmotions.NEGATIVE:
                ShowDialog("/emote:Waiting/ Oh wow, a goody-two-shoes. What’s next? Recycling and using eco-friendly batteries? Lame!", "Bender");
                break;
            default:
                break;
        }
    }

    void ShowDialog(string text, string character) {
        DialogManager.Show(new DialogData(text, character));
    }
}

        //dialogTexts.Add(new DialogData("/size:up/Hi, /size:init/my name is Li.", "Li"));

        //dialogTexts.Add(new DialogData("Let's start this test!", "Li"));

        //dialogTexts.Add(new DialogData("The idea is to create something very simple, make me react to the text and animate me in a few different ways.", "Li"));

        //dialogTexts.Add(new DialogData("Remember that you can change my sprite or background if you choose so, you can even go 3D if you're more experienced with that!", "Li"));

        //dialogTexts.Add(new DialogData("Anyways... Let's move on!", "Li"));

        //dialogTexts.Add(new DialogData("Create a branching option where you have to choose between 3 possible answers to give me.", "Li"));

        //dialogTexts.Add(new DialogData("Once you create the options and pick one of them I'll say: ", "Li"));

        //dialogTexts.Add(new DialogData("Yo choose option A!", "Li"));

        //dialogTexts.Add(new DialogData("Or...", "Li"));

        //dialogTexts.Add(new DialogData("Yo choose option B!", "Li"));

        //dialogTexts.Add(new DialogData("Or...", "Li"));

        //dialogTexts.Add(new DialogData("Yo choose option C!", "Li"));

        //dialogTexts.Add(new DialogData("After this, you'll send me to the SecondScene!", "Li"));

        //dialogTexts.Add(new DialogData("Where you'll have to create a VERY simple mini game and make me react to it.", "Li"));
