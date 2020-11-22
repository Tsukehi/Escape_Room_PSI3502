using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Valve.VR.InteractionSystem;

public class PuzzleControl : MonoBehaviour
{
    public Material correctColor;
    public Material incorrectColor;

    public LinearMapping cd3Position;
    public MeshRenderer cd3;

    public LinearMapping cd4Position;
    public MeshRenderer cd4;

    public LinearMapping cd5Position;
    public MeshRenderer cd5;

    public LinearMapping cd6Position;
    public MeshRenderer cd6;

    public MeshRenderer puzzleStatus;

    public UnityEvent complete;

    private bool cd3Puz;
    private bool cd4Puz;
    private bool cd5Puz;
    private bool cd6Puz;

    // Start is called before the first frame update
    void Start()
    {
        cd3.material = incorrectColor;
        cd4.material = incorrectColor;
        cd5.material = incorrectColor;
        cd6.material = incorrectColor;
    }

    // Update is called once per frame
    void Update()
    {
        checkPosition();
        checkPuzzleStatus();
    }

    void checkPosition()
    {
        float position3 = cd3Position.value * 1.0f;
        float position4 = cd4Position.value * 1.0f;
        float position5 = cd5Position.value * 1.0f;
        float position6 = cd6Position.value * 1.0f;

        //Debug.Log(position4);
        if (position3 == 0)
        {
            cd3.material = correctColor;
            cd3Puz = true;
        }
        else
        {
            cd3.material = incorrectColor;
            cd3Puz = false;
        }
        if ((position4 >= 0.31f) && (position4 <= 0.35f))
        {
            cd4.material = correctColor;
            cd4Puz = true;
        }
        else
        {
            cd4.material = incorrectColor;
            cd4Puz = false;
        }
        if ((position5 >= 0.64f) && (position5 <= 0.68f))
        {
            cd5.material = correctColor;
            cd5Puz = true;
        }
        else
        {
            cd5.material = incorrectColor;
            cd5Puz = false;
        }
        if (position6 == 1)
        {
            cd6.material = correctColor;
            cd6Puz = true;
        }
        else
        {
            cd6.material = incorrectColor;
            cd6Puz = false;
        }
    }

    void checkPuzzleStatus() {
        if (cd3Puz & cd4Puz & cd5Puz & cd6Puz) {
            puzzleStatus.material = correctColor;
            // Chama funcao da porta
            complete.Invoke();
        } else
        {
            puzzleStatus.material = incorrectColor;
        }
    }
}
