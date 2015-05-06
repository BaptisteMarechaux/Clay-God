using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WaitForDeactivationText : MonoBehaviour {
    public float duration;
    float t;
    public Text text;
	
	// Update is called once per frame
	void Update () {
        if (t < duration)
        {
            t += Time.deltaTime;
            if (text != null)
            {
                if (t > duration * 0.1f)
                    text.color = Color.Lerp(text.color, new Color(text.color.r, text.color.g, text.color.b, 0), duration * 1.25f * Time.deltaTime);
            }

        }
        else
        {
            t = 0;
            text.color = new Color(text.color.r, text.color.g, text.color.b, 1);
            this.gameObject.SetActive(false);
        }
	}
}
