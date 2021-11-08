using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelManager : MonoBehaviour
{
    [SerializeField] GameObject _loaderCanvas;
    public async void LoadScene(string sceneName)
    {
        var scene = SceneManager.LoadSceneAsync(sceneName);
    }
    
    public void Loaded()
    {
        _loaderCanvas.SetActive(false);
    }
}
