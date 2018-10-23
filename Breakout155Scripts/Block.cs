/*
 * Block.cs 
 * 
 * Description:
 * the block sends its parent GameObject a message
 * when another GamObject collides with it
 * 
 * Components used in this script:
 *      Collider2D (BoxCollider2D)
 * 
 * Public variables:
 *      none
 *
 */


// the Block class will be attached to a 
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


// Now we create our Block class,
// we inherit from the Monobehaviour class
// so we can use our Block class in Unity
// on a GameObject - see link below for deocumentation
// https://docs.unity3d.com/ScriptReference/MonoBehaviour.html
// see the link below for a video on how inheritance works
// https://unity3d.com/learn/tutorials/topics/scripting/inheritance?playlist=17117https://unity3d.com/learn/tutorials/topics/scripting/inheritance?playlist=17117
public class Block : MonoBehaviour
{

    /***********************************************
     * 
     * CLASS METHODS
     * Below are all of the methods used in the class
     * 
     ***********************************************/

    /* A NOTE ON COLLISIONS IN THIS CLASS
     * 
     * A block will die when something hits it.
     * the block GameObject will be destroyed, removing it from the scene
     * when its OnCollisionEnter2D method runs.
     * 
     * if our block GameObject has any of the 2D collider compoents
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


    /* On Collision Enter
     * IMPORTANT!
     * We will use the SendMessage method in this class
     *  see link: https://docs.unity3d.com/ScriptReference/Component.SendMessage.html
     * 
     * SendMessage will try to run a method on a specified 
     * GameObject. If that GameObject does not have that 
     * method, you will get an error. this can be ignored,
     * but may be harder to debug later
     * 
     * 
     */
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // we create a variable to store the parent transform 
        // of the block GameObject. 
        // see link : https://docs.unity3d.com/ScriptReference/Transform.html
        // see link : https://docs.unity3d.com/ScriptReference/Transform-parent.html
        Transform parentTransform = transform.parent;

        /*
         * IMPORTANT!
         * we want to send the parentTransform a message to
         * tell it the block is about to die.
         * We will call our message "OnBlockDie".
         * 
         * The parentTransform's GameObject needs to have a component
         * with a method called OnBlockDie.
         * 
         * without this we will get an error!
         * 
         * The "Game" component on the "Game" GameObject we create in the editor will 
         * be the parent transform and it will have the
         * OnBlockDie method the block needs
         * 
         */
        string message = "OnBlockDie";

        /*
         * SEND THE MESSAGE
         * here we use the SendMessage method to send our OnBlockDie message
         * to the parentTransform
         * see link: https://docs.unity3d.com/ScriptReference/Component.SendMessage.html 
         * 
         */
        parentTransform.SendMessage(message);

        /*
         * DESTROY THE BLOCK
         * now we destroy the block GameObject
         * this will remove the block GameObject it and all its components from the Unity scene
         * and Hierarchy.
         * 
         * see link: https://docs.unity3d.com/ScriptReference/Object.Destroy.html
         * 
         */
        Destroy(gameObject);
    }
    
}
