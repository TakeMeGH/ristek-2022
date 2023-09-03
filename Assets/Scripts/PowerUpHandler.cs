using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PowerUpHandler : MonoBehaviour
{

    [SerializeField] int healAmount;
    AudioSource audioSource;
    [SerializeField] AudioClip healAudio;
    [SerializeField] AudioClip gunAudio;

    private void Start() {
        audioSource = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player" && gameObject.tag == "gunPowerUp"){
            AudioSource playerAudio = other.gameObject.GetComponent<AudioSource>();
            playerAudio.PlayOneShot(gunAudio);
            other.gameObject.GetComponent<PlayerController>().lastTimePowerUp = Time.time;
            Destroy(gameObject);
        }
        else if(other.gameObject.tag == "Player" && gameObject.tag == "healthPowerUp"){
            PlayeHealthHandling playerHealth = other.gameObject.GetComponent<PlayeHealthHandling>();
            if(playerHealth.currentHealth == playerHealth.maxHealth) return;
            AudioSource playerAudio = other.gameObject.GetComponent<AudioSource>();
            playerAudio.PlayOneShot(healAudio, 0.7f);
            playerHealth.currentHealth += healAmount;
            if(playerHealth.currentHealth > playerHealth.maxHealth){
                playerHealth.currentHealth = playerHealth.maxHealth;
            }
            playerHealth.refreshHealthBar();
            Destroy(gameObject);

        }
        
    }
}
