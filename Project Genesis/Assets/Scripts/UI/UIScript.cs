using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UIScript : MonoBehaviour
{
    public GameObject Brutus;
    private AudioController audioContr;
    public FallToEndGame endGameCondition;
    public CinemachineVirtualCamera cameraFollow;
    public CinemachineVirtualCamera cameraPause;
    public GameObject Pozo;
    public GameObject leds;
    public Animator endDoor;
    public GameObject player;
    private PlayerHealth playerHealth;
    public SpriteRenderer playerHeadSR;
    public SpriteRenderer playerBody;

    private Sprite defaultHeadS;

    public Sprite pauseSprite;
    public Sprite backSprite;
    public Sprite defaultPose;

    private bool isPaused = false;
    //listado de sprites
    int i = 0;
    private List<Sprite> listSprite;

    private MainMenu menu;
    public GameObject GameOver;

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
        defaultHeadS = playerHeadSR.sprite;
        GameOver.gameObject.SetActive(false);
        cameraFollow.gameObject.SetActive(true);
        //cameraPause.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        playerHPController();
        //cam.GetComponent<CameraFollow>().inPause = isPaused;
        //si se presiono esc se pausa el jeugo
        bool pause = Input.GetButtonDown("Cancel");
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
            playerHeadSR.sprite = listSprite[i];

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
                    playerHeadSR.sprite = defaultHeadS;
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
        if (endGameCondition.finalGame)
        {
            StartCoroutine(fallToEnd());
        }
        else
        {
            if ((playerHealth.hp <= 0 && player.activeSelf))
            {
                StartCoroutine(Death());
            }
            switch (playerHealth.hp)
            {
                case 3:
                    playerHeadSR.color = new Color(0, 255, 0);
                    break;
                case 2:
                    playerHeadSR.color = new Color(255, 240, 0);
                    break;
                case 1:
                    playerHeadSR.color = new Color(255, 0, 0);
                    break;
            }
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
        yield return null;
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
        playerHeadSR.sprite = pauseSprite;
        playerBody.sprite = defaultPose;
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        //cameraPause.gameObject.SetActive(true);
        cameraFollow.gameObject.SetActive(false);
        Time.timeScale = 0;

    }

    void ResumeGame()
    {
        player.GetComponent<Animator>().enabled = true;
        player.GetComponent<PlayerMovement>().enabled = true;
        player.GetComponent<Jump>().enabled = true;
        playerHeadSR.sprite = defaultHeadS;
        cameraFollow.gameObject.SetActive(true);
        //cameraPause.gameObject.SetActive(false);
        Time.timeScale = 1;

    }

    public void endGameUI()
    {
        StartCoroutine(disablePozoGround());
    }
    private IEnumerator disablePozoGround()
    {
        Animator animPozo = Pozo.GetComponent<Animator>();
        yield return new WaitForSeconds(5);
        audioContr.playfinalGameClip();
        yield return new WaitForSeconds(8);
        cameraFollow.Follow = null;
        animPozo.enabled = true;
        leds.SetActive(false);
        Brutus.SetActive(false);
        yield return new WaitForSeconds(5);
        cameraFollow.Follow = player.transform;
        yield return new WaitForSeconds(2);
        endDoor.enabled = true;
        audioContr.playOpenTheDoor();
        //AutoMovementPlayer
        player.GetComponent<PlayerAutoMovementToEndGame>().enabled = true;
        yield return new WaitForSeconds(20);
        menu.GoToLevelCredits();
    }





}
