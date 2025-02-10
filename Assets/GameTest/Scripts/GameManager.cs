using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public static GameManager Instance { get; private set; }
    public Action OnGoToNextScene;
    void Awake() {
        if (Instance == null)
            Instance = this;
    }

    public void GoToNextScene() {
        OnGoToNextScene?.Invoke();
    }

    public void GoToScene(string sceneName) {
        SceneManager.LoadScene(sceneName);
    }
}
