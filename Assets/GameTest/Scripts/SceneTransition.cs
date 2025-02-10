using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransition : MonoBehaviour
{
    [SerializeField] string _sceneName = "";
    [SerializeField] Image _targetImage; 
    [SerializeField] float _fadeDuration = 0.5f;

    void OnEnable() {
        GameManager.Instance.OnGoToNextScene += GoToScene;
    }

    void OnDisable() {
        GameManager.Instance.OnGoToNextScene -= GoToScene;
    }

    void Start() {
        StartCoroutine(FadeCoroutine(false));
    }

    void GoToScene() {
        StartCoroutine(FadeCoroutine(true));
    }

    IEnumerator FadeCoroutine(bool fadeIn) {
        if (_targetImage == null) {
            Debug.LogError("No se asignó una imagen.");
            yield break;
        }

        Color color = _targetImage.color;
        float startAlpha = fadeIn ? 0f : 1f;
        float endAlpha = fadeIn ? 1f : 0f;

        color.a = startAlpha;
        _targetImage.color = color;

        float elapsedTime = 0f;

        while (elapsedTime < _fadeDuration) {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / _fadeDuration);
            _targetImage.color = new Color(color.r, color.g, color.b, alpha);
            yield return null;
        }

        color.a = endAlpha;
        _targetImage.color = color;

        // Si es un fade in y hay una escena para cargar, la cargamos
        if (fadeIn && SceneManager.GetSceneByName(_sceneName) != null)
            GameManager.Instance.GoToScene(_sceneName);
        else if (fadeIn)
            Debug.LogError("Escena no existe");
    }
}
