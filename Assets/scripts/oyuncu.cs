using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class oyuncu : MonoBehaviour
{
    public Transform mermiPos;
    public GameObject mermi;
    public GameObject patlama;
    public Image canImg;
    private float can = 100f;
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            GameObject go = Instantiate(mermi, mermiPos.position, mermiPos.rotation) as GameObject;
            GameObject goPatlama = Instantiate(patlama, mermiPos.position, mermiPos.rotation) as GameObject;
            go.GetComponent<Rigidbody>().velocity = mermiPos.transform.forward*50f;
            Destroy(go.gameObject,2f);
            Destroy(goPatlama.gameObject,2f);
            Debug.Log("ateþ edildi");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.tag.Equals("zombi"))
        {
            Debug.Log("zombi vurdu");
            can -= 10f;
            canImg.fillAmount = can / 100f;
            canImg.color = Color.Lerp(Color.red, Color.green, can / 100f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("kalp"))
        {
            can += 10f;
            canImg.fillAmount = can / 100f;
            canImg.color = Color.Lerp(Color.red, Color.green, can / 100f);
            Destroy(other.gameObject);
        }
    }
}
