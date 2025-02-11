using System.Collections;
using UnityEngine;

public class BenderAI : MonoBehaviour
{
    [SerializeField] Transform _container;
    bool _playing = false;

    void OnEnable() {
        MinigameController.Instance.onStartGame += StartGame;
        MinigameController.Instance.onGameOver += GameOver;
    }

    void OnDisable() {
        MinigameController.Instance.onStartGame -= StartGame;
        MinigameController.Instance.onGameOver -= GameOver;
    }

    void StartGame() {
        _playing = true;
        StartCoroutine(CatchCollectables());
    }

    IEnumerator CatchCollectables() {
        while (_playing) {
            yield return new WaitForSeconds(UnityEngine.Random.Range(0.5f,2));
            if (_container.childCount>0) {
                _container.GetChild(0).GetComponent<Collectable>().Collect(isPlayer: false);
            }
        }
    }

    void GameOver() {
        _playing = false;
        StopAllCoroutines();
    }
}
