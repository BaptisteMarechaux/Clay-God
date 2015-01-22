using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(TestScript))] // [CustomEditor(typeof(MonoBehavior), true)]
public class MyFirstCustomInspector : Editor {
	public override void OnInspectorGUI()
	{
		EditorGUILayout.BeginVertical();
		EditorGUILayout.LabelField ("Coucou les amis");
		EditorGUILayout.LabelField((target as TestScript).titi.ToString());
		EditorGUILayout.EndVertical ();
		//base.OnInspectorGUI ();
	}
}
