using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TitleFade : MonoBehaviour {
    [SerializeField]
    Text titleText;
    [SerializeField]
    Image titleImage;

    [SerializeField]
    float timeTowait = 3;

    float t;

    float t2;

    [SerializeField]
    Camera gameMainCamera;

    [SerializeField]
    Camera premierShotCamera;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
    void Update()
    {
        t += Time.deltaTime;
        if(t>=timeTowait)
        {
            titleText.color = Color.Lerp(titleText.color, new Color(titleText.color.r, titleText.color.g, titleText.color.b, 0), Time.deltaTime);
        }

        if (titleText.color.a < 0.03)
        {
            titleImage.color = Color.Lerp(titleImage.color, new Color(titleImage.color.r, titleImage.color.g, titleImage.color.b, 0), 5 * Time.deltaTime);
            t2 += Time.deltaTime;
        }

        if (t2 > 2f)
        {
            gameMainCamera.transform.position = Vector3.Lerp(gameMainCamera.transform.position, premierShotCamera.transform.position, 5 * Time.deltaTime);
            gameMainCamera.transform.rotation = Quaternion.Lerp(gameMainCamera.transform.rotation, premierShotCamera.transform.rotation, 5 * Time.deltaTime);
        }
        
    }
}
