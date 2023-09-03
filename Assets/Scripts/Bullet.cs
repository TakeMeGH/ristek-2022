using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float speed;
    [SerializeField] private Vector3 direction;
    [SerializeField] GameObject bullet;
    
    float duration = 0.7f;
    
    // face = True -> right
    // face = false -> left
    public void Init(Vector3 direction, bool face, bool isExtraBullet, bool powerUP)
    {
        this.direction = direction;
        this.transform.SetParent(null);
        float z = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        if(!isExtraBullet){
            if(face){
                if(z > 45){
                    z = 45;
                }
                else if(z < -45){
                    z = -45;
                }
            }
            else{
                if(z < 135 && z >= 0){
                    z = 135;    
                }
                else if(z > -135 && z < 0){
                    z = -135;
                }
            }
        }
        transform.rotation = Quaternion.Euler(0, 0, z);
        if(powerUP){
            GameObject bulletSpawn = Instantiate(bullet, transform.position, transform.rotation);
            bulletSpawn.GetComponent<Bullet>().rotateBullet(Quaternion.Euler(0, 0, z - 25));
            bulletSpawn = Instantiate(bullet, transform.position, transform.rotation);
            bulletSpawn.GetComponent<Bullet>().rotateBullet(Quaternion.Euler(0, 0, z + 25));
        }
    }
    void rotateBullet(Quaternion bulletRotation){
        this.transform.rotation = bulletRotation;
    }
    // Update is called once per frame
    void Update()
    {
        this.transform.position += transform.right * speed * Time.deltaTime;
        duration -= Time.deltaTime;
        if(duration <= 0){
            Destroy(gameObject);
        }
    }

    // TODO : Implementasi behaviour bullet jika mengenai wall atau enemy
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "enemy"){
            other.gameObject.GetComponent<EnemyController>().enemyDead();
            Destroy(gameObject);
        }
        else if(other.gameObject.tag == "wall"){
            Destroy(gameObject);
        }
    }
}
