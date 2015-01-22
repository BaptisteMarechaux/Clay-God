using UnityEngine;
using UnityEditor; //Utilisable que dans un script qui se trouve dans un répertoire editor
using System.Collections;

//Classe Editor
public class MyFirstEditorClass : Editor{

	[MenuItem("CustomMenu/SayHello")] //Permet de définir le chemin du customMenu
	static public void SayHello() //Static pour que la fonction ne soit liée a aucun objet
	{
		Debug.Log("Hello Editor ! :)");
	}

	[MenuItem("CustomMenu/CreateBoxes")] //Permet de définir le chemin du customMenu
	static public void CreateBoxes(int X=0, int Y=0) //Static pour que la fonction ne soit liée a aucun objet
	{
		for(int i=0;i<=100;i++)
		{
			for(int j=0;j<=100;j++)
			{
				if(Random.Range(0f, 1f) <= 0.3f)
				{
					var go = GameObject.CreatePrimitive (PrimitiveType.Cube);
					go.transform.position = new Vector3 (i, 0, j);
				}
			}
		}

	}
}
