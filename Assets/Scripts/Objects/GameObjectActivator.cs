using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameObjectActivator : MonoBehaviour
{
    public GameObject objectToActivate;
    // Start is called before the first frame update
    void Start()
    {
        if(File.Exists(Application.persistentDataPath + "/mako.seed"))
            Invoke("Activate", 2f);
        else
            Invoke("Activate", 6f);
    }

    void Activate()
    {
        objectToActivate.SetActive(true);
    }
}
