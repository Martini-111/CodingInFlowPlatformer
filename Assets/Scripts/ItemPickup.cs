using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemPickup : MonoBehaviour
{
    public int pineapple_count = 0;
    [SerializeField] private Text pineapplesText;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Pineapple")){
            Destroy(collision.gameObject);
            pineapple_count++;
            //Debug.Log("Pineapples: " + pineapple_count);
            pineapplesText.text = "Pineapples: " + pineapple_count;
        }
    }
}
