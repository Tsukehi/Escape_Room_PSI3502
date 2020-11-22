using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;


public class botao : MonoBehaviour
{
    public UnityEvent evento;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown()
    {
        Debug.Log("apertou");
    }

    void OnMouseUp()
    {
        Debug.Log("soltou");
        evento.Invoke();

    }
    

}
