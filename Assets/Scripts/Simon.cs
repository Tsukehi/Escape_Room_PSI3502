using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Simon : MonoBehaviour
{
    public MeshRenderer[] buttons;

    public GameObject objetoTodo;
    public bool GameStart;
    public GameObject[] ButtonObject;
    public int[] lightOrder;
    public MeshRenderer wonLight;
    int level;
    int buttonsClicked = 0;
    int colorOrderRunCount = 0;
    bool passed = false;
    bool won = false;
    public Material newMaterial;
    public Material defaultMaterial;
    public Material green;
    public Material red;
    public float lightspeed;
    public UnityEvent completo;


    void Start()
    {
        DisableInteractableButtons();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(GameStart);

        
        if (GameStart)
        {
            //Debug.Log("ta rodando");

            level = 0;
            buttonsClicked = 0;
            colorOrderRunCount = -1;
            won = false;
            for(int i = 0; i < lightOrder.Length; i++)
            {
                lightOrder[i] = (Random.Range(0,15));
            }
            EnableInteractableButtons();
            level = 1;
            StartCoroutine(ColorOrder());
            GameStart = false;

        
        }
    }

    public void ButtonClickOrder(int button)
    {
        
        buttonsClicked++;
        if (button == lightOrder[buttonsClicked-1])
        {
            //Debug.Log("pass");
            passed = true;
            EnableInteractableButtons();
        }
        else
        {
            //Debug.Log("failed");
            won = false;
            passed = false;
            StartCoroutine(colorBlink(red));
            //DisableInteractableButtons();
        }
        if (buttonsClicked == level && passed == true && buttonsClicked != 4)
        {
            level++;
            passed = false;
            StartCoroutine(ColorOrder());

        }
        if (buttonsClicked == level && passed == true && buttonsClicked == 4)
        {
            
            won = true;
            StartCoroutine(colorBlink(green));
            wonLight.material = green;
            DisableInteractableButtons();
        }
    }


    IEnumerator ColorOrder()

    {
        buttonsClicked = 0;
        colorOrderRunCount++;
        for (int i = 0; i <= colorOrderRunCount; i++)
        {
            if (level >= colorOrderRunCount)
            {
                buttons[lightOrder[i]].material = defaultMaterial;
                yield return new WaitForSeconds(lightspeed);
                buttons[lightOrder[i]].material = newMaterial;
                yield return new WaitForSeconds(lightspeed);
                buttons[lightOrder[i]].material = defaultMaterial;
                
            }
        }

    }
    IEnumerator colorBlink(Material colorToBlink)
    {
        DisableInteractableButtons();
        for (int j = 0; j <3; j ++)
        {
            Debug.Log("I run this many times" + j);
            for (int i = 0 ; i < buttons.Length; i++)
            {
                buttons[i].material = colorToBlink;
            }
            
            yield return new WaitForSeconds(.5f);

            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].material = defaultMaterial;
            }

            yield return new WaitForSeconds(.5f);
        }
        if(won == true)
        {
            Debug.Log("put won stuff here");
            completo.Invoke();
            GameStart = false;
        }
        //EnableInteractableButtons();
        GameStart = false;


    }

    public void toggleGameStart()
    {
        if (GameStart)
        {
            GameStart = false;
        }
        else
        {
            GameStart = true;
        }
    }

    void DisableInteractableButtons()
    {
       Debug.Log("desativando botoes");
       for (int i = 0; i < ButtonObject.Length; i++)
        {
            ButtonObject[i].GetComponent<BoxCollider>().enabled = false;
        }
    }

    void EnableInteractableButtons()
    {
        Debug.Log("ativando botoes");
        for (int i = 0; i < ButtonObject.Length; i++)
        {
            ButtonObject[i].GetComponent<BoxCollider>().enabled = true;
        }
    }
}