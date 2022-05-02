using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTarget : MonoBehaviour
{
    public float health = 10f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void dealDamage(float damage)
    {
        this.health -= damage;
        if(health <= 0)
        {
            //Ko umre
            Debug.Log("Umru sm");
            Destroy(this.gameObject);
        }
    }
}
