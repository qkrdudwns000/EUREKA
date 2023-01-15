using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBullet : MonoBehaviour
{
    public int damage;
    public bool isMelee;


    private void OnTriggerEnter(Collider other)
    {
        if (!isMelee && other.gameObject.tag == "Ground")
            Destroy(this.gameObject);
        else if(other.gameObject.tag == "Player")
        {
            other.GetComponent<PlayerController>().TakeDamage(damage);
            Destroy(this.gameObject);
        }
    }

}
