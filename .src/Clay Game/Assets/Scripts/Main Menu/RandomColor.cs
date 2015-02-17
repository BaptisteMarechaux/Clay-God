using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RandomColor : MonoBehaviour {
    public Image img;
    float t = 0;
    float dur = 60;
    Color nextColor;
	// Use this for initialization
	void Start () {
        nextColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 0.5f), 0);
	}
	
	// Update is called once per frame
	void Update () {
        img.color = Color.Lerp(img.color, nextColor,t);
        if(t<1)
        {
            t += Time.deltaTime / dur;
            if (t >= 0.1f)
            {
                t = 0;
                nextColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 0.5f), 0);
            }
                
        }
        
      
	}
}
