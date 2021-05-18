using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Nivel1Controller : MonoBehaviour
{
    private AudioController audioContr;
    public CinemachineVirtualCamera cameraFollow;
    public CinemachineVirtualCamera cameraPause;


    public GameObject player;
    private PlayerHealth playerHealth;
    public SpriteRenderer playerLeds;
    public SpriteRenderer playerBody;

    private Sprite defaultHeadS;

    public Sprite pauseSprite;
    public Sprite backSprite;
    public Sprite defaultPose;

    //listado de sprites
    int i = 0;
    private List<Sprite> listSprite;

    private MainMenu menu;
    public GameObject GameOver;
    private bool isPaused = false;

    private enum options { paused, back };


    private void Awake()
    {
        menu = GetComponent<MainMenu>();
        playerHealth = player.GetComponent<PlayerHealth>();
        audioContr = player.GetComponent<AudioController>();

    }
    void Start()
    {
        //se inicializa el listado de sprites
        listSprite = new List<Sprite>();
        listSprite.Add(pauseSprite);
        listSprite.Add(backSprite);
        defaultHeadS = playerLeds.sprite;
        GameOver.gameObject.SetActive(false);
        cameraFollow.gameObject.SetActive(true);
        cameraPause.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        playerHPController();
        //si se presiono esc se pausa el jeugo
        bool pause = Input.GetButtonDown("Pause");
        if (pause && !isPaused)
        {
            //Se pausa el juego
            isPaused = true;
            pauseGame();

        }
        else if (pause && isPaused)
        {
            isPaused = false;
            ResumeGame();
        }
        //si cambia de opcion en el menu
        bool changeOption = Input.GetButtonDown("Horizontal");
        if (changeOption && isPaused)
        {
            i++;
            if (i == listSprite.Count)
                i = 0;
            playerLeds.sprite = listSprite[i];

        }
        //si esta pausado y presiono enter o espacio
        bool enter = Input.GetButtonDown("Submit");
        if (isPaused && enter)
        {
            //switch para controlar opciones futuras
            switch (i)
            {
                case (int)options.paused:
                    isPaused = false;
                    playerLeds.sprite = defaultHeadS;
                    ResumeGame();
                    break;
                case (int)options.back:
                    isPaused = false;
                    menu.GoToMainMenu();
                    break;
            }
        }

    }


    void playerHPController()
    {

        if ((playerHealth.hp <= 0 && player.activeSelf))
        {
            StartCoroutine(Death());
        }
        switch (playerHealth.hp)
        {
            case 3:
                playerLeds.color = new Color(0, 255, 0);
                break;
            case 2:
                playerLeds.color = new Color(255, 240, 0);
                break;
            case 1:
                playerLeds.color = new Color(255, 0, 0);
                break;
        }

    }

    private IEnumerator Death()
    {
        playerHealth.hp = 0;
        audioContr.playdeathClip();
        yield return new WaitForSeconds(1.2f);
        GameOver.gameObject.SetActive(true);
    }

    private IEnumerator fallToEnd()
    {
        yield return new WaitForSeconds(9);
        playerHealth.hp = 0;
        GameOver.gameObject.SetActive(true);
    }

    private void destroyPlayer()
    {
        playerHealth.hp = 0;
    }

    void pauseGame()
    {
        player.GetComponent<Animator>().enabled = false;
        player.GetComponent<PlayerMovement>().enabled = false;
        player.GetComponent<Jump>().enabled = false;
        playerLeds.sprite = pauseSprite;
        playerBody.sprite = defaultPose;
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        cameraPause.gameObject.SetActive(true);
        cameraFollow.gameObject.SetActive(false);

    }

    void ResumeGame()
    {
        player.GetComponent<Animator>().enabled = true;
        player.GetComponent<PlayerMovement>().enabled = true;
        player.GetComponent<Jump>().enabled = true;
        playerLeds.sprite = defaultHeadS;
        cameraFollow.gameObject.SetActive(true);
        cameraPause.gameObject.SetActive(false);
    }
    //realiza zoom a la camara

    public void endGame()
    {
        StartCoroutine(WinNivel());
    }

    private IEnumerator WinNivel()
    {
        yield return new WaitForSeconds(1.2f);
        menu.GoToLevel03();
    }

}
