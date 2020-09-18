using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SwordController : MonoBehaviour
{

    public GameObject sword;
    public float pickupRange;
    public LayerMask pickupLayers;
    private Animator anim;
    public bool swinging;
    public float swingCooldown;



    //Outline vars
    public GameObject outlinedPickup = null;
    public Material noAlpha;
    public Material outline;

    //Pickup vars
    private bool hasBlade;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!swinging)
        {
            PointToMouse();
        }

        GameObject newClosestOutline = FindClosestPickup();
        if(newClosestOutline != outlinedPickup)
        {
            if (outlinedPickup != null)
            {
                outlinedPickup.GetComponentInChildren<SpriteRenderer>().material = noAlpha;
            }
            outlinedPickup = newClosestOutline;
            if (outlinedPickup != null)
            {
                outlinedPickup.GetComponentInChildren<SpriteRenderer>().material = outline;
            }
        }

        if (Input.GetMouseButtonDown(0) && !swinging)
        {
            SwingBlade();
        }
    }

    /*
     * Returns a GameObject that is the closest pickup within pickRange of the sword
     * Returns NULL if no pickups are nearby
     */ 
    private GameObject FindClosestPickup()
    {
        Collider2D[] foundPickups = Physics2D.OverlapCircleAll(transform.position, pickupRange, pickupLayers);

        GameObject closestPickup = null;
        float distFromClosest = pickupRange * 2;
        for(int i = 0; i < foundPickups.Length; i++)
        {
            var distFromi = Vector2.Distance(foundPickups[i].transform.position, transform.position);
            if(distFromi < distFromClosest)
            {
                closestPickup = foundPickups[i].gameObject;
                distFromClosest = distFromi;
            }
        }
        return closestPickup;
    }

    private void PointToMouse()
    {
        Vector3 mousePos;
        Vector3 objPos;


        mousePos = Input.mousePosition;
        mousePos.z = 10; //The distance between the camera and object
        objPos = Camera.main.WorldToScreenPoint(transform.position);
        mousePos.x = mousePos.x - objPos.x;
        mousePos.y = mousePos.y - objPos.y;
        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;

        transform.DOLocalRotate(new Vector3(0, 0, angle - 90), .1f);
    }

    public void PickUpBlade(GameObject pickup)
    {
        pickup.transform.position = transform.position + new Vector3(0, 2f, 0);
        pickup.transform.SetParent(transform);
        hasBlade = true;
    }

    public void DiscardBlade()
    {
        hasBlade = false;
    }
    public void SwingBlade()
    {
        swinging = true;
        anim.SetTrigger("Swinging");
        StartCoroutine(ResetSwinging(swingCooldown));

       if(outlinedPickup != null)
        {
            PickUpBlade(outlinedPickup);
        }
    }
    private IEnumerator ResetSwinging(float delay)
    {
        yield return new WaitForSeconds(delay);
        swinging = false;
    }
}
