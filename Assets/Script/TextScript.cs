using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextScript : MonoBehaviour
{
    public float upSpeed;
    public float alphaSpeed;
    Text text;
    Color alpha;
    public float DestroyTime;

    // Start is called before the first frame update
    void Start()
    {
        print("»ý¼ºµÊ");
        text = GetComponent<Text>();
        alpha = text.color;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(0, upSpeed * Time.deltaTime, 0));
        alpha.a = Mathf.Lerp(alpha.a, 0, Time.deltaTime * alphaSpeed);
        text.color = alpha;
        Destroy(gameObject, 3f);
        print("»èÁ¦µÊ");
    }
}
