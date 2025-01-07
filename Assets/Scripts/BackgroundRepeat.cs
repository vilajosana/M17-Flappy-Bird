using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundRepeat : MonoBehaviour
{
    private float spriteWidht;


    void Start()
    {
        BoxCollider2D groundCollider = GetComponent<BoxCollider2D>();
        spriteWidht = groundCollider.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x < -spriteWidht)
        {
            ResetPosition();
        }
        
    }

    private void ResetPosition()
    {
        transform.Translate(new Vector3(2* spriteWidht,0f,0f));
    }
}
