using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmarineInput : MonoBehaviour
{
    public delegate void Launch();
    public static event Launch OnLaunch;

    public Transform Submarine;
    public GameObject arrow;
    GameObject lastArrow;


    public float minArrowLength = 0.5f;
    public float maxArrowLength = 8f;
    public float forceMulti = 10;
    float initForce = 0;


    void Awake()
    {
        //myInput = GetComponent<PlayerInput>();
        /*controls = new Controls();
        controls.Enable();*/
        GameState.gameManager.sub = Submarine;
        GameState.OnReset += ResetSub;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void ResetSub()
    {
        Submarine.GetComponent<CapsuleCollider>().enabled = false;
        Submarine.GetComponent<Rigidbody>().isKinematic = true;
        Submarine.GetComponent<Rigidbody>().isKinematic = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!GameState.IsStarted() && GameState.gameManager.gamePhase == GamePhase.Aiming)
        {
            Vector3 point = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.x));         
            if(arrow)
            {
                if(!arrow.activeSelf)
                    arrow.SetActive(true);

                if(lastArrow && !lastArrow.activeSelf)
                    lastArrow.SetActive(true);

                Vector3 arr = (point + Submarine.position) * 0.5f;
                //arr.z = Submarine.position.z;
                arrow.transform.position = arr;
                arrow.transform.LookAt(point);

                float dist = Mathf.Clamp(Vector3.Distance(point, Submarine.position)/10, minArrowLength, maxArrowLength);
                initForce = dist;
                //Debug.DrawRay(Submarine.position, Submarine.forward * initForce * forceMulti, Color.blue);
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
            
            if(Input.GetMouseButtonDown(0))
            {
                if(!lastArrow)
                {
                    lastArrow = Instantiate(arrow);
                    SpriteRenderer[] sprites = lastArrow.GetComponentsInChildren<SpriteRenderer>();

                    for(int i = 0;i < sprites.Length; i++)
                    {
                        Color c = new Color(0.5f, 0.5f, 0.5f, 0.3f); //new Color(sprites[i].color.r, sprites[i].color.g, sprites[i].color.b, 0.5f);
                        sprites[i].color = c;
                    }
                }
                lastArrow.transform.localScale = arrow.transform.localScale;
                lastArrow.transform.position = arrow.transform.position;
                lastArrow.transform.localRotation = arrow.transform.localRotation;
                lastArrow.SetActive(false);
                
                arrow.SetActive(false);
                if(OnLaunch != null)
                    OnLaunch();

                Submarine.GetComponent<CapsuleCollider>().enabled = true;
                //Submarine.GetComponent<ConstantForce>().force = Submarine.forward * initForce * forceMulti;
                //Submarine.GetComponent<SubmarineController>().SetForce(initForce * forceMulti);
                Submarine.GetComponent<Rigidbody>().AddForce(Submarine.forward * initForce * forceMulti, ForceMode.Force);
                Submarine.GetComponent<Rigidbody>().useGravity = true;

                
            }
        }
    }

    void OnDestroy()
    {
        GameState.OnReset -= ResetSub;
    }
}
