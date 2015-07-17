using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class TerrainObjectsEditor : EditorWindow {
    GameObject _prefab, _mountainprefab; //Prefab d'arbre, Prefab de bout de montagne
    int _popRate = 5;
    int _width = 129;
	int _depth = 129;
    int _mountainsCount = 0;
    int _mountainPopRate = 5;
    Object[] _mountainTypes = new Object[10];

    List<Transform> instanciatedObjects = new List<Transform>();

    bool editionActivated;

    bool[,] nodes;

    Vector3[] verts;
    
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

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Mountains  Pop Rate");
        _mountainPopRate = EditorGUILayout.IntSlider("Mountains Pop Rate", _mountainPopRate, 0, 100);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Mountain Types Size");
        _mountainsCount = EditorGUILayout.IntSlider("Mountain Types Size", _mountainsCount, 0, 10);
        EditorGUILayout.EndHorizontal();

        for (int i = 0; i < _mountainsCount;i++)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Moutain " + i.ToString());
            _mountainTypes[i] = (GameObject)EditorGUILayout.ObjectField(_mountainTypes[i], typeof(GameObject));
            EditorGUILayout.EndHorizontal();
        }
           


      /*
        if (GUI.Button(new Rect(0, 100, 1000, 100), "Coucou, Je suis un bouton"))
        {
            Debug.Log("On m'a cliqué dessus!");
        }
       * */

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Trees Pop Rate");
        _popRate = EditorGUILayout.IntSlider("Trees Pop Rate", _popRate, 0, 10);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Terrain Width");
        _width = EditorGUILayout.IntSlider(_width, 0, 513);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Terrain Depth");
        _depth = EditorGUILayout.IntSlider(_depth, 0, 513);
        EditorGUILayout.EndHorizontal();

        if (GUILayout.Button("Place Objects"))
        {

            CreateTerrain();
               
        }
        if (GUILayout.Button("Suppress All"))
        {

            EraseScene();

        }

        if (GUILayout.Button("Create Other Plane"))
        {

            CreateMeshPlane();

        }

        EditorGUILayout.EndVertical();
    }

    [MenuItem("MyTerrainEditor/Terrain Objects Editor")]
    public static void CreateMyTerrainOEditor()
    {
        var window = new TerrainObjectsEditor();
        window.Show();
    }

    void CreateTerrain()
    {
        instanciatedObjects.Clear();
        GameObject groundPlane = GameObject.CreatePrimitive(PrimitiveType.Plane);
        groundPlane.transform.position = new Vector3(0, 0, 0);
        instanciatedObjects.Add(groundPlane.transform);
        groundPlane.transform.localScale *= 10;

        if(_mountainsCount == 0)
        {
            
        }
        else
        {
            for(int i = 0; i < 100; i++)
            {
                for(int j = 0;  j< 100; j++)
                {
                    if (Random.Range(0, 1000) <= _mountainPopRate)
                    {
                        GameObject pref = (GameObject)Instantiate(_mountainTypes[Random.Range(0, _mountainsCount)], new Vector3(i -45 , -0.1f, j -45 ), Quaternion.identity);
                        instanciatedObjects.Add(pref.transform);
                    }                   
                }
            }
        }

        if (_prefab != null)
        {
            for (int i = 0; i < 100; i++)
            {
                for (int j = 0; j < 100; j++)
                {
                    if (Random.Range(0,100) < _popRate)
                    {
                        GameObject pref = (GameObject)Instantiate(_prefab, new Vector3(i-50, 0, j-50), Quaternion.identity);
                        instanciatedObjects.Add(pref.transform);
                    }
                }       
            }
        }
    }

    void EraseScene()
    {
        if (instanciatedObjects[0] == null)
            return;
        for (int i = 0; i < instanciatedObjects.Count; i++)
        {
            if (Application.isEditor)
                GameObject.DestroyImmediate(instanciatedObjects[i].gameObject);
        }

        instanciatedObjects.Clear();
    }

    void CreateMeshPlane()
    {
        int sizeX = _width * _depth - _depth + 1;
        int sizeY = _depth;

        MeshFilter mf = new MeshFilter();

        var m = new Mesh();
        verts = new Vector3[_width*_depth];
        m.vertices = new Vector3[_width*_depth];
        var uvs = new Vector2[_width * _depth];

        int ind = 0;
        for(int i = 0;i< _width;i++)
        {
            for (int j = 0; j < _depth; j++)
            {
                verts[ind] = new Vector3(i, 0, j);
                uvs[ind] = (new Vector2(i,j));
                ind++;
            }
        }
        /*
        verts[0].Set(verts[0].x, Random.Range(0, 2), verts[0].z);
        verts[_depth - 1].Set(0,0,0);
        verts[verts.Length - _depth].Set(0,0,0);
        verts[verts.Length - 1].Set(0,0,0);
        */

        int spacing = _width + _depth;

        int spacingX = _width * _depth - _depth;

        int spacingY = _depth - 1;

        int nI = 0; int nJ = 0;

        while (spacingY > 1)
        {
            int halfSpacingX = spacingX / 2;
            int halfSpacingY = spacingY / 2;

            //Diamond Step
            while (nI < sizeX)
            {
                while (nJ < sizeY)
                {
                    verts[nI + nJ] = DiamondStep(nI + nJ, nI, nJ, halfSpacingX, halfSpacingY);
                    nJ += spacingY;
                }
                nI += spacingX;
                nJ = halfSpacingY;
            }

            //Square Step
            nI = 0;nJ = 0;
            int JStart = 0;

            while (nI < sizeX)
            {
                if ((nI / halfSpacingX) % 2 == 0)
                {
                    JStart = halfSpacingX;
                }
                else
                {
                    JStart = 0;
                }
                nJ = JStart;
                while (nJ < sizeY)
                {
                    verts[nI + nJ] = SquareStep(nI + nJ, nI, nJ, halfSpacingX, halfSpacingY);
                    nJ += spacingY;
                }
                nI += halfSpacingX;

            }

            spacingX = halfSpacingX;
            spacingY = halfSpacingY;

        }

        int[] indices = new int[(_width-1) * (_depth-1) * 2 * 3];
        int indexi = 0;

        for (int yPoint = 0; yPoint < _depth-1; yPoint++)
        {
            for(int xPoint = yPoint; xPoint < yPoint + _depth-1; xPoint+=5)
            {
                indices[indexi] = xPoint + yPoint;
                indices[indexi + 1] = xPoint + 1 + yPoint;
                indices[indexi + 2] = xPoint + 5 + yPoint;
                indexi += 3;
            }
        }

        m.SetIndices(indices , MeshTopology.Triangles, 0);

        //var newPlane = GameObject.CreatePrimitive(PrimitiveType.Plane);

        //m.uv = new Vector2[]{new Vector2 (0, 0), new Vector2 (0, 1), new Vector2(1, 1), new Vector2 (1, 0)};

        var newPlane = (GameObject)Instantiate(_mountainprefab);
        m.uv = uvs;
        m.RecalculateBounds();
        m.RecalculateNormals();
        newPlane.name = "TerrainPlane";

        newPlane.GetComponent<MeshFilter>().mesh = m;

        Debug.Log(m.vertices.Length);

    }


    Vector3 DiamondStep(int index, float nI, float nJ, float HalfX, float HalfY)
    {
        float sum = 0;
        int n = 0;

        if (nI >= HalfX && nJ >= HalfY)
        {
            var s = (nI-HalfX)+(nJ-HalfY);
            sum += verts[Mathf.RoundToInt(s)].y;
            n++;
        }

        if (nI >= HalfX && (nJ + HalfY) < _depth)
        {
            var s = (nI - HalfX) + (nJ + HalfY);
            sum += verts[Mathf.RoundToInt(s)].y;
            n++;
        }

        if((nI + HalfY) < _width && (nJ + HalfY +1 ) < _depth) 
        {
            var s = (nI + HalfX) + (nJ - HalfY);
            sum += verts[Mathf.RoundToInt(s)].y;
            n++;
        }

        if ((nI + HalfX) < _width && (nJ + HalfX + 1) < _depth)
        {
            var s = (nI + HalfX) + (nJ - HalfY);
            sum += verts[Mathf.RoundToInt(s)].y;
            n++;
        }

        return new Vector3(verts[index].x, Random.Range(0,100) * 0.01f * 2f * HalfY + sum/n, verts[index].z);
    }

    Vector3 SquareStep(int index, int nI, int nJ, float HalfX, float HalfY)
    {
        float sum = 0;
        int n = 0;

        if (nI >= HalfX)
        {
            var s = nI - HalfX + nJ;
            sum += verts[Mathf.RoundToInt(s)].y;
            n++;
        }

        if (nI + HalfX < _width)
        {
            var s = nI + HalfX + nJ;
            sum += verts[Mathf.RoundToInt(s)].y;
            n++;
        }

        if (nJ > HalfY)
        {
            var s = nI - HalfY + nJ;
            sum += verts[Mathf.RoundToInt(s)].y;
            n++;
        }

        if (nJ + HalfY < _depth)
        {
            var s = nI + HalfY + nJ;
            sum += verts[Mathf.RoundToInt(s)].y;
            n++;
        }

        return new Vector3(verts[index].x, Random.Range(0, 100) * 0.01f * 2f * HalfY + sum/n, verts[index].z);
    }
}
