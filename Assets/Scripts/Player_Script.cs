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

    private ArrayList rotateLeftControls;
    private ArrayList rotateRightControls;

    private ArrayList extendPistonsControls;

    // Start is called before the first frame update
    void Start()
    {
        //defaults to square when player is created
        mode = (int)PLAYER_MODE.SQUARE;
        
        initialiseArrayLists();
        addAllControls();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //FixedUpdate is called once every physics calculation
    void FixedUpdate()
    {
        foreach(string control in toSquareControls)
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
    }

    private void initialiseArrayLists()
    {
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
