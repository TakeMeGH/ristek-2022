using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayeHealthHandling : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public int currentHealth;
    [SerializeField] public int maxHealth = 10;
    [SerializeField] GameObject healthBar;

    [SerializeField] GameControl gameControl;
    [SerializeField] public float invulnerableDuration;
    FlashEffect flashEffect;

    Vector3 normalScale;
    float lastTimeTakingDamage = -100;
    void Start()
    {
        currentHealth = maxHealth;
        normalScale = healthBar.transform.localScale;
        flashEffect = GetComponent<FlashEffect>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void refreshHealthBar(){
        float curHealthPercentage = (float)currentHealth / maxHealth;
        healthBar.transform.localScale = new Vector3(curHealthPercentage * normalScale.x, normalScale.y, normalScale.z);
    }

    public bool isInvulnerable(){
        if(Time.time - lastTimeTakingDamage <= invulnerableDuration){
            return true;
        }
        else{
            return false;
        }
    }

    public void takingDamage(int damage){
        if(isInvulnerable()) return;
        lastTimeTakingDamage = Time.time;
        flashEffect.flashObject();
        currentHealth -= damage;
        refreshHealthBar();
        if(currentHealth <= 0){
            Destroy(gameObject);
            gameControl.GameOver();
        }
    }

}
