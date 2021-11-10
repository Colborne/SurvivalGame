using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;
using System.Linq;

public class DirectoryLoader : MonoBehaviour
{   
    [SerializeField] TMP_Dropdown characters;
    [SerializeField] GameObject input;
    [SerializeField] GameObject label;
    void Awake()
    {
        string[] files = System.IO.Directory.GetFiles(Application.persistentDataPath,"*.plyr");
        string[] names = new string[files.Length];

        for(int i = 0; i < names.Length; i++)
            names[i] = files[i].Split('/').Where(x => !string.IsNullOrWhiteSpace(x)).Last().Split('.').Where(x => !string.IsNullOrWhiteSpace(x)).First().Substring(5);
        
        List<string> plyr = names.ToList();

        string[] array = { "New" };
        List<string> list = new List<string>(array);

        characters.ClearOptions();
        characters.AddOptions(list);
        characters.AddOptions(plyr);
        characters.value = 0;
        characters.RefreshShownValue();
    }

    private void Update() 
    {
        if(characters.value == 0){
            input.SetActive(true);
            label.SetActive(false);
        }
        else
        {
            input.SetActive(false);  
            label.SetActive(true);
        }   
    }
}
