/*
 * Game.cs 
 * 
 * Description:
 * this is the "brain" of our game.
 * the Game class controls when the game starts
 * and stops, the player lives and score and
 * the spawning of the blocks, the ball and the player
 * 
 * 
 * Components used in this script:
 *      none
 * 
 ***********************************************************
 *
 * PUBLIC VARIABLES:
 * 
 *      int startingLives 
 *          - stores the lives of the player at game start.
 *          
 *      int blockRows
 *          - the total number of rows of blocks
 *          
 *      int blockColumns
 *          - the total number of block columns
 *      
 *      float blockRowSpacing
 *          - the spacing between rows of blocks
 *      
 *      float blockColumnSpacing
 *          - the spacing between columns of blocks
 *      
 *      float  blockPositionOffsetY
 *          - moves all of the blocks up or down
 *          - useful for setting the blocks higher or lower on the screen
 *      
 *      GameObject ballPrefab
 *          - a prefab from the Project view of the ball
 *      
 *      GameObject blockPrefab
 *          - a prefab from the Project view of a block
 *      
 *      Transform ballSpawnPoint
 *          - a transform in the scene to set the position of our ball when it spawns
 *      
 *      Text livesText
 *          - the Text user interface component for lives
 *      
 *      Text scoreText
 *          - the Text user interface component for score
 *      
 *      Text highScoreText
 *          - the Text user interface component for high score
 *          
 *      GameObject startGamePanel
 *          - contains the start game button 
 *          - we can hide this while the game is being played
 *          - we can show it on game over
 *      
 ************************************************************
 * 
 * CUSTOM PUBLIC METHODS
 * 
 *      void StartGame
 *          - initialises the game
 *          - called from the start game button in the scene
 *      
 *      void OnBlockDie
 *          - a "callback" method used by the Block class to tell the game a block has been destroyed
 *          
 *      void OnLoseLife
 *          - a "callback" method used by the LoseLife class to tell the game that the ball has collided with it.
 *      
 ************************************************************
 * 
 * CUSTOM PRIVATE METHODS
 * 
 *      void GameOver
 *          - clears the game and checks for a new high score
 *          
 *      void LevelSetup
 *          - adds the blocks and a ball to the game
 *          
 *      void DestroyBlocks
 *          - removes blocks from the game
 *          
 *      void UpdateLivesText
 *          - updates the display to show current lives
 *          
 *      void UpdateScoreText
 *          - updates the display to show current score
 *          
 *      void SetHighScore(int newHighScore)
 *          - updates the saved high score with a new one if "newHighScore" is larger
 *      
 *      void SpawnBall
 *          -  creates a copy of the public "ballPrefab" variable and spawns it in the scene at the same position and rotation as the public "ballSpawnPoint" variable
 *            
 * 
 */

// the Game class will be attached to a 
// GameObject in our scene as a Component.
// All Components need to inherit from
// the Monobehaviour class in the
// UnityEngine code library.
// First we use the UnityEngine libarary
// with a C# using Directive (see link below)
// https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/using-directive
using UnityEngine;


// the Game class will control the UI elements.
// these include text fields for lives, score and high score
// the UI library is part of UnityEngine,
// so we refer to it as UnityEngine.UI
using UnityEngine.UI;

// IMPORTANT!
// below is a link to the script reference
// for all of the Unity code libraries
// This has ALL of the documentation for 
// every part of the Unity game engine
// Its also filled with small code examples
// to learn from!
// https://docs.unity3d.com/ScriptReference/index.html


// Now we create our Game class,
// we inherit from the Monobehaviour class
// so we can use our Player class in Unity
// on a GameObject - see link below for deocumentation
// https://docs.unity3d.com/ScriptReference/MonoBehaviour.html
// see the link below for a video on how inheritance works
// https://unity3d.com/learn/tutorials/topics/scripting/inheritance?playlist=17117https://unity3d.com/learn/tutorials/topics/scripting/inheritance?playlist=17117
public class Game : MonoBehaviour
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
     * PLAYER VARIABLES
     * 
     ***********************************************/ 
    // Player Starting Lives 
    // stores the lives of the player at game start.
    public int startingLives = 3;


    /***********************************************
     * 
     * BLOCK VARIABLES
     * 
     ***********************************************/

    // Block Rows 
    // the total number of rows of blocks
    public int blockRows = 5;

    // Block Columns 
    // the total number of block columns
    public int blockColumns = 5;

    // Block Row Spacing
    // the spacing between rows of blocks
    public float blockRowSpacing = 0.5f;

    // Block Column Spacing
    // the spacing between columns of blocks
    public float blockColumnSpacing = 1;

    // Block Position Offset Y
    // moves all of the blocks up or down
    // useful for setting the blocks higher or lower on the screen
    public float blockPositionOffsetY = 1;



    /***********************************************
     * 
     * PREFAB VARIABLES
     * 
     ***********************************************/

    // All of our prefab variables are of type GameObject
    // see link: https://docs.unity3d.com/ScriptReference/GameObject.html

    // Ball Prefab
    // a prefab from the Project view of the ball
    public GameObject ballPrefab;

    // Block Prefab
    // a prefab from the Project view of a block
    public GameObject blockPrefab;
    

    /************************************************
     * 
     * BALL SPAWN POINT
     * 
     ***********************************************/ 

    // Ball Spawn Point
    // a transform in the scene to set the position of our ball
    // when it spawns
    public Transform ballSpawnPoint;



    /***********************************************
     * UI VARIABLES
     * 
     * we will be setting some text for our lives, score and high score
     * we can use the UnityEngine.UI code library to access
     * Unity's UI components.
     * 
     * 
     * Text component
     *    see link: https://docs.unity3d.com/ScriptReference/UI.Text.html
     ***********************************************/

    // Lives Text
    // the Text user interface component for lives
    // its "text" property will be set when the game starts
    // and when the player loses a life
    public Text livesText;

    // Score Text
    // the Text user interface component for score
    // its "text" property will be set when the game starts
    // and when a block is destroyed
    public Text scoreText;

    // High Score Text
    // the Text user interface component for high score
    // its "text" property will be set when the game loads
    // and when the previous high score is beaten
    public Text highScoreText;

    // Start Game Panel
    // the button for starting our game is a child of this 
    // GameObject in our scene. 
    // We can deactivate the Start Game Panel to hide the 
    // button when the game starts and show it on game over
    public GameObject startGamePanel;



    /***********************************************
     * PRIVATE VARIABLES
     * these will only be used inside this class
     * they cannot be accessed from other components
     * 
     ***********************************************/

    // a private variable to store the current lives left
    // it will be set to the starting lives when a new game is started
    // and when a life is lost
    private int currentLives;

    // a private variable to store the current score
    // it will be set to zero on game start
    // and increase when a block is destroyed
    private int currentScore;



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
        // here we call the custom method "SetHighScore"
        // to display our high score in the game
        // note we are giving it a value of zero on purpose!
        SetHighScore(0);
    }
    

    /*
     * Start Game
     * A custom method to initialize the game.
     * This method will
     *  - deactivate the Start Game Panel 
     *  - set the player lives and score
     *  - call the LevelSetup method
     * 
     * NOTE:
     * This method is called from the Start Game Button
     * GameObject in the Hierarchy from the "On Click"
     * event on its Button component
     * 
     * parameters: none
     * 
     */
    public void StartGame()
    {
        /*
         * DEACTIVATE GAME BUTTON
         * deactivate the GameObject "Start Game Panel" 
         * by calling the SetActive method
         * see link: https://docs.unity3d.com/ScriptReference/GameObject.SetActive.html
         */
        startGamePanel.SetActive(false);


        /*
         * SET PLAYER LIVES
         * we have just started a new game in this method
         * se we need to set the currentLives variable
         * to our public variable, "startingLives"
         * 
         */
        currentLives = startingLives;

        // now we call the custom method "UpdateLivesText" to set our
        // text component to display our updated current lives
        UpdateLivesText();


        /*
         * SET PLAYER SCORE
         * reset the current score to zero
         * since we are starting a fresh new game
         * 
         */
        currentScore = 0;

        // call the custom method "UpdateScoreText"
        // our updated current score
        UpdateScoreText();


        /*
         * LEVEL SETUP
         * call the custom method "LevelSetup"
         * which will create our blocks and place our ball
         * 
         */
        LevelSetup();
    }

    /*
     * On Block Die
     * this is a "callback" method used by the Block class
     * to tell the game a block has been destroyed
     * 
     *  - increases current score
     *  - updates user interface with new score
     *  
     *  parameters: none
     * 
     */
    public void OnBlockDie()
    {
        // get a value for the points per block
        int pointsPerBlock = 10;

        // add points to the current score using the += operator
        currentScore += pointsPerBlock;

        UpdateScoreText();
    }


    /*
     * On Lose Life
     * this is a "callback" method used by the LoseLife class
     * to tell the game that the ball has collided with it.
     * 
     *  - decreases current lives
     *  - updates the lives text
     *  - checks if lives are below one
     *      - if lives are below one, game over
     *      - else spawn a new ball
     *  
     *  parameters: none
     * 
     */
    public void OnLoseLife()
    {
        // decrease the players lives by one
        // using the -- operator
        // see link: https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/decrement-operator
        currentLives--;


        // call the custom method "UpdateLivesText" to update the game UI
        UpdateLivesText();


        /*
         * CHECKING FOR GAME OVER
         * here we use an if statement to check if the player has any lives left
         * 
         * if the current lives are less than 1, its game over!
         * 
         * if the current lives are more than 1, spawn a new ball
         * 
         */ 
        if (currentLives < 1) // if current lives are less than 1
        {
            GameOver(); // run the custom method "GameOver"
        }
        else // if the current lives are more than 1
        {
            SpawnBall();
        }
    }


    /*
     * Game Over
     * when the player runs out of lives this method is called
     * destroys all the blocks currently left in the scene
     * activates the start game panel to show the start game button
     * sets the high score (if one was achieved)
     * 
     * parameters: none
     * 
     */
    private void GameOver()
    {
        // call the custom method "DestroyBlocks"
        // this will clear all block GameObjects from our scene
        DestroyBlocks();
        
        // activate the GameObject "Start Game Panel" 
        // by calling the SetActive method
        // see link: https://docs.unity3d.com/ScriptReference/GameObject.SetActive.html
        startGamePanel.SetActive(true);

        // call the custom method "SetHighScore"
        // to display our high score in the game
        // note we are giving it the currentScore value
        SetHighScore(currentScore);
    }


    /*
     * Level Setup
     * creates all of our blocks and places them in the scene
     * creates the ball and places it in the scene
     * 
     * - uses 2 "for" loops to creates the rows and columns of blocks
     * 
     * parameters: none
     * 
     */
    private void LevelSetup()
    {
        /*
         * for loops will run code a specified number of times.
         * this is useful for generating the blocks in rows and colmumns
         * see video link on loops: https://unity3d.com/learn/tutorials/topics/scripting/loops?playlist=17117
         * 
         * we will be using a "nested" for loop, which means that there are 2 loops
         * the first or "outer" loop will go through the rows,
         * then the "nested" or "inner" loop will run.
         *
         *************************************
         * 
         * EXAMPLE OF NESTED LOOPS:
         * Think of it like this:
         * A town has a list of streets, each street has a list of houses.
         * We could have a list of streets, and in each street is another list of houses
         * 
         * to "loop" through all the houses, we would first "loop" through the list of streets
         * the streets would be the "outer" loop and the houses would be the "inner" loop
         * 
         *************************************
         *
         * HOW IT WORKS FOR THE GAME
         * For our game, we want to spawn rows of blocks from top to buttom,
         * spaced out in columns side by side.
         * We start the first "outer" for loop with the rows.
         * The we have the "inner" loop spawn each block 
         * 
         * 
         */
         // THE OUTER LOOP
        for (int rowIndex = 0; rowIndex < blockRows; rowIndex++) // for loop to go over the rows 
        {
            // THE INNER LOOP
            for (int columnIndex = 0; columnIndex < blockColumns; columnIndex++) // for loop to go over the columns
            {
                /*
                 * SETTING THE BLOCK X AND Y POSITIONS
                 * we want to place our blocks in rows and columns 
                 * each column set be the x position of the block
                 * each row will set the y position of the block
                 * 
                 */

                /*
                 * GETTING THE GAME X POSITION
                 * we get the x position of the Game transform, which this script is attached to.
                 * The Game Transform will be the parent to the block Transform when we spawn it later
                 * see link: https://docs.unity3d.com/ScriptReference/Transform-parent.html
                 * 
                 */
                float gamePositionX = transform.position.x;

                /*
                 * GETTING THE HALF WIDTH OF ALL THE BLOCKS
                 * we need to know the combined width of all the blocks, then dvide by 2.
                 * From this information we can place the blocks side by side in the centre of the screen
                 * 
                 */ 
                float blocksHalfTotalWidth = (blockColumns / 2);

                /*
                 * SETTING BLOCK POSITION X
                 * this is in 3 stages
                 * 1. game position x - blocks half total width
                 * 2. + column index (add the "inner" for loop index, called "columnIndex")
                 * 3. * column spacing (multiplied by spacing between the blocks)
                 * 
                 *  Column Spacing: public variable called "blockColumnSpacing" is an editable variable on our component in the inspector
                 */

                // 1. block position x = game position x - blocks half total width
                float blockPositionX = gamePositionX - blocksHalfTotalWidth;

                // 2. block position x = block position x + column index (this is "inner" for loop index )
                // NOTE: we are using the += operator here, meaning (a equals a plus b)
                // see link: https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/addition-assignment-operator
                blockPositionX += columnIndex;

                // 3. block position x = block position x + column spacing (this can add padding between the blocks)
                // NOTE: we are using the *= operator here, meaing (a equals a multiplied by b)
                // see ink: https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/multiplication-assignment-operator
                blockPositionX *= blockColumnSpacing;

                
                /*
                 * GETTING THE GAME Y POSITION
                 * we get the y position of the Game transform, which this script is attached to.
                 * The Game Transform will be the parent to the block Transform when we spawn it later
                 * see link: https://docs.unity3d.com/ScriptReference/Transform-parent.html
                 * 
                 */
                float gamePositionY = transform.position.y;

                /*
                * GETTING THE HALF HEIGHT OF ALL THE BLOCKS
                * we need to know the combined height of all the blocks, then dvide by 2.
                * From this information we can place the blocks top to bottom in the centre of the screen
                * 
                */
                float blocksHalfTotalHeight = (blockRows / 2);

                /*
                 * SETTING BLOCK POSITION Y
                 * this is the same as x with an extra stage to add an offset.
                 * the offset will push all the blocks up or down the screen allowing 
                 * adustment in the editor using "blockPositionOffsetY".
                 * 1. game position y - blocks half total height
                 * 2. + row index (this is the "outer" for loop index )
                 * 3. * row spacing (multiplied by spacing can add padding between the blocks, )
                 * 4. - block position offset y (minus the public variable "blockPositionOffsetY")
                 * 
                 * Row Spacing: the row spacing variable, called, "blockRowSpacing" is an public variable on our component in the inspector
                 * Position Offset Y: offset all the blocks using "blockPositionOffsetY", a public variable on our component in the inspector
                 * 
                 */

                // 1. block position y = game position y - blocks half total width
                float blockPositionY = gamePositionY - blocksHalfTotalHeight;

                // 2. block position y = block position y + row index (this is "outer" for loop index )
                // NOTE: we are using the += operator here, meaning (a equals a plus b)
                // see link: https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/addition-assignment-operator
                blockPositionY += rowIndex;

                // 3. block position y = block position y * row spacing (this can add padding between the blocks)
                // NOTE: we are using the *= operator here, meaing (a equals a multiplied by b)
                // see link: https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/multiplication-assignment-operator
                blockPositionY *= blockRowSpacing;

                // 4. block position y = block position y - position offset y (push all blocks up or down by this much)
                // NOTE: we are using the -= operator here, meaning (a equals a minus b)
                // see link: https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/subtraction-assignment-operator
                blockPositionY -= blockPositionOffsetY;

                /*
                 * CREATE A VECTOR3 VARIABLE FOR THE BLOCK POSITION
                 * We set positions for things in Unity using Vector3 variables.
                 * Vector3 has 3 directions: x, y and z
                 * We can set these independantly on a Vector3 variable using the form vector3.x, vector3.y etc.
                 * see link: https://docs.unity3d.com/ScriptReference/Vector3-ctor.html
                 * 
                 * we will create a vector2 called "blockPosition" and set its x,y and z properties to 
                 * the block positions created earlier
                 */

                // create a new Vector3 variable
                Vector3 blockPosition = new Vector3();

                // add our block position x to the "x" property of the block position variable
                // see link: https://docs.unity3d.com/ScriptReference/Vector3-x.html
                blockPosition.x = blockPositionX;

                // add our block position y to the "y" property of the block position variable
                // see link: https://docs.unity3d.com/ScriptReference/Vector3-y.html
                blockPosition.y = blockPositionY;

                // set the z property to zero, since we are in 2D ;)
                blockPosition.z = 0;

                /*
                 * GET THE ROTATION OF THE GAME TRANSFORM
                 * our block doesn't need any rotation applied,
                 * so we get the rotation of the Game Transform.
                 * 
                 * Rotations in Unity are in Quaternions.
                 * Quaternions are like Vector3, but have 4 values instead of 3:
                 * x,y,z and w. 
                 * see link: https://docs.unity3d.com/ScriptReference/Quaternion-ctor.html
                 * 
                 */
                 // create a Quaternion variable for the block rotation and store the Game rotation in it
                Quaternion blockRotation = transform.rotation;


                /*
                 * A PARENT TRANSFORM FOR THE BLOCKS
                 * the blocks send a message called "OnBlockDie" from its Block component to its parent transform when it dies.
                 * this message is recieved by the Game (this script's transform) in its "OnBlockDie" method.
                 * we will create a Transform variable to store the blocks parent
                 * 
                 */
                // create a Transform variable and store the Game Transform in it
                // see link: https://docs.unity3d.com/ScriptReference/Transform.html
                Transform blockParent = transform;

                /*
                 * SPAWN A DAMN BLOCK ALREADY!!
                 * creating GameObjects from prefabs is done by using the
                 * Instantiate method.
                 * 
                 * Instantiate requires 3 parameters to create our block:
                 *      - the block prefab to spawn
                 *      - a position to place it
                 *      - a rotation to, er roatate it
                 */

                // here we call Instantiate to create our block, giving
                // it the block prefab, the block position and the block rotation
                // after this, a nice new block GameObject is spawned in the scene where we want it
                // see link: https://docs.unity3d.com/ScriptReference/Object.Instantiate.html 
                Instantiate(blockPrefab, blockPosition, blockRotation, blockParent);


            } // END OF "INNER" FOR LOOP

        } // END OF "OUTER" FOR LOOP


        // Spawn the ball calling the custom "SpawnBall" method
        SpawnBall();
    }


    /*
     * Destroy Blocks
     * destroys all the blocks in the scene
     * 
     *  - since all blocks are spawned with the Game Transform as their parent (the same transform this script is on)
     *    the Game transform can search through all its child transforms using "transform.GetChild".
     *  - we can also get a count of how many children the Game Transform has using "transform.childCount"  
     *  - we can destroy each block using its GameObject.
     *    
     * parameters: none
     * 
     */
    private void DestroyBlocks()
    {
        /*
         * GET THE NUMBER OF CHILDREN IN THE GAME TRANSFORM
         * use transform.chilCount to get the number of child transforms
         * see link: https://docs.unity3d.com/ScriptReference/Transform-childCount.html
         * 
         */
        int numBlockChildren = transform.childCount;

        
        /*
         * LOOP THROUGH THE GAME TRANSFORM CHILDREN
         * ....and destroy them!
         * 
         *  for loops will run code a specified number of times.
         * we use a for loop to get each one of the child transforms of of the Game.
         * each block transform found in the for loop will have its GameObject Destroyed
         * this will remove each block from the scene
         * 
         * see video link on loops: https://unity3d.com/learn/tutorials/topics/scripting/loops?playlist=17117
         * 
         */
        for (int blockChildIndex = 0; blockChildIndex < numBlockChildren; blockChildIndex++)
        {
            /*
             * GET THE CHILD TRANSFORM
             * Transform's have a method for getting their child Transforms called "GetChild"
             * we need to give it the index of the child, like this: GetChild(child index).
             * the "blockChildIndex" can be used for this
             * see link: https://docs.unity3d.com/ScriptReference/Transform.GetChild.html 
             * 
             */
            Transform blockChildTransform = transform.GetChild(blockChildIndex);


            /*
             * DESTROY THE BLOCK
             * We call the Destroy method and give it the child GameObject
             * Note: we cannot "Destroy" Transforms, but we can Destroy GameObjects
             * Note: we can "Destroy" and also "Add" other components a while the game is running, but transforms always have to be present
             * 
             * see link: https://docs.unity3d.com/ScriptReference/Object.Destroy.html
             * 
             */
            Destroy(blockChildTransform.gameObject);
        }
    }

    /*
     * Spawn Ball
     * creates a copy of the public "ballPrefab" variable and spawns it in the 
     * scene in the same position and rotation as the public "ballSpawnPoint" variable
     * 
     * parameters: none
     * 
     */
    private void SpawnBall()
    {
        /*
         * SETUP THE BALL SPAWN
         * we want to spawn a new ball, but need to know:
         *  - where to spawn (position)
         *  - which way is ball facing (rotation)
         * 
         * Position and rotaiton setting requires using the Transform component on the ball
         * We can get both from our public Transform variable ballSpawnPoint
         * 
         * Setting position and rotation also require Vector3 (for position)  
         * and Quaternion (for rotation) variables, we create them first, then spawn the ball
         * 
         * Vector3 see link: https://docs.unity3d.com/ScriptReference/Vector3.html
         * Quaternions see link: https://docs.unity3d.com/ScriptReference/Quaternion.html
         */

        // we take ballSpawnPoint's current position and store it in 
        // a Vector3 variable.
        // see link: https://docs.unity3d.com/ScriptReference/Vector3-ctor.html
        Vector3 ballPosition = ballSpawnPoint.position;

        // we take the ballSpawnPoint's current rotaiton and store it in
        // a Quaternion variable
        // see link: https://docs.unity3d.com/ScriptReference/Quaternion-ctor.html
        Quaternion ballRotation = ballSpawnPoint.rotation;

        /*
         * SPAWNING THE BALL
         * creating GameObjects from prefabs is done by using the
         * Instantiate method.
         * 
         * Instantiate requires 3 parameters to create our ball:
         *      - the ball prefab to spawn
         *      - a position to place it
         *      - a rotation to, er roatate it
         */

        // here we call Instantiate to create our ball, giving
        // it the ball prefab, the ball position and the ball rotation
        // after this, a nice new ball GameObject is spawned in the scene where we want it
        // see link: https://docs.unity3d.com/ScriptReference/Object.Instantiate.html
        Instantiate(ballPrefab, ballPosition, ballRotation);
    }

    /*
     * Update Lives Text
     * updates the display to show current lives
     * 
     *  - updates the Text UI component on the public variable, livesText
     *  
     *  parameters: none
     * 
     */
    private void UpdateLivesText()
    {
        // we want to display our lives on the livesText user interface component.
        // The component's "text" property needs a string, but our currentLives is an integer.
        // We need to convert the currentLives from an integer to a string
        // using the "ToString" method.
        // see link: https://docs.microsoft.com/en-us/dotnet/api/system.int32.tostring?view=netframework-4.7.2#System_Int32_ToString_System_String_
        string currentLivesString = currentLives.ToString();

        // now we should display some text in front of our lives  to tell the player
        // the number is their lives left.
        // we create a string with the word "LIVES: " so we can "glue" the strings together
        string livesLabel = "LIVES: ";

        // here, we "glue" together the currentLivesString and the livesLabel 
        // using the + or plus operator, also called concatenation 
        // see link: https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/string
        livesText.text = livesLabel + currentLivesString;
    }

    /*
     * Update Score Text
     * updates the display to show current score
     * 
     *  - updates the Text UI component on the public variable, scoreText
     *  
     *  parameters: none
     *  
     */
    private void UpdateScoreText()
    {
        // we want to display our score on the scoreText user interface component.
        // The component's "text" property needs a string, but our currentScore is an integer.
        // We need to convert the currentScore from an integer to a string
        // using the "ToString" method.
        // see link: https://docs.microsoft.com/en-us/dotnet/api/system.int32.tostring?view=netframework-4.7.2#System_Int32_ToString_System_String_
        string currentScoreString = currentScore.ToString();

        // now we should display some text in front of our score  to tell the player
        // the number is their current score.
        // we create a string with the word "SCORE: " so we can "glue" the strings together
        string scoreLabel = "SCORE: ";

        // here, we "glue" together the currentScoreString and the scoreLabel 
        // using the + or plus operator, also called concatenation 
        // see link: https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/string
        scoreText.text = scoreLabel + currentScoreString;
    }

    /*
     * Update High Score Text
     * updates the saved high score with a new one if "newHighScore" is larger
     *
     *  - saves the new score and updates the highScoreText component with the new highest score
     *  
     *  parameters:
     *      newHighScore
     *      - the latest high score
     *      - used to compare to current high score
     * 
     */
    private void SetHighScore(int newHighScore)
    {
        /*
         * Loading and Saving data
         * we can use the PlayerPrefs class to save data to disk.
         * we can then load the data back into the game every time it loads
         * see link: https://docs.unity3d.com/ScriptReference/PlayerPrefs.html
         * 
         */


        /*
         * Loading data
         * Below is how we load our highest score
         */
        // first we get our current highest score from the PlayerPrefs.GetInt method
        // if we didnt set a highest score yet, PlayerPrefs will create an integer 
        // variable for us and set it to zero.
        // see link: https://docs.unity3d.com/ScriptReference/PlayerPrefs.GetInt.html
        int currentSavedHighScore = PlayerPrefs.GetInt("HighScore");

        // here we check if the current saved score is less than the new high score
        if (currentSavedHighScore < newHighScore)
        {
            // if the new score is greater, we set the current score to the new high score
            currentSavedHighScore = newHighScore;

            /*
             * Saving data
             * Below is where we save our highest score 
             */
            // now we save the updated current high score to PlayerPrefs
            // using PlayerPrefs.SetInt
            // see link: https://docs.unity3d.com/ScriptReference/PlayerPrefs.SetInt.html
            PlayerPrefs.SetInt("HighScore", currentSavedHighScore);
        }
        
        // we want to display our high score on the highScoreText user interface component
        // the component's "text" property needs a string, but our high score is an integer.
        // We need to convert the current high score from an integer to a string
        // using the "ToString" method
        // see link: https://docs.microsoft.com/en-us/dotnet/api/system.int32.tostring?view=netframework-4.7.2#System_Int32_ToString_System_String_
        string currentHighScoreString = currentSavedHighScore.ToString();

        // now we should display some text in front of our score to tell the player
        // the number is their score.
        // we create a string with the word "SCORE: " so we can "glue" the strings together
        string highScoreLabel = "HIGHSCORE: ";

        // here, we "glue" together the currentHighScoreString and the highScoreLabel 
        // using the + or plus operator, also called concatenation 
        // see link: https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/string
        highScoreText.text = highScoreLabel + currentHighScoreString;
    }
}
