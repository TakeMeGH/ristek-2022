using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashEffect : MonoBehaviour
{

    [SerializeField] Material flashMaterial;
    [SerializeField] int numFlashClip;
    float eachFlash;

    Material originalMaterial;
    SpriteRenderer spriteRenderer;
    PlayeHealthHandling playerHealthHandling;
    float flashDuration; 
    Coroutine flashRoutine;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerHealthHandling = GetComponent<PlayeHealthHandling>();
        flashDuration = playerHealthHandling.invulnerableDuration;
        originalMaterial = spriteRenderer.material;
        if(numFlashClip <= 1){
            numFlashClip = 1;
        }
        eachFlash = (float)(flashDuration / numFlashClip);
    }

    // Update is called once per frame
    void Update()
    {
    }


    public void flashObject(){
       if (flashRoutine != null)
        {
            StopCoroutine(flashRoutine);
        }
        flashRoutine = StartCoroutine(FlashRoutine());
    }

    private IEnumerator FlashRoutine()
    {
        for(int i = 0; i < numFlashClip; i++){
            spriteRenderer.material = flashMaterial;
            yield return new WaitForSeconds(eachFlash - 0.1f);
            spriteRenderer.material = originalMaterial;
            flashRoutine = null;
            yield return new WaitForSeconds(0.1f);

        }
       
    }
}
