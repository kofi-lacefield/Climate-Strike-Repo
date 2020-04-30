using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class Interactable : MonoBehaviour
{
    // Start is called before the first frame update
    public float radius = 3f;
    public bool playerInside = false;
    OmnisceneScript dontDestroy;
    int counter = 0;
    float alpha = 0.5f;
    private void Start()
    {
        CircleCollider2D circleCollider = gameObject.AddComponent<CircleCollider2D>() as CircleCollider2D;
        circleCollider.radius = radius;
        circleCollider.isTrigger = true;
        dontDestroy = GameObject.FindObjectOfType<OmnisceneScript>();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            interactablePopUp();
            playerInside = true;
        }
    }

    public void interactablePopUp()
    {
        GameObject popUp;
        popUp = Instantiate(dontDestroy.interactionKeyPopup, gameObject.transform.position, Quaternion.identity);
        popUp.GetComponentInChildren<TextMeshProUGUI>().color = new Color(1f,1f,1f, alpha);
        popUp.GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f, alpha);
        StartCoroutine(popUpDrift(popUp));
    }

    IEnumerator popUpDrift(GameObject popUp)
    {
        yield return new WaitForSeconds(0.1f);

        // Code to execute after the delay
        alpha += 0.05f;
        popUp.GetComponent<Transform>().position = new Vector3(popUp.GetComponent<Transform>().position.x, popUp.GetComponent<Transform>().position.y + 0.1f, popUp.GetComponent<Transform>().position.z);
        popUp.GetComponentInChildren<TextMeshProUGUI>().color = new Color(1f, 1f, 1f, alpha);
        popUp.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, alpha);
        counter++;
        if (counter == 10) 
        {
            StopCoroutine(popUpDrift(popUp));
        } else
        {
            StartCoroutine(popUpDrift(popUp));
        }
        
    }
}
