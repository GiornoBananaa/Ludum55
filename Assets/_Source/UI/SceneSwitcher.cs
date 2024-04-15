using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneSwitcher : MonoBehaviour
{
    private Button _button;
    [SerializeField] private string _sceneName;

    void Start()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(() => NextScene());
    }
    private void NextScene()
    {
        SceneManager.LoadScene(_sceneName);
    }
}