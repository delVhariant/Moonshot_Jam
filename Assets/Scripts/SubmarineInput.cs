using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmarineInput : MonoBehaviour
{
    public delegate void Launch();
    public static event Launch OnLaunch;

    public Transform Submarine;
    public GameObject arrow;

    public float minArrowLength = 0.5f;
    public float maxArrowLength = 8f;
    float initForce = 0;


    void Awake()
    {
        //myInput = GetComponent<PlayerInput>();
        /*controls = new Controls();
        controls.Enable();*/
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!GameState.IsStarted())
        {
            if(Input.GetMouseButton(0))
            {
                Vector3 point = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.x));                
                if(arrow)
                {
                    if(!arrow.activeSelf)
                        arrow.SetActive(true);

                    Vector3 arr = (point + Submarine.position) * 0.5f;
                    //arr.z = Submarine.position.z;
                    arrow.transform.position = arr;
                    arrow.transform.LookAt(point);

                    float dist = Mathf.Clamp(Vector3.Distance(point, Submarine.position)/10, minArrowLength, maxArrowLength);
                    initForce = dist;
                    arrow.transform.localScale = new Vector3(1, 1, dist);
                    Color c = Color.green;
                    if(dist <=maxArrowLength/5)
                    {
                        c = Color.green;
                    }
                    else if(dist <= maxArrowLength/3)
                    {
                        c = Color.yellow;
                    }
                    else
                    {
                        c = Color.red;
                    }
                    SpriteRenderer[] sprites = arrow.GetComponentsInChildren<SpriteRenderer>();

                    for(int i = 0;i < sprites.Length; i++)
                    {
                        sprites[i].color = Color.Lerp(sprites[i].color, c, dist/maxArrowLength);
                    }
                }
                Submarine.LookAt(point);
            }
            else if(Input.GetMouseButtonUp(0))
            {
                arrow.SetActive(false);
                if(OnLaunch != null)
                    OnLaunch();
                //GameState.StartRun();
                Submarine.GetComponent<Rigidbody>().AddForce(Submarine.forward * 500 * initForce, ForceMode.Force);
            }
        }
    }
}
