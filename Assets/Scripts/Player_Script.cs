using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum PLAYER_MODE {SQUARE, CIRCLE, TRIANGLE}

public class Player_Script : MonoBehaviour
{
    //public variables
    public Sprite squareSprite;
    public Color squareColour;
    public Sprite circleSprite;
    public Color circleColour;
    public Sprite triangleSprite;
    public Color triangleColour;

    //private variables
    private int mode;


    public float rotation;

    private bool extended;//true if partially or fully extended
    private int extendVelocity;//negative: contracting, positive: extending, 0: stationary

    // Start is called before the first frame update
    void Start()
    {
        initialiseVariables();
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
        rigidBody.AddTorque(rotation);
    }

    private void handleControls()
    {
        if (Input.GetButtonDown("To Square"))
        {
            mode = (int)PLAYER_MODE.SQUARE;
        }
        if (Input.GetButtonDown("To Circle"))
        {
            mode = (int)PLAYER_MODE.CIRCLE;
        }
        if (Input.GetButtonDown("To Triangle"))
        {
            mode = (int)PLAYER_MODE.TRIANGLE;
        }
        rotation = -Input.GetAxis("Horizontal");
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
        rotation = 0;
        extended = false;
        extendVelocity = 0;
    }
}
