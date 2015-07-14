using UnityEngine;
using UnityEditor;
using System.Collections;

public class TerrainObjectsEditor : EditorWindow {
    GameObject _prefab, _mountainprefab;
    float _popRate;
    float _width;
	float _depth;

    bool editionActivated;

    bool[,] nodes;
    
    public void OnGUI()
    {
        EditorGUILayout.BeginVertical();
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Terrain Editor");
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Tree prefab");
        _prefab = (GameObject)EditorGUILayout.ObjectField(_prefab, typeof(GameObject));
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("High Montain prefab");
        _mountainprefab = (GameObject)EditorGUILayout.ObjectField(_mountainprefab, typeof(GameObject));
        EditorGUILayout.EndHorizontal();


      /*
        if (GUI.Button(new Rect(0, 100, 1000, 100), "Coucou, Je suis un bouton"))
        {
            Debug.Log("On m'a cliqué dessus!");
        }
       * */

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Pop Rate");
        _popRate = EditorGUILayout.FloatField(_popRate);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Terrain Width");
        _width = EditorGUILayout.FloatField(_width);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Terrain Depth");
        _depth = EditorGUILayout.FloatField(_depth);
        EditorGUILayout.EndHorizontal();

        if (GUILayout.Button("Place Objects"))
        {
            
            //var go = (GameObject)GameObject.Instantiate(_prefab);
            //go.transform.position = new Vector3(0, 0, 0);
            editionActivated = !editionActivated;
               
        }

        EditorGUILayout.EndVertical();
    }

    [MenuItem("MyTerrainEditor/Terrain Objects Editor")]
    public static void CreateMyTerrainOEditor()
    {
        var window = new TerrainObjectsEditor();
        window.Show();
    }
}
