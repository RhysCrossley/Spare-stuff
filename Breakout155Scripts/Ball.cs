/*
 * Ball.cs 
 * 
 * Description:
 * sets the starting velocity on the
 * Rigidbody2D Component. 
 * 
 * Components used in this script:
 *      Rigidbody2D
 * 
 * Public variables:
 *      float randomStartingX
 *      - random starting x velocity
 *      - when the ball spawns, it will move either left or right at random
 *          
 *      float startingVelocityY
 *      - the starting upward velocity
 *      - when the ball spawns, it will always move this fast in the y direction
 */

// the Ball class will be attached to a 
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


// Now we create our Ball class.
// Inherit from the Monobehaviour class
// so we can use our Ball class in Unity
// on a GameObject - see link below for deocumentation
// https://docs.unity3d.com/ScriptReference/MonoBehaviour.html
// see the link below for a video on how inheritance works
// https://unity3d.com/learn/tutorials/topics/scripting/inheritance?playlist=17117https://unity3d.com/learn/tutorials/topics/scripting/inheritance?playlist=17117
public class Ball : MonoBehaviour
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
     * Random Starting X
     * type: float (a decimal number)
     * 
     * - the ball will move in a random left/right (x) direction when it spawns.
     * 
     */
    public float randomStartingX = 5;

    /*
     * Starting Velocity Y
     * type: float (a decimal number)
     * 
     * - the ball always needs to start moving in the upward (y) direction when it gets spawned.
     * 
     */
    public float startingVelocityY = 5;




    /***********************************************
     * 
     * CLASS METHODS
     * Below are all of the methods used in the class
     * 
     ***********************************************/

    // the Start method runs once when the GameObject the
    // Player class is attached to is created in the Unity Scene
    // https://docs.unity3d.com/ScriptReference/MonoBehaviour.Start.html
    private void Start ()
    {
        // Create a random x direction for the ball
        // using Random.Range
        // see link below
        // https://docs.unity3d.com/ScriptReference/Random.Range.html
        // note the number we get from Random.Range will be between a negative number
        // and a positive number (-random starting x and +random starting x)
        float randomDirectionX = Random.Range(-randomStartingX, randomStartingX);

        // Vector2 variables can store 2 values, x and y.
        // ballVelocity will be used later to set velocity on the body2D
        // link link below 
        // https://docs.unity3d.com/ScriptReference/Vector2.html
        Vector2 ballVelocity = new Vector2();

        // set the x value of our Vector2 variable ballVelocity
        // to our randomDirectionX
        ballVelocity.x = randomDirectionX;

        // set the y value to our startingVelocityY
        // so the ball always moves in the upward direction at start
        ballVelocity.y = startingVelocityY;

        // body2D will store a reference to our 
        // Rigidbody2D component - see link below for deocumentation
        // https://docs.unity3d.com/ScriptReference/Rigidbody2D.html
        Rigidbody2D body2D = GetComponent<Rigidbody2D>();

        // now we can set the velocity property of our Rigidbody2D component (called body2D)
        // to our calculated velocity in ballVelocity. 
        // the velocity property of Rigidbody2D is a Vector2, just like ballVelocity
        // see the link below
        // https://docs.unity3d.com/ScriptReference/Rigidbody2D-velocity.html
        body2D.velocity = ballVelocity;
        
	}
}
