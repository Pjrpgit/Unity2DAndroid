using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShipController : MonoBehaviour
{
    public float maxSpeed = 10f;
    private new Rigidbody2D rigidbody2D;
    private Animator animator;
    public GameObject gameController;
    public bool vivo;
    public GameObject enemyGenerator;
    public int valgema;
    GameController GC;
    public GameObject gema1;
    public GameObject gema2;
    public GameObject gema3;
    public GameObject gema4;
    public GameObject gema5;
    public GameObject gema6;
    public GameObject gema7;
    public GameObject gema8;
    public GameObject gema9;
    public GameObject gema10;
    public Joystick joystick;

    private void Awake()
    {
        vivo = true;
        animator = GetComponent<Animator>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        GC = GameController.instance;

    }
    void Start()
    {

    }

    private void FixedUpdate()
    {
        if (vivo) { 
            rigidbody2D.velocity = new Vector2(joystick.Horizontal * maxSpeed, rigidbody2D.velocity.y);
            gameController.SendMessage("IncrementaScore",0.15);
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, joystick.Vertical * maxSpeed);
            gameController.SendMessage("IncrementaScore", 0.25);
        }
        
    }
    public void UpdateState(string state = null)
    {
        if (state != null)
        {
            animator.Play(state);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            vivo = false;
            Kill();
            enemyGenerator.SendMessage("CancelGenerator");
            UpdateState("Explosion");
            gameController.GetComponent<GameController>().gameState = GameController.GameState.Ended;

        }
        if (collision.tag == "Hundido")
        {
            vivo = false;
            enemyGenerator.SendMessage("CancelGenerator");
            gameController.GetComponent<GameController>().gameState = GameController.GameState.Ended;
            gameController.GetComponent<GameController>().gameState = GameController.GameState.Hundido;

        }
        if (collision.tag == "Gema1")
        {
            valgema++;
           gameController.SendMessage("CancelGem",1); 
           gameController.GetComponent<GameController>().gema1.SetActive(false);
           gameController.SendMessage("ActualizaGema", valgema.ToString());
           gameController.SendMessage("IncrementaScore", 1000);
        }
        if (collision.tag == "Gema2")
        {
            valgema++;
            gameController.SendMessage("CancelGem", 2);
            gameController.GetComponent<GameController>().gema2.SetActive(false);
            gameController.SendMessage("ActualizaGema", valgema.ToString());
            gameController.SendMessage("IncrementaScore", 2000);
        }
        if (collision.tag == "Gema3")
        {
            valgema++;
            gameController.SendMessage("CancelGem", 3);
            gameController.GetComponent<GameController>().gema3.SetActive(false);
            gameController.SendMessage("ActualizaGema", valgema.ToString());
            gameController.SendMessage("IncrementaScore", 1000);
        }
        if (collision.tag == "Gema4")
        {
            valgema++;
            gameController.SendMessage("CancelGem", 4);
            gameController.GetComponent<GameController>().gema4.SetActive(false);
            gameController.SendMessage("ActualizaGema", valgema.ToString());
            gameController.SendMessage("IncrementaScore", 2000);
        }
        if (collision.tag == "Gema5")
        {
            valgema++;
           gameController.SendMessage("CancelGem", 5);
           gameController.GetComponent<GameController>().gema5.SetActive(false);
           gameController.SendMessage("ActualizaGema", valgema.ToString());
            gameController.SendMessage("IncrementaScore", 1000);
        }
        if (collision.tag == "Gema6")
        {
            valgema++;
            gameController.SendMessage("CancelGem", 6);
            gameController.GetComponent<GameController>().gema6.SetActive(false);
            gameController.SendMessage("ActualizaGema", valgema.ToString());
            gameController.SendMessage("IncrementaScore", 2000);
        }
        if (collision.tag == "Gema7")
        {
            valgema++;
            gameController.SendMessage("CancelGem", 7);
            gameController.GetComponent<GameController>().gema7.SetActive(false);
            gameController.SendMessage("ActualizaGema", valgema.ToString());
            gameController.SendMessage("IncrementaScore", 5000);
        }
        if (collision.tag == "Gema8")
        {
            valgema++;
            gameController.SendMessage("CancelGem", 8);
            gameController.GetComponent<GameController>().gema8.SetActive(false);
            gameController.SendMessage("ActualizaGema", valgema.ToString());
            gameController.SendMessage("IncrementaScore", 2000);
        }
        if (collision.tag == "Gema9")
        {
            valgema++;
            gameController.SendMessage("CancelGem", 9);
            gameController.GetComponent<GameController>().gema9.SetActive(false);
            gameController.SendMessage("ActualizaGema", valgema.ToString());
            gameController.SendMessage("IncrementaScore", 1000);
        }
        if (collision.tag == "Gema10")
        {
            valgema++;
            gameController.SendMessage("CancelGem", 10);
            gameController.GetComponent<GameController>().gema10.SetActive(false);
            gameController.SendMessage("ActualizaGema", valgema.ToString());
            gameController.SendMessage("IncrementaScore", 2000);
        }



    }
    public void Revive()
    {
        vivo = true;
    }
    private void Kill()
    {
        rigidbody2D.gravityScale = 0.4f;
    }
    public int DimeGemas()
    {
        return valgema;
    }
}
