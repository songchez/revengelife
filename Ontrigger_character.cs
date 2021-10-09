using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ontrigger_character : MonoBehaviour
{
    bool onApple = false;
    bool onChange = false;
    public GameObject tree;
    void Update()
    {
        if (onApple && (Input.GetMouseButton(0) | Input.GetKey(KeyCode.Space)))
        {
            tree.GetComponent<SpriteRenderer>().sprite = null;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<SpriteRenderer>().sprite != null && other.gameObject.tag == "tree")
        {
            tree = other.gameObject;
            onApple = true;
            
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<SpriteRenderer>().sprite != null && other.gameObject.tag == "tree")
        {
            tree = null;
            onApple = false;
        }
    }
}
