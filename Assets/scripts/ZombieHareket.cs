using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieHareket : MonoBehaviour
{
    private GameObject oyuncu;
    public GameObject kalp;
    private int zombiCan=3;
    private float mesafe;
    void Start()
    {
        oyuncu = GameObject.Find("FPSController");
    }

    
    void Update()
    {
        GetComponent<NavMeshAgent>().destination = oyuncu.transform.position;
        mesafe = Vector3.Distance(transform.position, oyuncu.transform.position);
        
        if (mesafe < 1f)
        {
            GetComponentInChildren<Animation>().Play("Zombie_Attack_01");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.tag.Equals("bullet")){
            Debug.Log("zombi vuruldu");
            zombiCan--;
            if (zombiCan == 0)
            {
                Instantiate(kalp, transform.position,Quaternion.identity);
                GetComponentInChildren<Animation>().Play("Zombie_Death_01");
                Destroy(this.gameObject, 1.667f);
            }
        }
    }
}
