using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu]
[CustomGridBrush(false, true, false, "Hurt Block Brush")]
public class BlockBrush : GridBrushBase
{
    public GameObject prefab;
    public string tilemapName = "";

    //public int m_Z;

    public override void Paint(GridLayout grid, GameObject brushTarget, Vector3Int position)
    {
        // Do not allow editing palettes
        if (brushTarget.layer == 31)
            return;

        //Debug.Log(brushTarget.name);
        if(brushTarget.name != tilemapName)
        {
            brushTarget = GameObject.Find(tilemapName);
        }

        GameObject instance = (GameObject)PrefabUtility.InstantiatePrefab(prefab);
        Undo.RegisterCreatedObjectUndo((Object)instance, "Paint Prefabs");
        if (instance != null)
        {
            instance.transform.SetParent(brushTarget.transform);
            instance.transform.position = grid.LocalToWorld(grid.CellToLocalInterpolated(new Vector3Int(position.x, position.y, 0) + new Vector3(.5f, .5f, .5f)));
        }
    }

    public override void Erase(GridLayout grid, GameObject brushTarget, Vector3Int position)
    {
        // Do not allow editing palettes
        if (brushTarget.layer == 31)
            return;

        if (brushTarget.name != tilemapName)
        {
            brushTarget = GameObject.Find(tilemapName);
        }

        Transform erased = GetObjectInCell(grid, brushTarget.transform, new Vector3Int(position.x, position.y, 0));
        if (erased != null)
            Undo.DestroyObjectImmediate(erased.gameObject);
    }

    private static Transform GetObjectInCell(GridLayout grid, Transform parent, Vector3Int position)
    {
        int childCount = parent.childCount;
        Vector3 min = grid.LocalToWorld(grid.CellToLocalInterpolated(position));
        Vector3 max = grid.LocalToWorld(grid.CellToLocalInterpolated(position + Vector3Int.one));
        Bounds bounds = new Bounds((max + min) * .5f, max - min);

        for (int i = 0; i < childCount; i++)
        {
            Transform child = parent.GetChild(i);
            if (bounds.Contains(child.position))
                return child;
        }
        return null;
    }

    [CustomEditor(typeof(BlockBrush))]
    public class BlockBrushEditor : GridBrushEditorBase
    {
        private BlockBrush prefabBrush { get { return target as BlockBrush; } }

        private SerializedProperty m_Prefabs;
        private SerializedObject m_SerializedObject;

        protected void OnEnable()
        {
            m_SerializedObject = new SerializedObject(target);
            m_Prefabs = m_SerializedObject.FindProperty("prefab");
        }

        public override void OnPaintInspectorGUI()
        {
            m_SerializedObject.UpdateIfRequiredOrScript();
            //prefabBrush.m_PerlinScale = EditorGUILayout.Slider("Perlin Scale", prefabBrush.m_PerlinScale, 0.001f, 0.999f);
            //prefabBrush.m_Z = EditorGUILayout.IntField("Position Z", prefabBrush.m_Z);

            //EditorGUILayout.PropertyField(m_Prefabs, new GUIContent("Game Object"));
            m_SerializedObject.ApplyModifiedPropertiesWithoutUndo();
        }
    }

}
