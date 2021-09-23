using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGenerator : MonoBehaviour
{
    public GameObject[] objectToSpawn; //Add multiple arrays that are biome based
    public int amount;
    public int meshHeight;
    public float meshX;
    public float meshZ;
     
    private void Start() 
    {
        Invoke("spawn", .1f);
    }
    
    void spawn()
    {
        int fails = 0;
        for(int i = 0; i < amount; i++)
        {
            Vector3 start = new Vector3(
            Random.Range(transform.position.x - meshX/2, transform.position.x + meshX/2), 
            transform.position.y + meshHeight,
            Random.Range(transform.position.z - meshZ/2, transform.position.x + meshZ/2));

            RaycastHit hit;
            if(Physics.Raycast(start, Vector3.down, out hit) && hit.collider.CompareTag("Ground"))  //Check For Height to check what array to check
            {
                GameObject placed = Instantiate(objectToSpawn[Random.Range(0,objectToSpawn.Length-1)], new Vector3(start.x, hit.point.y, start.z) , Quaternion.identity);
            }
            else
            {
                fails++;
            }
        }

        Debug.Log(fails);
    }
}
