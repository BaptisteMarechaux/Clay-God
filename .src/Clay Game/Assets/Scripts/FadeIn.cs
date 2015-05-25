using UnityEngine;
using System.Collections;

public class FadeIn : MonoBehaviour {
    [SerializeField]
    Material mat;

    void OnEnable()
    {
        mat.color = new Color(mat.color.r, mat.color.g, mat.color.g, 0);
        
    }
	
	// Update is called once per frame
	void Update () {
	    
	}

    void FixedUpate()
    {
        mat.color = Color.Lerp(mat.color, new Color(mat.color.r, mat.color.g, mat.color.g, 1), Time.deltaTime);
    }
}
