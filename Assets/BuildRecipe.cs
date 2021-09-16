using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildRecipe : MonoBehaviour
{
    public string name = "Required Items";
    public InventoryItem item;
    public int amountRequired;

    /*
    [System.Serializable]
    public class itemAmounts
    {
        public string name = "Required Items";
        public InventoryItem item;
        public int amountRequired;
    }

    public itemAmounts[] items;
    public static BuildRecipe Instance;

    private void Awake() {
        BuildRecipe.Instance = this;
    }
    */
}
