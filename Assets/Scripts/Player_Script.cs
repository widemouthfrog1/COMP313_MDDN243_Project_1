using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum PLAYER_MODE {SQUARE, CIRCLE, TRIANGLE}

public class Player_Script : MonoBehaviour
{
    //public variables
    public string toSquare;
    public string toCircle;
    public string toTriangle;
    public string rotateLeft;
    public string rotateRight;
    public string extendPistons;

    public Sprite squareSprite;
    public Color squareColour;
    public Sprite circleSprite;
    public Color circleColour;
    public Sprite triangleSprite;
    public Color triangleColour;

    //private variables
    private int mode;
    private ArrayList toSquareControls;
    private ArrayList toCircleControls;
    private ArrayList toTriangleControls;

    private bool rotatingLeft;
    private ArrayList rotateLeftControls;
    private bool rotatingRight;
    private ArrayList rotateRightControls;

    private bool extended;//true if partially or fully extended
    private int extendVelocity;//negative: contracting, positive: extending, 0: stationary
    private ArrayList extendPistonsControls;

    // Start is called before the first frame update
    void Start()
    {
        initialiseVariables();
        addAllControls();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //FixedUpdate is called once every physics calculation
    void FixedUpdate()
    {
        handleControls();
        updateSprite();
        updateColliders();

        Rigidbody2D rigidBody = GetComponent<Rigidbody2D>();
        if (rotatingLeft)
        {
            rigidBody.AddTorque(1);
        }
        if (rotatingRight)
        {
            rigidBody.AddTorque(-1);
        }
    }

    private void handleControls()
    {
        foreach (string control in toSquareControls)
        {
            if (Input.GetKeyDown(control))
            {
                mode = (int)PLAYER_MODE.SQUARE;
                break;//make sure code isn't called twice for multiple key presses
            }
        }
        foreach (string control in toCircleControls)
        {
            if (Input.GetKeyDown(control))
            {
                mode = (int)PLAYER_MODE.CIRCLE;
                break;//make sure code isn't called twice for multiple key presses
            }
        }
        foreach (string control in toTriangleControls)
        {
            if (Input.GetKeyDown(control))
            {
                mode = (int)PLAYER_MODE.TRIANGLE;
                break;//make sure code isn't called twice for multiple key presses
            }
        }
        foreach (string control in rotateLeftControls)
        {
            if (Input.GetKeyDown(control))
            {
                rotatingLeft = true;
                break;//make sure code isn't called twice for multiple key presses
            }
            if (Input.GetKeyUp(control))
            {
                rotatingLeft = false;
                break;//make sure code isn't called twice for multiple key presses
            }
        }
        foreach (string control in rotateRightControls)
        {
            if (Input.GetKeyDown(control))
            {
                rotatingRight = true;
                break;//make sure code isn't called twice for multiple key presses
            }
            if (Input.GetKeyUp(control))
            {
                rotatingRight = false;
                break;//make sure code isn't called twice for multiple key presses
            }
        }
    }

    private void updateSprite()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        if (mode == (int)PLAYER_MODE.SQUARE)
        {
            if (!spriteRenderer.sprite.Equals(squareSprite))
            {
                spriteRenderer.sprite = squareSprite;
                spriteRenderer.color = squareColour;
            }
        }
        if (mode == (int)PLAYER_MODE.CIRCLE)
        {
            if (!spriteRenderer.sprite.Equals(circleSprite))
            {
                spriteRenderer.sprite = circleSprite;
                spriteRenderer.color = circleColour;
            }
        }
        if (mode == (int)PLAYER_MODE.TRIANGLE)
        {
            if (!spriteRenderer.sprite.Equals(triangleSprite))
            {
                spriteRenderer.sprite = triangleSprite;
                spriteRenderer.color = triangleColour;
            }
        }
    }

    private void updateColliders()
    {
        BoxCollider2D boxCollider = GetComponent<BoxCollider2D>();
        CircleCollider2D circleCollider = GetComponent<CircleCollider2D>();
        PolygonCollider2D polygonCollider = GetComponent<PolygonCollider2D>();
        if (mode == (int)PLAYER_MODE.SQUARE)
        {
            if (boxCollider.enabled == false)
            {
                boxCollider.enabled = true;
            }
            circleCollider.enabled = false;
            polygonCollider.enabled = false;
        }
        if (mode == (int)PLAYER_MODE.CIRCLE)
        {
            if (circleCollider.enabled == false)
            {
                circleCollider.enabled = true;
            }
            boxCollider.enabled = false;
            polygonCollider.enabled = false;
        }
        if (mode == (int)PLAYER_MODE.TRIANGLE)
        {
            if (polygonCollider.enabled == false)
            {
                polygonCollider.enabled = true;
            }
            boxCollider.enabled = false;
            circleCollider.enabled = false;
        }
    }

    private void initialiseVariables()
    {
        //defaults to square when player is created
        mode = (int)PLAYER_MODE.SQUARE;
        rotatingLeft = false;
        rotatingRight = false;
        extended = false;
        extendVelocity = 0;

        //initialse array lists
        toSquareControls = new ArrayList();
        toCircleControls = new ArrayList();
        toTriangleControls = new ArrayList();
        rotateLeftControls = new ArrayList();
        rotateRightControls = new ArrayList();
        extendPistonsControls = new ArrayList();
    }

    //Puts comma-separated controls in a string
    private void addControls(ArrayList controlList, string controls)
    {
        string[] split = controls.Split(',');
        foreach (string s in split)
        {
            controlList.Add(s);
        }
    }

    private void addAllControls()
    {
        addControls(toSquareControls, toSquare);
        addControls(toCircleControls, toCircle);
        addControls(toTriangleControls, toTriangle);
        addControls(rotateLeftControls, rotateLeft);
        addControls(rotateRightControls, rotateRight);
        addControls(extendPistonsControls, extendPistons);
    }
}
