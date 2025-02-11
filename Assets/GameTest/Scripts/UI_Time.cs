using UnityEngine;

public class UI_Time : MonoBehaviour
{
    TMPro.TextMeshProUGUI _tmpro;

    void Awake() {
        _tmpro = GetComponent<TMPro.TextMeshProUGUI>();
        _tmpro.text = "";
    }

    void OnEnable() {
        MinigameController.Instance.onChangeTime += UpdateTime;
    }

    void OnDisable() {
        MinigameController.Instance.onChangeTime -= UpdateTime;
    }

    void UpdateTime(int obj) {
        _tmpro.text = obj.ToString();
    }
}
