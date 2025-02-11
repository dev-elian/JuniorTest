using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using System.Collections;

public class UI_Score : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _playerScore;
    [SerializeField] TextMeshProUGUI _benderScore;
    [SerializeField] Image _playerImage;
    [SerializeField] Image _benderimage;
    [SerializeField] Sprite _playerImageNormal;
    [SerializeField] Sprite _benderImageNormal;
    [SerializeField] Sprite _playerImageLosing;
    [SerializeField] Sprite _benderImageLosing;
    [SerializeField] Sprite _playerImageWinning;
    [SerializeField] Sprite _benderImageWinning;

    [SerializeField] List<string> _benderLosingDialogs;
    [SerializeField] List<string> _benderWinningDialogs;
    bool _playing = false;

    void OnEnable() {
        MinigameController.Instance.onChangeScores += UpdateScoreUI;
        MinigameController.Instance.onStartGame += StartGame;
        MinigameController.Instance.onGameOver += GameOver;
    }

    void OnDisable() {
        MinigameController.Instance.onStartGame -= StartGame;
        MinigameController.Instance.onChangeScores -= UpdateScoreUI;
        MinigameController.Instance.onGameOver -= GameOver;
    }

    void StartGame() {
        _playing = true;
        StartCoroutine(ChangeExpresions());
        StartCoroutine(ShowComments());
    }

    IEnumerator ChangeExpresions() {
        while (_playing) {
            int diff = MinigameController.Instance.PlayerScore - MinigameController.Instance.BenderScore;
            if (diff > 5) {
                _playerImage.sprite = _playerImageWinning;
                _benderimage.sprite = _benderImageLosing;
            } else if (diff < -5) {
                _playerImage.sprite = _playerImageLosing;
                _benderimage.sprite = _benderImageWinning;
            } else {
                _playerImage.sprite = _playerImageNormal;
                _benderimage.sprite = _benderImageNormal;
            }
            yield return new WaitForSeconds(3);
        }
    }
    IEnumerator ShowComments() {
        while (_playing) {
            int diff = MinigameController.Instance.PlayerScore - MinigameController.Instance.BenderScore;
            if (diff > 5) {
                SecondSceneScript.Instance.ShowDialog(GetBenderLosingDialog(), "Bender");
                yield return new WaitForSeconds(10);
            } else if (diff < -5) {
                SecondSceneScript.Instance.ShowDialog(GetBenderWinningDialog(), "Bender");
                yield return new WaitForSeconds(10);
            } else {
                yield return new WaitForSeconds(2);
            }
        }
    }

    string GetBenderWinningDialog() {
        int rnd = UnityEngine.Random.Range(0, _benderWinningDialogs.Count);
        string phrase = _benderWinningDialogs[rnd];
        _benderWinningDialogs.RemoveAt(rnd);
        return phrase;
    }

    string GetBenderLosingDialog() {
        int rnd = UnityEngine.Random.Range(0, _benderLosingDialogs.Count);
        string phrase = _benderLosingDialogs[rnd];
        _benderLosingDialogs.RemoveAt(rnd);
        return phrase;
    }

    void GameOver() {
        _playing = false;
        StopAllCoroutines();
    }

    void UpdateScoreUI() {
        _playerScore.text = MinigameController.Instance.PlayerScore.ToString();
        _benderScore.text = MinigameController.Instance.BenderScore.ToString();
    }
}
