using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField] float _minTimeToDestroy = 0.5f;
    [SerializeField] float _maxTimeToDestroy = 2f;

    void Awake() {
        Destroy(gameObject, Random.Range(_minTimeToDestroy, _maxTimeToDestroy));
    }

    public void Collect(bool isPlayer) {
        if (isPlayer)
            MinigameController.Instance.AddScorePlayer();
        else
            MinigameController.Instance.AddScoreBender();
        Destroy(gameObject);
    }
}
