using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGivingDamage : MonoBehaviour
{

    [SerializeField] PlayeHealthHandling playerHealth;
    [SerializeField] int damage;

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Player"){
            playerHealth.takingDamage(damage);

        }
    }
}
