using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
using UnityEngine.Events;
using UnityEngine.Audio;

public class SamplerController : MonoBehaviour
{
    /*
    A música "Numb" foi lançada em 8/9/03 e é o maior sucesso do Linkin Park.
    Por isso foram escolhidas as posições 0,3,8,9.

    ORDEM DOS INSTRUMENTOS:
    0 -> Numb Bass
    1 -> Castle of Glass Bass
    2 -> In the End Bass
    3 -> Numb Drums
    4 -> In the End Drums
    5 -> Castle of Glass Drums
    6 -> In the End Others
    7 -> Castle of Glass Others
    8 -> Numb Others
    9 -> Numb Vocal
    10 -> Castle of Glass Vocal
    11 -> In the End Vocal
    */

    public Material correctColor; // cor liberado ao incluir puzzle
    public Material incorrectColor; // cor enquanto puzzle nao for concluido
    public MeshRenderer statusbox; // bota que indica status do puzzle

    // Cria booleanas para verificar posicao do botao
    private bool box053ok = false;
    private bool box054ok = false;
    private bool box055ok = false;
    private bool box056ok = false;
    private bool box057ok = false;
    private bool box058ok = false;
    private bool box059ok = false;
    private bool box060ok = false;
    private bool box061ok = false;
    private bool box062ok = false;
    private bool box063ok = false;
    private bool box064ok = false;

    // booleana para executar musica no inicio do puzzle
    private bool result = false;

    // Coleta GameObjects do Unity
    [SerializeField]
    GameObject Box053;
    [SerializeField]
    GameObject Box054;
    [SerializeField]
    GameObject Box055;
    [SerializeField]
    GameObject Box056;
    [SerializeField]
    GameObject Box057;
    [SerializeField]
    GameObject Box058;
    [SerializeField]
    GameObject Box059;
    [SerializeField]
    GameObject Box060;
    [SerializeField]
    GameObject Box061;
    [SerializeField]
    GameObject Box062;
    [SerializeField]
    GameObject Box063;
    [SerializeField]
    GameObject Box064;
    [SerializeField]
    GameObject Box066;

    // Cria variaveis do LinearDrive dos botoes
    LinearMapping box053pos;
    LinearMapping box054pos;
    LinearMapping box055pos;
    LinearMapping box056pos;
    LinearMapping box057pos;
    LinearMapping box058pos;
    LinearMapping box059pos;
    LinearMapping box060pos;
    LinearMapping box061pos;
    LinearMapping box062pos;
    LinearMapping box063pos;
    LinearMapping box064pos;
    LinearMapping box066pos;

    // Cria variaveis das musicas
    AudioSource song053;
    AudioSource song054;
    AudioSource song055;
    AudioSource song056;
    AudioSource song057;
    AudioSource song058;
    AudioSource song059;
    AudioSource song060;
    AudioSource song061;
    AudioSource song062;
    AudioSource song063;
    AudioSource song064;

    public UnityEvent complete;

    // Start is called before the first frame update
    void Start()
    {
        statusbox.material = incorrectColor; // inicialmente o puzzle nao esta concluido

        // inicializa Linear Mapping dos botoes
        box053pos = Box053.GetComponent<LinearMapping>();
        box054pos = Box054.GetComponent<LinearMapping>();
        box055pos = Box055.GetComponent<LinearMapping>();
        box056pos = Box056.GetComponent<LinearMapping>();
        box057pos = Box057.GetComponent<LinearMapping>();
        box058pos = Box058.GetComponent<LinearMapping>();
        box059pos = Box059.GetComponent<LinearMapping>();
        box060pos = Box060.GetComponent<LinearMapping>();
        box061pos = Box061.GetComponent<LinearMapping>();
        box062pos = Box062.GetComponent<LinearMapping>();
        box063pos = Box063.GetComponent<LinearMapping>();
        box064pos = Box064.GetComponent<LinearMapping>();
        box066pos = Box066.GetComponent<LinearMapping>();

        // coleta audio dos botoes
        song053 = Box053.GetComponent<AudioSource>();
        song054 = Box054.GetComponent<AudioSource>();
        song055 = Box055.GetComponent<AudioSource>();
        song056 = Box056.GetComponent<AudioSource>();
        song057 = Box057.GetComponent<AudioSource>();
        song058 = Box058.GetComponent<AudioSource>();
        song059 = Box059.GetComponent<AudioSource>();
        song060 = Box060.GetComponent<AudioSource>();
        song061 = Box061.GetComponent<AudioSource>();
        song062 = Box062.GetComponent<AudioSource>();
        song063 = Box063.GetComponent<AudioSource>();
        song064 = Box064.GetComponent<AudioSource>();

        // inicializa audios
        song053.Play();
        song054.Play();
        song055.Play();
        song056.Play();
        song057.Play();
        song058.Play();
        song059.Play();
        song060.Play();
        song061.Play();
        song062.Play();
        song063.Play();
        song064.Play();
    }

    // Update is called once per frame
    void Update()
    {       
        StopSong(song053, box053pos);
        StopSong(song054, box054pos);
        StopSong(song055, box055pos);
        StopSong(song056, box056pos);
        StopSong(song057, box057pos);
        StopSong(song058, box058pos);
        StopSong(song059, box059pos);
        StopSong(song060, box060pos);
        StopSong(song061, box061pos);
        StopSong(song062, box062pos);
        StopSong(song063, box063pos);
        StopSong(song064, box064pos);

        ResetPositionButtons();
        ResetLinearMappingButtons();
        ResetAudio();

        RunAudio(song053, box053pos);
        RunAudio(song054, box054pos);
        RunAudio(song055, box055pos);
        RunAudio(song056, box056pos);
        RunAudio(song057, box057pos);
        RunAudio(song058, box058pos);
        RunAudio(song059, box059pos);
        RunAudio(song060, box060pos);
        RunAudio(song061, box061pos);
        RunAudio(song062, box062pos);
        RunAudio(song063, box063pos);
        RunAudio(song064, box064pos);

        if (checkPosition()){
            statusbox.material = correctColor;
            // Para de executar Audio


            // Chama funcao da porta
            complete.Invoke();
        } else{
            statusbox.material = incorrectColor;
        }
    }

    void StopSong(AudioSource song, LinearMapping box)
    {
        if (box.value >= 0.9f){song.Stop();}
    }

    void ResetLinearMappingButtons()
    {
        if (box066pos.value == 1.0f)
        {
            box053pos.value = 0.0f;
            box054pos.value = 0.0f;
            box055pos.value = 0.0f;
            box056pos.value = 0.0f;
            box057pos.value = 0.0f;
            box058pos.value = 0.0f;
            box059pos.value = 0.0f;
            box060pos.value = 0.0f;
            box061pos.value = 0.0f;
            box062pos.value = 0.0f;
            box063pos.value = 0.0f;
            box064pos.value = 0.0f;
        }
    }

    void ResetPositionButtons()
    {
        if (box066pos.value == 1.0f)
        {
            Box053.transform.position = new Vector3(0.0f,0.0f,0.0f);
            Box054.transform.position = new Vector3(0.0f,0.0f,0.0f);
            Box055.transform.position = new Vector3(0.0f,0.0f,0.0f);
            Box056.transform.position = new Vector3(0.0f,0.0f,0.0f);
            Box057.transform.position = new Vector3(0.0f,0.0f,0.0f);
            Box058.transform.position = new Vector3(0.0f,0.0f,0.0f);
            Box059.transform.position = new Vector3(0.0f,0.0f,0.0f);
            Box060.transform.position = new Vector3(0.0f,0.0f,0.0f);
            Box061.transform.position = new Vector3(0.0f,0.0f,0.0f);
            Box062.transform.position = new Vector3(0.0f,0.0f,0.0f);
            Box063.transform.position = new Vector3(0.0f,0.0f,0.0f);
            Box064.transform.position = new Vector3(0.0f,0.0f,0.0f);
        }
    }

    void ResetAudio()
    {
        if (box066pos.value == 1.0f)
        {
            song053.Stop();
            song054.Stop();
            song055.Stop();
            song056.Stop();
            song057.Stop();
            song058.Stop();
            song059.Stop();
            song060.Stop();
            song061.Stop();
            song062.Stop();
            song063.Stop();
            song064.Stop();
        }
    }

    void RunAudio(AudioSource song, LinearMapping box)
    {
        if (box.value == 0.0f && song.isPlaying == false){song.Play();}
    }

    bool checkPosition()
    {
        if (box053pos.value == 0.0f){box053ok = true;}
        if (box054pos.value >= 0.9f){box054ok = true;}
        if (box055pos.value >= 0.9f){box055ok = true;}
        if (box056pos.value == 0.0f){box056ok = true;}
        if (box057pos.value >= 0.9f){box057ok = true;}
        if (box058pos.value >= 0.9f){box058ok = true;}
        if (box059pos.value >= 0.9f){box059ok = true;}
        if (box060pos.value >= 0.9f){box060ok = true;}
        if (box061pos.value == 0.0f){box061ok = true;}
        if (box062pos.value == 0.0f){box062ok = true;}
        if (box063pos.value >= 0.9f){box063ok = true;}
        if (box064pos.value >= 0.9f){box064ok = true;}
        if (box053ok && box054ok && box055ok &&
            box056ok && box057ok && box058ok &&
            box059ok && box060ok && box061ok &&
            box062ok && box063ok && box064ok) { 
            result = true;
        }
        box053ok = false;
        box054ok = false;
        box055ok = false;
        box056ok = false;
        box057ok = false;
        box058ok = false;
        box059ok = false;
        box060ok = false;
        box061ok = false;
        box062ok = false;
        box063ok = false;
        box064ok = false;
        
        return result;
    }
}
