using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
using UnityEngine.Events;

public class piano : MonoBehaviour {

    private int[][] level_seq = {
        new int[] { 17, 1, 22, 47, 44 },
        new int[] { 20, 43, 6, 12, 41 },
        new int[] { 32, 20, 2, 27, 6  }
    };

    private int position;
    private int level;
    private bool done = false;
    public UnityEvent complete;

    public Animator anim;
    public Material verde;
    public Material vermelho;
    public Material branco;
    public MeshRenderer cover;
    public MeshRenderer botao;
    public MeshRenderer[] niveis;
    public GameObject[] botoes;

    void Start() {
        for (int i = 0; i < niveis.Length; i++) 
            niveis[i].material = branco;
        botao.material = vermelho;
    }

    void Update() {
        for (int i = 0; i < 52; i++) {
            HoverButton hb = botoes[i].GetComponent<HoverButton>();
            HandEvent he = hb.onButtonIsPressed;
            if (hb.buttonDown) {
                tocaTecla(i);
                hb.buttonDown = false;
            }
        }
    }

    public void trocaPiano() {
        botao.material = verde;
        anim.SetInteger("open", 1);
        level = 1;
        position = 0;
        startLevel(level);
        botao.GetComponent<BoxCollider>().enabled = false;
    }

    public void startLevel(int level) {
        niveis[level - 1].material = vermelho;
        int a = 2;
        
        //limpa os botoes
        for (int i = 0; i < botoes.Length; i++) {
            MeshRenderer hb = botoes[i].GetComponent<MeshRenderer>();
            hb.material = branco;
        }

        //trava os botoes
        for (int i = 0; i < botoes.Length; i++) {
            BoxCollider bc = botoes[i].GetComponent<BoxCollider>();
            bc.enabled = false;
        }
        //colore os botoes
        for (int i = 0; i < level_seq[level-1].Length; i++) {
            StartCoroutine(ExecuteAfterTime(a, level_seq[level - 1][i], vermelho));
            a++;
        }
        a++;
        StartCoroutine(aceitarTentativa(a));
    }

    IEnumerator aceitarTentativa(int a) {
        yield return new WaitForSeconds(a);
        for (int i = 0; i < level_seq[level - 1].Length; i ++) {
            botoes[level_seq[level - 1][i]].GetComponent<BoxCollider>().enabled = true;
        }
    }

    public void tocaTecla(int i) {
        if (done) 
            return;
        if(level_seq[level-1][position] == i) {
            position++;
            Debug.Log("Acertou a tercla");
            botoes[i].GetComponent<MeshRenderer>().material = verde;
            BoxCollider bc = botoes[i].GetComponent<BoxCollider>();
            bc.enabled = false;
        } else {
            position = 0;
            startLevel(level);
        }
        if (position >= level_seq[level - 1].Length) {
            position = 0;
            niveis[level - 1].material = verde;
            level++;
            if (level > level_seq.Length) {
                Debug.Log("Fim de JOGO!");
                Debug.Log("Fim de JOGO!");
                Debug.Log("Fim de JOGO!");
                Debug.Log("Fim de JOGO!");
                Debug.Log("Fim de JOGO!");
                Debug.Log("Fim de JOGO!");
                Debug.Log("Fim de JOGO!");
                Debug.Log("Fim de JOGO!");
                anim.SetInteger("open", 2); //fecha o piano
                done = true;

                complete.Invoke();
                return;
            }
            startLevel(level);
        }
    }
    IEnumerator ExecuteAfterTime(int time, int teclas, Material mat) {
        yield return new WaitForSeconds(time);
        MeshRenderer m = botoes[teclas].GetComponent<MeshRenderer>();
        m.material = mat;
    }

}
