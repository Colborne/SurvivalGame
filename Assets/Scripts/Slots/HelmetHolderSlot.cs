using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelmetHolderSlot : MonoBehaviour
{   
    public Transform parentOverride;
    public GameObject currentEquipmentModel;

    public void UnloadHelmet()
    {
        if(currentEquipmentModel != null)
        {
            currentEquipmentModel.SetActive(false);
        }
    }
    public void UnloadHelmetDestroy()
    {
        if(currentEquipmentModel != null)
        {
            Destroy(currentEquipmentModel);
        }
    }
    public void LoadHelmetModel(HelmetItem helmetItem)
    {
        UnloadHelmetDestroy();
        
        if(helmetItem == null)
        {
            UnloadHelmet();
            return;
        }
        
        GameObject model = Instantiate(helmetItem.modelPrefab) as GameObject;
        
        if(model != null)
        {
            if(parentOverride != null)
            {
                model.transform.parent = parentOverride;
            }
            else
            {
                model.transform.parent = transform;
            }

            model.transform.localPosition = Vector3.zero;
            model.transform.localRotation = Quaternion.identity;
            model.transform.localScale = Vector3.one;
        }
        currentEquipmentModel = model;
    }
}
