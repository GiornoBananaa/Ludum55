using UnityEngine;
using UnityEngine.UI;

public class PopUpOn : MonoBehaviour
{
    private Button _button;
    [SerializeField] private GameObject _popUp;

    void Start()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(() => PopUp());
    }

    private void PopUp()
    {
        _popUp.gameObject.SetActive(true);
    }
}
