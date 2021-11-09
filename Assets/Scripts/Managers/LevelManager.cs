using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelManager : MonoBehaviour
{
    [SerializeField] GameObject _loaderCanvas;
    [SerializeField] GameObject SettingsCanvas;
    
    public async void LoadScene(string sceneName)
    {
        var scene = SceneManager.LoadSceneAsync(sceneName);
    }

    public void Settings()
    {
        SettingsCanvas.SetActive(!SettingsCanvas.active);
    }
    
    public void Loaded()
    {
        _loaderCanvas.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
