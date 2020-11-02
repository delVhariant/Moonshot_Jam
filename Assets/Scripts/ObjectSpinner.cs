using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpinner : MonoBehaviour
{
    public float speed = 3;
    public float spinX = 0;
    public float spinY = 0;
    public float spinZ = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(spinX, spinY, spinZ) * speed * Time.deltaTime, Space.Self);
    }
}
