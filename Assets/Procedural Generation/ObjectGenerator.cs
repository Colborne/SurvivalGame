using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGenerator : MonoBehaviour
{
    [System.Serializable]
    public class biome
    {
        public string name;
        public float minHeight;
        public float maxHeight;
        public GameObject[] objectToSpawn;
        public int[] amount;

    }
    
    
    public biome[] biomes; //Add multiple arrays that are biome based

    public int meshHeight;
    public float meshX;
    public float meshZ;
     
    private void Start() 
    {
        Invoke("spawn", .1f);
    }
    
    void spawn()
    {
        int place = 0;
        int attempts = 0;

        for(int i = 0; i < biomes.Length; i++)
        {
            for(int x = 0; x < biomes[i].objectToSpawn.Length; x++)
            {
                for(int y = 0; y < biomes[i].amount[x]; y++)
                {
                    attempts++; 
                    for(int t = 0; t < 1000; t++)
                    {                        
                        Vector3 start = new Vector3(
                        Random.Range(transform.position.x - meshX/2, transform.position.x + meshX/2), 
                        transform.position.y + meshHeight,
                        Random.Range(transform.position.z - meshZ/2, transform.position.x + meshZ/2));
                        
                        RaycastHit hit;
                        
                        if(Physics.Raycast(start, Vector3.down, out hit) 
                        && hit.collider.CompareTag("Ground") 
                        && hit.point.y > biomes[i].minHeight 
                        && hit.point.y < biomes[i].maxHeight)  //Check For Height to check what array to check
                        {
                            GameObject placed = Instantiate(biomes[i].objectToSpawn[x], new Vector3(start.x, hit.point.y, start.z) , Quaternion.identity); //Random.Range(0,biomes[i].objectToSpawn.Length-1
                            place++;
                            break;
                        }
                    }
                }
            }
        }

        Debug.Log("There were " + place + " placed objects. Out of " + attempts + " attempts.");
    }
}
