using UnityEngine;
using UnityEditor;
using System.Collections;

public class TerrainObjectsEditor : EditorWindow {
    GameObject _prefab;

    bool editionActivated;
    
    public void OnGUI()
    {
        EditorGUILayout.BeginVertical();
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Coucou, ceci est une fenetre");
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Prefab to generate");
        _prefab = (GameObject)EditorGUILayout.ObjectField(_prefab, typeof(GameObject));
        EditorGUILayout.EndHorizontal();

       
        if (GUI.Button(new Rect(0, 100, 1000, 100), "Coucou, Je suis un bouton"))
        {
            Debug.Log("On m'a cliqué dessus!");
        }
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
        var window = new MyFirstEditorWindow();
        window.Show();
    }
}
