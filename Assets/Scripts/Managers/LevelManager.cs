using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class LevelManager : MonoBehaviour
{
    [SerializeField] GameObject _loaderCanvas;
    [SerializeField] GameObject SettingsCanvas;
    [SerializeField] TMP_InputField input;
    [SerializeField] TMP_Dropdown characters;
    
    public async void LoadScene(string sceneName)
    {
        if(characters.value == 0)
            PersistentData.name = input.text;
        else
            PersistentData.name = characters.options[characters.value].text;  
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
