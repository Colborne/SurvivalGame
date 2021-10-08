using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    InputManager input;
    Rigidbody rb;
    public GameObject fx;
    public InventoryItem item;

    public float speed;
    private void Awake() 
    {
        input = FindObjectOfType<InputManager>();
        rb = GetComponent<Rigidbody>();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        Ray ray = Camera.main.ScreenPointToRay(input.mousePosition);
        transform.rotation = Quaternion.LookRotation(ray.direction);
        speed = input.attackChargeTimer;
        rb.AddForce(transform.forward * 2500f * speed);
        input.attackChargeTimer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(rb.velocity != Vector3.zero)
            rb.rotation = Quaternion.LookRotation(rb.velocity);  
    }

    private void OnTriggerEnter(Collider collision) 
    { 
        if(collision.tag == "Hittable" || collision.tag == "Ground")
        {
            ObjectStats stats = collision.GetComponent<ObjectStats>();
            ResourceObject resource = collision.GetComponent<ResourceObject>();
            SoundManager sound = FindObjectOfType<InputManager>().GetComponent<SoundManager>();
            Tree tree = collision.GetComponent<Tree>();

            if (stats != null)
            {
                sound.PlaySound("Sounds/chop"); 
                stats.TakeDamage(10);
                for(int i = 0; i < Random.Range(1,3); i++)
                    Instantiate(resource.fx, transform.position + new Vector3(0,1,0), Random.rotation);

                if(tree != null)
                {   
                    tree.timeSinceInitialization = Time.timeSinceLevelLoad - tree.timer;                
                    if(tree.timeSinceInitialization > 1f)
                        tree.CheckHit(true);
                }
            }

            if (fx != null)
            {
                GameObject _fx = Instantiate(fx, transform.position, transform.rotation);
                _fx.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            }
            Destroy(gameObject);
        }
    }
}
