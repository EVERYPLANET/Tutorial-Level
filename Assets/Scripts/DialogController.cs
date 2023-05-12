using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogController : MonoBehaviour
{
    public TextMeshProUGUI textBox;
    public string[] startLines;
    public string[] interactionLines;
    public string[] endLines;
    public float textSpeed;

    private string[] currentLines;

    private int index;

    private PlayerController playerC;

    public bool end = false;
    
    // Start is called before the first frame update
    void Start()
    {
        playerC = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        textBox.text = string.Empty;
        currentLines = startLines;
        StartDialog();

        playerC.controlsActive = false;
        print("ok");
    }

    private void OnEnable()
    {
        textBox.text = string.Empty;
        selectLines();
        StartDialog();
        
        playerC.controlsActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (textBox.text == currentLines[index])
            {
                nextLine();
            }
            else
            {
                StopAllCoroutines();
                textBox.text = currentLines[index];
            }
        }
    }

    void StartDialog()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach (char c in currentLines[index].ToCharArray())
        {
            textBox.text += c;
            playerC.AM.playClip("voice");
            yield return new WaitForSeconds(textSpeed);

        }
    }

    void nextLine()
    {
        if (index < currentLines.Length - 1)
        {
            index++;
            textBox.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            playerC.controlsActive = true;
            this.gameObject.SetActive(false);

            if (playerC.CheckInventory("Key") || end)
            {
                SceneLoader scene = GameObject.FindGameObjectWithTag("Scene").GetComponent<SceneLoader>();
                scene.LevelFade();
            }
        }
    }

    private void selectLines()
    {
        print(playerC.CheckInventory("Key"));
        if (playerC.CheckInventory("Key") || end)
        {
            currentLines = endLines;
        }
        else
        {
            currentLines = interactionLines;
        }
    }

    public void SetEnd(bool set)
    {
        end = set;
    }
    
}
