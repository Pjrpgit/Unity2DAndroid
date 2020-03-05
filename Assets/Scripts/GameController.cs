using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class GameController : MonoBehaviour
{

    public string filePath;
    public string jsonUsuarios;
    public Usuarios users;
    public Usuario usuario;
    public RawImage background;
    public RawImage clouds;
    public enum GameState { Idle, Playing,Ended,Hundido,Finished};
    public GameState gameState = GameState.Idle;
    public float parallaxSpeed = 0.0058f;
    public bool pulsado = false;
    public GameObject uiIdle;
    public GameObject uiPlaying;
    public GameObject uiGameOver;
    public GameObject uiFinish;
    public GameObject ship;
    public GameObject enemyGenerator;
    public InputField inputField;
    public Scene intro;
    public Scene principal;
    public Button btn;
    [Range (0f,120f)]
    public float timePass;
    public GameObject timer;
    public GameObject record;
    public Text tiempo;
    public Text gemas;
    public Text scor;
    public Text nombretext;
    public Text scr;
    public static GameController instance;
    public GameObject gema1;
    public bool sinrecog1 = true;
    public GameObject gema2;
    public bool sinrecog2 = true;
    public GameObject gema3;
    public bool sinrecog3 = true;
    public GameObject gema4;
    public bool sinrecog4 = true;
    public GameObject gema5;
    public bool sinrecog5 = true;
    public GameObject gema6;
    public bool sinrecog6 = true;
    public GameObject gema7;
    public bool sinrecog7 = true;
    public GameObject gema8;
    public bool sinrecog8 = true;
    public GameObject gema9;
    public bool sinrecog9 = true;
    public GameObject gema10;
    public bool sinrecog10 = true;
    public float score;
    public float descuento;
    public bool segund = false;




    private void Awake()
    {
        btn.onClick.AddListener(TaskOnClick);
        filePath = Application.dataPath + "/Scripts/Usuarios.json";
        ActualizaJson();
        instance = this;
        score = 0;
        descuento = 400;
        if (SceneManager.GetActiveScene().name.Equals("Principal"))
        {
            inputField.text = PlayerPrefs.GetString("playername");
            PlayerPrefs.DeleteAll();
            inputField.characterLimit = 8;
        }
    }

    void Start()
    {
        ship.SetActive(false);

    }
    void StartPlaying()
    {
        gameState = GameState.Playing;
        if (SceneManager.GetActiveScene().name.Equals("Principal"))
        {
            StoreName();
        }else
        {
            ActualizaJson();
            if (PlayerPrefs.GetInt("invitado") == 0)
            {
                usuario = new Usuario();
                usuario.nombre = PlayerPrefs.GetString("playername");
                if(users.usuarios.Contains(users.usuarios.Find(p => p.nombre == usuario.nombre)))
                {
                    usuario.score = users.usuarios.Find(p => p.nombre == usuario.nombre).score;
                }
                else
                {
                    usuario.score = 0;
                }
                nombretext.text = "PILOT: " + usuario.nombre;
                scr.text = "RECORD: " + usuario.score;
            }
            else
            {
                scr.text = "PRACTICE MODE";
                nombretext.text = "PILOT: " + "GUEST";
            }
        }
        uiIdle.SetActive(false);
        uiGameOver.SetActive(false);
        ship.SetActive(true);
        enemyGenerator.SendMessage("StartGenerator");
        uiPlaying.SetActive(true);
        timePass -= Time.deltaTime;
        tiempo.text = timePass.ToString("000");
    }

    void Update()
    {
        Parallax(1);
        if ((gameState == GameState.Idle && pulsado) || gameState == GameState.Idle && (SceneManager.GetActiveScene().name.Equals("Second")&& Input.GetMouseButtonDown(0)))
        {
            StartPlaying();
        } else if (gameState == GameState.Playing)
        {
            timePass -= Time.deltaTime;
            score += Time.deltaTime/0b10;
            if (timePass > 2) {
                tiempo.text = timePass.ToString("000");
                Parallax(3);
                if (timePass < 70 && sinrecog1)
                {
                    if(gema1!=null)gema1.SetActive(true);
                }
                if (timePass < 60 && (sinrecog2 && sinrecog6))
                {
                    gema2.SetActive(true);
                    if(gema6!=null)gema6.SetActive(true);
                }
                if (timePass < 40 && sinrecog3 && sinrecog7)
                {
                    gema3.SetActive(true);
                    if (gema7 != null) gema7.SetActive(true);
                }
                if (timePass < 30 && sinrecog4 && sinrecog8)
                {
                    gema4.SetActive(true);
                    if (gema8 != null) gema8.SetActive(true);
                }
                if (timePass < 20 && sinrecog5 && sinrecog9 && sinrecog10)
                {
                    gema5.SetActive(true);
                    if (gema9 != null) gema9.SetActive(true);
                    if (gema10 != null) gema10.SetActive(true);
                }
            }
            else if (timePass < 2 && timePass > 0)
            {
                tiempo.text = timePass.ToString("000");
                enemyGenerator.SendMessage("CancelGenerator");

            }
            else
            {
                tiempo.text = timePass.ToString("000");
                gameState = GameState.Finished;
                ship.SetActive(false);
                uiFinish.SetActive(true);

            }
        } else if (gameState == GameState.Ended)
        {
            uiPlaying.SetActive(false);
        }
        else if (gameState == GameState.Hundido)
        {

                uiPlaying.SetActive(false);
                uiGameOver.SetActive(true);
                if (Input.GetKeyDown("space") || Input.GetMouseButtonDown(0))
                {
                    RestarGame();
                }
       
        }
        else if (gameState == GameState.Finished)
        {
            if (SceneManager.GetActiveScene().name.Equals("Principal")){
                score = score-descuento;
                if (score < 0) score = 100;
                descuento = 0;
            }
            if (usuario != null)
            {
                ActualizaJson();
                if(score > usuario.score)
                {
                    record.SetActive(true);
                    scor.text = score.ToString();
                    usuario.score = score;
                    ActualizaUser(usuario);
                }
                else
                {
                    scor.text = score.ToString();
                }

            }
            else
            {
                scor.text = "PRACTICE MODE";
            }
            if (Input.GetMouseButtonDown(0))
            {
                if (!segund) { 
                    segund = true;
                    SceneManager.LoadScene("Second");
                    gameState = GameState.Playing;
                    enemyGenerator.SendMessage("ResetTimmer");
                }
                else
                {
                    RestarGame();
                }
            }
        }
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }

    }
    void Parallax(int numero)
    {
        float finalSpeed = (parallaxSpeed * Time.deltaTime)*numero;
        background.uvRect = new Rect(background.uvRect.x + finalSpeed, 0f, 1f, 1f);
        clouds.uvRect = new Rect(clouds.uvRect.x + finalSpeed*12, 0f, 1f, 1f);
    }
    public void RestarGame()
    {
        SceneManager.LoadScene("Principal");
        gameState = GameState.Playing;
    }
    public void ActualizaGema(string tgema=null)
    {
        if (tgema != null) { 
        gemas.text = tgema;
        }
    }
    public void CancelGem(int numero)
    {
        switch (numero) {
            case 1:
                sinrecog1 = false;
                break;
            case 2:
                sinrecog2 = false;
                break;
            case 3:
                sinrecog3 = false;
                break;
            case 4:
                sinrecog4 = false;
                break;
            case 5:
                sinrecog5 = false;
                break;
            case 6:
                sinrecog6 = false;
                break;
            case 7:
                sinrecog7 = false;
                break;
            case 8:
                sinrecog8 = false;
                break;
            case 9:
                sinrecog9 = false;
                break;
            case 10:
                sinrecog10 = false;
                break;
        }
        
    }
    public void IncrementaScore(int numero)
    {
        score = score + numero;
    }
    public void StoreName()
    {
        string nombre_input = inputField.text.ToUpper();
        if(nombre_input!="")
        {
            try
            {
                usuario= users.usuarios.Find(p => p.nombre == nombre_input.TrimStart().TrimEnd());
                nombretext.text = "PILOT: " + usuario.nombre;
                scr.text = "RECORD: "+ usuario.score;
                PlayerPrefs.SetInt("invitado", 0);
                PlayerPrefs.SetString("playername", usuario.nombre);
            }
            catch(System.NullReferenceException)
            {
                usuario = new Usuario();
                usuario.nombre=nombre_input.TrimStart().TrimEnd();
                usuario.score=0;
                nombretext.text = "PILOT: " + usuario.nombre;
                scr.text = "RECORD: " + usuario.score;
                AgregaJson(usuario);
                PlayerPrefs.SetInt("invitado", 0);
                PlayerPrefs.SetString("playername", usuario.nombre);
            }
            
        }
        else
        {
            usuario = null;
            scr.text = "PRACTICE MODE";
            nombretext.text = "PILOT: " + "GUEST";
            PlayerPrefs.SetInt("invitado", 1);
        }


    }
    public void ActualizaJson()
    {
        jsonUsuarios = File.ReadAllText(filePath);
        users = JsonUtility.FromJson<Usuarios>(jsonUsuarios);
    }
    public void AgregaJson(Usuario usuario)
    {
        users.usuarios.Add(usuario);
        jsonUsuarios = JsonUtility.ToJson(users);
        File.WriteAllText(filePath, jsonUsuarios);
    }
    public void ActualizaUser(Usuario usuario)
    {
        users.usuarios.Find(p => p.nombre == usuario.nombre).score=usuario.score;
        jsonUsuarios = JsonUtility.ToJson(users);
        File.WriteAllText(filePath,jsonUsuarios);
    }
    public void TaskOnClick()
    {
        pulsado = true;
    }

}
[System.Serializable]
public class Usuario
{
    public string nombre;
    public float score;
}
[System.Serializable]
public class Usuarios
{
    public int total;
    public List<Usuario> usuarios;
    
}
