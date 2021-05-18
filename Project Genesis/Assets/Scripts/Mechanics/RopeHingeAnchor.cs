using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RopeHingeAnchor : MonoBehaviour
{
    [Header("")]
    public Camera cam;

    [Header("Rope Settings")]
    public GameObject ropeHingeAnchor;
    public int linePoints = 2;
    public LayerMask ropeLayerMask;
    public float ropeMaxCastDistance = 20f;
    public float climbSpeed = 2f;

    [Header("Crosshair Settings")]
    public Transform crosshair;
    public bool Crosshairvisible = false;
    public SpriteRenderer crosshairSprite;
    public float crosshairDistance = 2;



    public GameObject hand;
    public SpriteRenderer handToAttach;    

    public bool jumpOnRope
    {
        get
        {
            return ropeAttached;
        }
    }

    private DistanceJoint2D ropeJoint;
    private PlayerMovement playerMovement;
    private bool ropeAttached = false;
    private Vector2 playerPosition;
    private Rigidbody2D ropeHingeAnchorRb;
    private SpriteRenderer ropeHingeAnchorSprite;
    private LineRenderer ropeRenderer;
    public List<Vector2> ropePositions = new List<Vector2>();
    private bool distanceSet;
    private bool hookBusy = false;


    private bool isColliding;

    private Jump jump;

    void Awake()
    {
        ropeJoint = GetComponent<DistanceJoint2D>();
        playerMovement = GetComponent<PlayerMovement>();
        ropeRenderer = GetComponent<LineRenderer>();
        ropeJoint.enabled = false;
        playerPosition = transform.position;
        ropeHingeAnchorRb = ropeHingeAnchor.GetComponent<Rigidbody2D>();
        ropeHingeAnchorSprite = ropeHingeAnchor.GetComponent<SpriteRenderer>();
        jump = GetComponent<Jump>();
        //Desactiva la mira
        if (!Crosshairvisible)
            crosshairSprite.sprite = null;
    }

    private void Start()
    {
        hand.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //Optiene la posicion del mouse
        var worldMousePosition =
            cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f));
        var facingDirection = worldMousePosition - transform.position;
        var aimAngle = Mathf.Atan2(facingDirection.y, facingDirection.x);
        if (aimAngle < 0f)
        {
            aimAngle = Mathf.PI * 2 + aimAngle;
        }

        // Asigna la direccion de la mira
        var aimDirection = Quaternion.Euler(0, 0, aimAngle * Mathf.Rad2Deg) * Vector2.right;
       
        playerPosition = transform.position;

        if (!ropeAttached)
        {
            playerMovement.isSwinging = false;
            SetCrosshairPosition(aimAngle);
        }
        else
        {
            playerMovement.isSwinging = true;
            playerMovement.ropeHook = ropePositions.Last();
            crosshairSprite.enabled = false;


        }

        HandleInput(aimDirection);
        UpdateRopePositions();
        HandleRopeLength();

    }

    //Posisiona el sprite del Crosshair
    private void SetCrosshairPosition(float aimAngle)
    {
        if (!crosshairSprite.enabled)
        {
            crosshairSprite.enabled = true;
        }
        var worldMousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
        worldMousePosition = new Vector3(worldMousePosition.x, worldMousePosition.y, 0);
        if (Vector2.Distance(transform.position, worldMousePosition) > ropeMaxCastDistance)
        {
            float x = transform.position.x + ropeMaxCastDistance * Mathf.Cos(aimAngle);
            float y = transform.position.y + ropeMaxCastDistance * Mathf.Sin(aimAngle);
            crosshair.transform.position = new Vector3(x, y, 0);
        }
        else
        {
            crosshair.transform.position = worldMousePosition;
        }
    }

    private void HandleInput(Vector2 aimDirection)
    {
        if (Input.GetButtonDown("Fire2") && !hookBusy)
        {

            
            if (!ropeAttached)
            {
                ropeRenderer.enabled = true;
                List<Collider2D> listColliders = new List<Collider2D>();
                ContactFilter2D filter = new ContactFilter2D();
                filter.SetLayerMask(LayerMask.GetMask("Magnet"));
                int count = Physics2D.OverlapCircle(playerPosition, ropeMaxCastDistance, filter,listColliders);
                Vector3 facingDirection;
                float pivotAngle;
                //limpio la listColliders de los pivotes por debajo del personaje
                List<Collider2D> listPivots = new List<Collider2D>();
                foreach (Collider2D col in listColliders)
                {
                    facingDirection = col.gameObject.transform.position - transform.position;
                    pivotAngle = Mathf.Atan2(facingDirection.y, facingDirection.x);
                    if (pivotAngle < 0f)
                    {
                        pivotAngle = Mathf.PI * 2 + pivotAngle;
                    }
                    pivotAngle = pivotAngle * Mathf.Rad2Deg;
                    
                    if (pivotAngle < 180)
                        listPivots.Add(col);

                }

                //Si se encontro un pivote
                if (listPivots.Count > 0)
                {
                    //De los pivotes que hay a rango el que este por encima
                    //y el mas cercano
                    
                    Collider2D nearPivot= listColliders.First();


                    foreach (Collider2D col in listColliders)
                    {
                        facingDirection = col.gameObject.transform.position - transform.position;
                        pivotAngle = Mathf.Atan2(facingDirection.y, facingDirection.x);
                        if (pivotAngle < 0f)
                        {
                            pivotAngle = Mathf.PI * 2 + pivotAngle;
                        }
                        pivotAngle = pivotAngle * Mathf.Rad2Deg;
                        if (pivotAngle < 180 && Vector2.Distance(playerPosition, col.gameObject.transform.position) < Vector2.Distance(playerPosition, nearPivot.gameObject.transform.position))
                            nearPivot = col;
                    }
                    ropeAttached = true;
                    if (!ropePositions.Contains(nearPivot.gameObject.transform.position))
                    {
                        //Hace que el jugador salte un poco para distanciar de la colision del piso
                        transform.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, 2f), ForceMode2D.Impulse);

                        ropePositions.Add(nearPivot.gameObject.transform.position);
                        ropeJoint.distance = Vector2.Distance(playerPosition, nearPivot.gameObject.transform.position);
                        ropeJoint.enabled = true;
                        ropeHingeAnchorSprite.enabled = true;
                    }
                }
                else
                {
                    StartCoroutine(HookToAir());

                }
            }

        }
        
        if (Input.GetButtonUp("Fire2") || Input.GetButtonDown("Jump") && (ropeAttached))
        {
            ResetRope();
            if(Input.GetButtonDown("Jump"))
            {
                jump.jumpNow();
            }
        }

    }

  
    private void ResetRope()
    {
        ropeJoint.enabled = false;
        ropeAttached = false;
        playerMovement.isSwinging = false;
        ropeRenderer.positionCount = linePoints;
        ropePositions.Clear();
        ropeHingeAnchorSprite.enabled = false;
    }

    private void UpdateRopePositions()
    {
        if (ropeAttached || hookBusy)
        {
            ropeRenderer.positionCount = linePoints;
            ropeRenderer.positionCount = ropePositions.Count;
            for (var i = ropeRenderer.positionCount - 1; i >= 0; i--)
            {
                if (i != ropeRenderer.positionCount - 1)
                {
                    ropeRenderer.SetPosition(i, new Vector3(ropePositions[i].x, Mathf.Sin(ropePositions[i].x*5) + ropePositions[i].y, -1)); 
                    if (i == ropePositions.Count - 1)
                    {
                        var ropePosition = ropePositions.Last();
                        ropeHingeAnchorRb.transform.position = ropePosition;
                        if (!distanceSet)
                        {
                            ropeJoint.distance = Vector2.Distance(transform.position, ropePosition);
                            distanceSet = true;
                        }
                    }
                }
                else
                {
                    ropeRenderer.SetPosition(i, transform.position);
                }
            }                        

        }
    }

    //para trepar
    private void HandleRopeLength()
    {

        if (Input.GetAxis("Vertical") >= 0.2f && ropeAttached && !isColliding)
        {
            ropeJoint.distance -= Time.deltaTime * climbSpeed;
        }
        else if (Input.GetAxis("Vertical") < -0.2f && ropeAttached && ropeJoint.distance < ropeMaxCastDistance*2)
        {
            ropeJoint.distance += Time.deltaTime * climbSpeed;
        }
    }
    void OnTriggerStay2D(Collider2D colliderStay)
    {
            isColliding = true;
    }


    private void OnTriggerExit2D(Collider2D colliderOnExit)
    {

            isColliding = false;
    }


    private void activeRope()
    {
        this.enabled = true;
    }

    private void FireArm(Vector2 aimDirection)
    {
        GameObject feedBackInstance = Instantiate(hand);
        feedBackInstance.transform.position = transform.position;

        feedBackInstance.transform.up = aimDirection;


        Rigidbody2D rb = feedBackInstance.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = aimDirection * 10f;
        }
    }

    private IEnumerator HookToAir()
    {
        hookBusy = true;
        Vector3 targetPosition;
        //Opcion 1: Gancho en direccion al mouse
        //targetPosition = crosshair.position;
        //Opcion 2: Gancho en direccion horizontal
        hand.SetActive(true);
        if (!playerMovement.flip)
        {
            targetPosition = new Vector3(transform.position.x + ropeMaxCastDistance, transform.position.y, 0);
        }
        else
        {
            targetPosition = new Vector3(transform.position.x - ropeMaxCastDistance, transform.position.y, 0);

        }

        ropeRenderer.enabled = true;
        yield return null;
        float t = 0;
        while (t < 0.2f)
        {
            t += Time.deltaTime;
            hand.transform.position = Vector3.Lerp(transform.position, targetPosition, t / .2f);
            ropePositions.Add(hand.transform.position);

            var distance = new Vector2(transform.position.x, transform.position.y) - new Vector2(hand.transform.position.x, hand.transform.position.y);

            for(int i=0; i<linePoints;i++)
            {
                var point = new Vector2(ropePositions[0].x + distance.x * (i + 1) / linePoints, hand.transform.position.y);
                ropePositions.Add(new Vector3(point.x, point.y, -1));
            }
            //ropePositions.Add(transform.position);

            yield return null;
            ropePositions.Clear();
        }
        t = 0;
        while (t < 0.2f)
        {
            t += Time.deltaTime;
            hand.transform.position = Vector3.Lerp(targetPosition, transform.position, t / .2f);
            ropePositions.Add(hand.transform.position);

            var distance = new Vector2(transform.position.x, transform.position.y) - new Vector2(hand.transform.position.x, hand.transform.position.y);

            for (int i = 1; i < linePoints; i++)
            {
                var point = new Vector2(ropePositions[0].x + distance.x * (i + 1) / linePoints, hand.transform.position.y);
                ropePositions.Add(new Vector3(point.x, point.y, -1));
            }
            yield return null;
            ropePositions.Clear();
        }
        ResetRope();
        hand.SetActive(false);
        hookBusy = false;
    }



}
