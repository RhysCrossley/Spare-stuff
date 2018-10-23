/*
 * Player.cs 
 * 
 * Description:
 * Controls the players left and right movement 
 * using the velocity property on the attached 
 * Rigidbody2D Component. 
 * 
 * Components used in this script:
 *      Rigidbody2D
 * 
 * Public variables:
 *      moveSpeed
 *
 */


// the Player class will be attached to a 
// GameObject in our scene as a Component.
// All Components need to inherit from
// the Monobehaviour class in the
// UnityEngine code library.
// First we use the UnityEngine libarary
// with a C# using Directive (see link below)
// https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/using-directive
using UnityEngine;

// IMPORTANT!
// below is a link to the script reference
// for all of the Unity code libraries
// This has ALL of the documentation for 
// every part of the Unity game engine
// Its also filled with small code examples
// to learn from!
// https://docs.unity3d.com/ScriptReference/index.html



// Now we create our Player class,
// we inherit from the Monobehaviour class
// so we can use our Player class in Unity
// on a GameObject - see link below for deocumentation
// https://docs.unity3d.com/ScriptReference/MonoBehaviour.html
// see the link below for a video on how inheritance works
// https://unity3d.com/learn/tutorials/topics/scripting/inheritance?playlist=17117https://unity3d.com/learn/tutorials/topics/scripting/inheritance?playlist=17117
public class Player : MonoBehaviour
{
    /*
     * variables set at the start of a class
     * can be changed in the Unity editor if they are
     * set to public, for example:
     * public float speed = 5;
     * 
     * See the link below
     * https://docs.unity3d.com/Manual/VariablesAndTheInspector.html 
     * 
     */


    /***********************************************
     * 
     * PUBLIC VARIABLES
     * these variables can be adjusted in the Unity
     * Editor
     * 
     ***********************************************/

    /*
     * Move Speed
     * the float (a decimal number) "moveSpeed" sets 
     * the movement speed of the player.
     * 
     */
    public float moveSpeed = 5;




    /***********************************************
     * PRIVATE VARIABLES
     * the varibles below will only be used inside 
     * this class they cannot be accessed from 
     * other components
     * 
     ***********************************************/

    /*
     * Body 2D
     * body2D will store a reference to our 
     * Rigidbody2D component - see link below for deocumentation
     * https://docs.unity3d.com/ScriptReference/Rigidbody2D.html
     */
    Rigidbody2D body2D;



    /***********************************************
     * 
     * CLASS METHODS
     * Below are all of the methods used in the class
     * 
     ***********************************************/

    /*
     * Start
     * the Start method  is part of the Monobehaviour class.
     * it runs once when the GameObject the
     * Player class is attached to is created in the Unity Scene
     * https://docs.unity3d.com/ScriptReference/MonoBehaviour.Start.html
     * 
     */
    private void Start ()
    {
        // here we get our reference to the
        // Rigidbody component so we can use it later
        body2D = GetComponent<Rigidbody2D>();
	}
    

    /*
     * Update
     * the Update method is part of the MonoBehaviour class.
     * it runs after the Start method
     * it will continue running at the maximum frames 
     * per second roughly 30-60 times per second or more
     * see link below for deocumentation
     * https://docs.unity3d.com/ScriptReference/MonoBehaviour.Update.html
     * 
     */
    private void Update ()
    {
        /* 
         * LEFT TO RIGHT MOVEMENT
         * to move left and right we will need input
         * from the keyboard. Using the Input class in Unity
         * we can access pre-made inputs by Unity.
         * these can be configured in the Editor - see link below
         * file:///G:/Program%20Files/Unity_2018_15f1/Editor/Data/Documentation/en/Manual/ConventionalGameInput.html
         * Horizontal is a pre-made input that gets the
         * "A" and "D" keys together as an axis like plaotting a point on a graph
         * the axis works like this: 
         * if "A" is pressed, the axis is -1
         * if "D" is pressed, the axis is 1
         * if neither key is pressed, the axis is 0
         * 
         */

        /*
         * GET THE RAW AXIS FOR LEFT AND RIGHT
         * note we use GetAxisRaw to get our keyboard input. 
         * this takes the "raw" key press and doesn't use the
         * settings from the Editor to smooth the axis value out over time
         * see the link below
         * https://docs.unity3d.com/ScriptReference/Input.GetAxisRaw.html
         *  
         */
        float playerInputX = Input.GetAxisRaw("Horizontal");

        /*
         * SET VELOCITY X 
         * create a float variable to store velocity in the x direction by
         * multiplying playerInputX by our public moveSpeed property
         * if "A" key is pressed, the playerInputX will be -1
         * making this variable also a minus number 
         */
        float velocityX = playerInputX * moveSpeed;


        /*
         * CREATE THE MOVEMENT VELOCITY VECTOR2
         * Vector2 variables can store 2 values, x and y.
         * movementVelocity will be used later to set velocity on the body2D
         * link link below 
         * https://docs.unity3d.com/ScriptReference/Vector2.html
         * 
         */
        Vector2 movementVelocity = new Vector2();

        /*
         * SET THE MOVEMENT VELOCITY X
         * set the x value of our Vector2 variable movementVelocity
         * to our calculated velocityX variable
         * 
         */
        movementVelocity.x = velocityX;


        /*
         * SET THE MOVEMENT VELOCITY Y
         * we don't move up and down in this game, so
         * set the y value of movementVelocity to zero;
         * 
         */
        movementVelocity.y = 0;


        /*
         * SET THE BODY2D VELOCITY TO MOVEMENT VELOCITY
         * now we can set the velocity property of our body2D to our calculated
         * velocity in movementVelocity. 
         * the body2D.velocity is a Vector2, just like movementVelocity
         * see link: https://docs.unity3d.com/ScriptReference/Rigidbody2D-velocity.html 
         * 
         */
        body2D.velocity = movementVelocity;
	}
}
