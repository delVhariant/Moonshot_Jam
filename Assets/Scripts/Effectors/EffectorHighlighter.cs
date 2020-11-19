using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectorHighlighter : MonoBehaviour
{
    public MeshRenderer highlightMesh;
    public EffectorBase effector;
    bool select = false;
    
    // Start is called before the first frame update
    void Start()
    {
        highlightMesh = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(select)
        {
            select = false;
            highlightMesh.enabled = false;
            effector.MoveEffector();
        }
    }

    
    void OnMouseOver()
    {
        if(highlightMesh && EffectorSpawner.effectorSpawner.state == SpawnState.Idle && GameState.gameManager.gamePhase == GamePhase.Planning)
        {
            // Reset the color of the GameObject back to normal
            highlightMesh.enabled = true;
        }
    }

    void OnMouseDown()
    {
        if(EffectorSpawner.effectorSpawner.state == SpawnState.Idle && GameState.gameManager.gamePhase == GamePhase.Planning)
        {
            select = true;            
        }
    }

    void OnMouseExit()
    {
        if(highlightMesh &&  highlightMesh.enabled)
        {
            // Reset the color of the GameObject back to normal
            highlightMesh.enabled = false;
        }
    }
}
