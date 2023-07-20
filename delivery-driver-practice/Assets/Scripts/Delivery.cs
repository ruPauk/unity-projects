using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delivery : MonoBehaviour
{
    private bool hasPackage = false;
    [SerializeField] float packageDestroyDelay = 0.2f;
    [SerializeField] Color32 noPackageColor = new Color32(1, 1, 1, 1);
    [SerializeField] Color32 hasPackageColor = new Color32(0, 0, 0, 1);
    SpriteRenderer carSpriteRenderer;

    void Start()
    {
        carSpriteRenderer = GetComponent<SpriteRenderer>();
    }



    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Package" && !hasPackage)
        {
            hasPackage = true;
            carSpriteRenderer.color = hasPackageColor;
            Destroy(collision.gameObject, packageDestroyDelay);
            Debug.Log("You have picked up a package (" + collision.gameObject.name + ")!");
        }
        if (collision.tag == "Destination" && hasPackage)
        {
            hasPackage = false;
            carSpriteRenderer.color = noPackageColor;
            Debug.Log("You have reached a destination point (" + collision.gameObject.name + ")!");
        }

    }
}
