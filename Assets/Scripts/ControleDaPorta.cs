using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class ControleDaPorta : MonoBehaviour
{
    public bool[] completed;
    public bool open;
    bool AllCompleted = false;
    public GameObject porta;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        checkIfAllCompleted();
        if (open)
        {   
            Debug.Log("ta abrindo");
            porta.GetComponent<Interactable>().enabled = true;
            //colocar a funcao da porta abrindo aqui
        }
    }

    public void PuzzleComplete(int NoPuzzle)
    {
        completed[NoPuzzle] = true;
    }

    public void checkIfAllCompleted()

    {
        AllCompleted = true;
        for (int i = 0; i < completed.Length; i++)
        {
            if (completed[i] == false)
            {
                AllCompleted = false;
            }

        }
        if (AllCompleted)
        {
            open = true;
        }
    }


}
