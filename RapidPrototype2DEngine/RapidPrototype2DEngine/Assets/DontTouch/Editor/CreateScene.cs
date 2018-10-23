using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;


public class CreateScene 
{
    //bool isCreated = false;
    
    [MenuItem("Tools/Create Topdown Shooter Scene")]
    public static void CreateSceneFromScratch()
    {
        if (EditorApplication.isPlaying == true)
        {
            EditorApplication.isPlaying = false;
        }

        EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);

        string path = "Assets/DontTouch/Default Scene Prefabs/";

        CreateItem(path + "Game UI.prefab");

        CreateItem(path + "A_.prefab");
        CreateItem(path + "Game Camera.prefab");
        CreateItem(path + "Goal.prefab");
        CreateItem(path + "Player.prefab");
        CreateItem(path + "Grid.prefab");




        //GameObject c = GameObject.Find("Main Camera");
        //if (c != null)
        //    DestroyImmediate(c);

        //GameObject g = new GameObject("Scene From Scratch");
        //g.AddComponent<SceneFromScratch>(); // a scene generating script

        EditorApplication.isPlaying = false;
    }

    static void CreateItem(string path)
    {
        GameObject item = (GameObject)AssetDatabase.LoadAssetAtPath(path, typeof(GameObject)) as GameObject;
        PrefabUtility.InstantiatePrefab(item);
    }
}
