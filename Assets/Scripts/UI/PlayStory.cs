using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayStory : MonoBehaviour
{
    public List<string> dialog;
    public float enterDelay = 0.5f;
    public float wordDelay = 0.1f;
    public TMP_Text tbOne;
    public TMP_Text tbTwo;
    TMP_Text currentTB;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayDialog()
    {
        tbOne.text = "";
        tbTwo.text = "";
        currentTB = tbOne;
        StartCoroutine(StartLoop(currentTB, dialog));        
    }

    IEnumerator StartLoop(TMP_Text target, List<string> dEntry)
    {
        for(int i=0;i < dEntry.Count;i++)
        {
            yield return new WaitForSeconds(enterDelay);
            yield return AddWord(target, dEntry[i]);
            if(target == tbOne)
            {
                target = tbTwo;
            }
            else
            {
                target = tbOne;
            }
        }
    }

    IEnumerator AddWord(TMP_Text text, string words)
    {
        var word = words.Split(' ');
        for(int w=0;w < word.Length;w++)
        {
            yield return new WaitForSeconds(wordDelay);
            text.text += $"{word[w]} ";
        }
        text.text += "\r\n\r\n";
        
    }
}
