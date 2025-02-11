using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameController : MonoBehaviour
{
    public static MinigameController Instance { get; private set; }

    #region Main Control
    public Action onStartGame;
    public Action onGameOver;
    public Action<int> onChangeTime;
    //Limites para instanciar objetos
    [SerializeField] RectTransform _topLeftLimit;
    [SerializeField] RectTransform _downRightLimit;
    [SerializeField] List<GameObject> _collectables;

    [SerializeField] float _minTimeToInstantiate = 0.5f;
    [SerializeField] float _maxTimeToInstantiate = 1.5f;

    [SerializeField] int _gameTimeInSeconds = 60;
    int _remainingTime = 60;
    bool _gameRunning = false;
    Transform _container;

    void Awake() {
        if (Instance == null)
            Instance = this;
        _container = transform.Find("Container");
    }

    public void StartGame() {
        if (_gameRunning) return;
        _gameRunning = true;
        _remainingTime = _gameTimeInSeconds;
        _playerScore = 0;
        _benderScore = 0;
        StartCoroutine(InstanciateCollectables());
        StartCoroutine(RunTimer());
        onStartGame?.Invoke();
    }

    void GameOver() {
        PlayerPrefs.SetInt(PlayerPrefsKeys.PLAYER_RESULT, _playerScore);
        PlayerPrefs.SetInt(PlayerPrefsKeys.BENDER_RESULT, _benderScore);
        _gameRunning = false;
        onGameOver?.Invoke();
        GameManager.Instance.GoToNextScene();
    }

    IEnumerator RunTimer() {
        while (_remainingTime>=0) {
            onChangeTime?.Invoke(_remainingTime);
            yield return new WaitForSeconds(1);
            _remainingTime -= 1;
        }
        GameOver();
    }

    IEnumerator InstanciateCollectables() {
        while (_gameRunning) {
            var rnd = UnityEngine.Random.Range(_minTimeToInstantiate, _maxTimeToInstantiate);
            yield return new WaitForSeconds(rnd);
            InstanceCollectable();
        }
    }

    void InstanceCollectable() {
        if (!_gameRunning) return;
        if (_collectables == null || _collectables.Count == 0) {
            Debug.LogError("No hay objetos coleccionables en la lista.");
            return;
        }

        if (_topLeftLimit == null || _downRightLimit == null) {
            Debug.LogError("Los límites no están asignados.");
            return;
        }

        // Convertir límites de RectTransform a coordenadas locales en el Canvas
        Vector3[] topLeftCorners = new Vector3[4];
        Vector3[] downRightCorners = new Vector3[4];

        _topLeftLimit.GetWorldCorners(topLeftCorners);
        _downRightLimit.GetWorldCorners(downRightCorners);

        float minX = topLeftCorners[0].x; // Esquina inferior izquierda
        float maxX = downRightCorners[2].x; // Esquina superior derecha
        float minY = downRightCorners[0].y;
        float maxY = topLeftCorners[2].y;

        Vector2 randomPosition = new Vector2(
            UnityEngine.Random.Range(minX, maxX),
            UnityEngine.Random.Range(minY, maxY)
        );
        GameObject collectablePrefab = _collectables[UnityEngine.Random.Range(0, _collectables.Count)];
        GameObject instance = Instantiate(collectablePrefab, randomPosition, Quaternion.identity, _container);
        instance.transform.SetAsLastSibling();
    }

    #endregion

    #region Scores
    int _playerScore = 0;
    int _benderScore = 0;
    public int PlayerScore { get => _playerScore; }
    public int BenderScore { get => _benderScore; }
    public System.Action onChangeScores;
    public void AddScorePlayer() {
        _playerScore++;
        onChangeScores?.Invoke();
    }

    public void AddScoreBender() {
        _benderScore++;
        onChangeScores?.Invoke();
    }
    #endregion

}
