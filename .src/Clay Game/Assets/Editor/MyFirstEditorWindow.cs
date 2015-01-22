using UnityEngine;
using UnityEditor;
using System.Collections;

public class MyFirstEditorWindow : EditorWindow {
	GameObject _prefab;
	float _popRate;
	float _width;
	float _depth;

	public void OnGUI()
	{
		EditorGUILayout.BeginVertical ();
		EditorGUILayout.BeginHorizontal ();
			EditorGUILayout.LabelField ("Coucou, ceci est une fenetre");
		EditorGUILayout.EndHorizontal ();

		EditorGUILayout.BeginHorizontal ();
			EditorGUILayout.LabelField ("Prefab to generate");
			_prefab = (GameObject)EditorGUILayout.ObjectField (_prefab, typeof(GameObject));
		EditorGUILayout.EndHorizontal();

		EditorGUILayout.BeginHorizontal ();
			EditorGUILayout.LabelField ("Pop Rate");
			_popRate = EditorGUILayout.FloatField (_popRate);
		EditorGUILayout.EndHorizontal ();

		EditorGUILayout.BeginHorizontal ();
			EditorGUILayout.LabelField ("Width");
			_width = EditorGUILayout.FloatField (_width);
		EditorGUILayout.EndHorizontal ();

		EditorGUILayout.BeginHorizontal ();
			EditorGUILayout.LabelField ("Depth");
			_depth = EditorGUILayout.FloatField (_depth);
		EditorGUILayout.EndHorizontal();
		if(GUI.Button (new Rect (0,100,1000,100), "Coucou, Je suis un bouton"))
		{
			Debug.Log("On m'a cliqué dessus!");
		}
		if(GUILayout.Button("GENERATE!!!"))
		{
			for(int i=0;i<=10;i++)
			{
				for(int j=0;j<=10;j++)
				{
					if(Random.Range(0f, 1f) <= 0.3f)
					{
						var go = (GameObject) GameObject.Instantiate(_prefab);
						go.transform.position = new Vector3 (i, 0, j);
					}
				}
			}
		}
		EditorGUILayout.EndVertical();







	}

	[MenuItem("CustomMenu/CreateMyFristEditorWindow")]
	public static void CreateMyFirstEditorWindow()
	{
		var window = new MyFirstEditorWindow();
		window.Show();
	}
}
