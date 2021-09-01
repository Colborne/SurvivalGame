using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentHolderSlot : MonoBehaviour
{   
    public Transform parentOverride;
    public GameObject currentEquipmentModel;

    public void UnloadEquipment()
    {
        if(currentEquipmentModel != null)
        {
            currentEquipmentModel.SetActive(false);
        }
    }
    public void UnloadEquipmentDestroy()
    {
        if(currentEquipmentModel != null)
        {
            Destroy(currentEquipmentModel);
        }
    }
    public void LoadEquipmentModel(EquipmentItem equipmentItem)
    {
        UnloadEquipmentDestroy();
        
        if(equipmentItem == null)
        {
            UnloadEquipment();
            return;
        }
        
        GameObject model = Instantiate(equipmentItem.modelPrefab) as GameObject;
        
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
