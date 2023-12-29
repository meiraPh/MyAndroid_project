using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class touchController : MonoBehaviour
{
    
    public Vector2 pastPosition;
    public float velocity = 1f;
    
    public float limit = 4f;
    public Vector2 limitVector = new Vector2(-4, 4);

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Move(Input.mousePosition.x - pastPosition.x);
        }   
        pastPosition = Input.mousePosition;
    }

    public void Move(float speed)
    {
        Vector3 dislocation = Vector3.right * Time.deltaTime * speed * velocity;
        Vector3 finalPosition = transform.localPosition + dislocation;

        if(finalPosition.x < -limit)
        {
            finalPosition.x = -limit;
        }
        else if(finalPosition.x > limit)
        {
            finalPosition.x = limit;
        }

        transform.localPosition = finalPosition;
        
    }

}
