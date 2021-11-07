using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameObjectActivator : MonoBehaviour
{
    public GameObject objectToActivate;
    public void Activate()
    {
        objectToActivate.SetActive(true);
    }
}
