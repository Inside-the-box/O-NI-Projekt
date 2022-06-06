using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;

public class BulletTarget : MonoBehaviour
{
    public float health = 10f;
    public ThirdPersonController player;
    public AudioSource audioSourceDead;            //  Audio source of the enemy when he dies.
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
            audioSourceDead.Play();
            Destroy(this.gameObject);
            this.player.monstersKilled++;
            this.player.textForPlayerUI.text = "Cilj: Premagati pošasti (" + this.player.monstersKilled + "/"+this.player.maxMonsters+")";
            if(this.player.monstersKilled >= this.player.maxMonsters)
            {
                this.player.goToNextScene();
            }
        }
    }
}
