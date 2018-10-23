/*
 * LoseLife.cs 
 * 
 * Description:
 * sends a UnityEvent called "OnLoseLife" when something collides with it
 * 
 * Components used in this script:
 *      Collider2D (BoxCollider2D)
 * 
 * Public variables:
 *      UnityEvent onLoseLife
 *          - invokes an event when something collides with this GameObject
 *          - used to tell the Game that the player has lost a life
 *
 */

// the LoseLife class will be attached to a 
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


// the LoseLife class will send a UnityEvent.
// the Evevnts library is part of UnityEngine,
// so we refer to it as UnityEngine.Events
using UnityEngine.Events;


// Now we create our Game class,
// we inherit from the Monobehaviour class
// so we can use our Player class in Unity
// on a GameObject - see link below for deocumentation
// https://docs.unity3d.com/ScriptReference/MonoBehaviour.html
// see the link below for a video on how inheritance works
// https://unity3d.com/learn/tutorials/topics/scripting/inheritance?playlist=17117https://unity3d.com/learn/tutorials/topics/scripting/inheritance?playlist=17117
public class LoseLife : MonoBehaviour
{

    /***********************************************
     * 
     * PUBLIC VARIABLES
     * these variables can be adjusted in the Unity
     * Editor
     * 
     ***********************************************/

    /*
     * On Lose Life
     * type: UntyEvent
     * 
     * - this will send an event using the Invoke method like so: UnityEvent.Invoke()
     * - this event will be "listened" to by the game.
     * - We set the game to "lisen" for the event in the editor
     * - the "onLoseLife" event will be activated or "Invoked" when something collides with with this GameObject
     * 
     * see link: https://docs.unity3d.com/ScriptReference/Events.UnityEvent.html
     */
    public UnityEvent onLoseLife;



    /***********************************************
     * 
     * CLASS METHODS
     * Below are all of the methods used in the class
     * 
     ***********************************************/

    /*
     * On Collision Enter 2D
     * if our LoseLife GameObject has any of the 2D collider compoents
     * like (BoxCollider2D, CircleCollider2D, CapsuleCollider2D or PolygonCollider2D)
     * and that 2D collider's "Is Trigger" property is unticked (or false)
     * then it will create a "Collision" event.
     * we can "listen" for that event with the following methods:
     * 
     * OnCollisionEnter2D - when something first starts colliding
     *     see link: https://docs.unity3d.com/ScriptReference/MonoBehaviour.OnCollisionEnter2D.html
     * 
     * OnCollisionStay2D - for every frame per second things are colliding
     *     see link: https://docs.unity3d.com/ScriptReference/MonoBehaviour.OnCollisionStay2D.html
     * 
     * OnCollisionExit - when things stop colliding
     *     see link: https://docs.unity3d.com/ScriptReference/MonoBehaviour.OnCollisionExit2D.html
     * 
     * Each Collision event will also provide a Collision2D object, this
     * object contains information about what was hit, including the other GameObject
     * we just Collided with.
     *     see link: https://docs.unity3d.com/ScriptReference/Collision2D.html
     * 
     */
    private void OnCollisionEnter2D(Collision2D collision)
    {
        /*
         * GET THE BALL GAMEOBJECT
         * the parameter "collision" from the OnCollisionEnter2D method
         * contains a reference to the ball GameObject
         * 
         * Create a variable to store the ball GameObject so we can destroy the ball
         * 
         */ 
        GameObject ballGameObject = collision.gameObject;

        /*
         * DESTROY THE BALL
         * now we destroy the ball GameObject
         * this will remove the ball GameObject it and all its components from the Unity scene
         * and Hierarchy.
         * 
         * see link: https://docs.unity3d.com/ScriptReference/Object.Destroy.html
         * 
         */
        Destroy(ballGameObject);


        /*
         * INVOKE THE ON LOSE LIFE EVENT
         * we call the Invoke method on our "onLoseLife" event
         * this will send the event to the Game
         * see link: https://docs.unity3d.com/ScriptReference/Events.UnityEvent.Invoke.html
         * 
         */
        onLoseLife.Invoke();
    }
}
