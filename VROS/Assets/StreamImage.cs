using UnityEngine;

public class StreamImage : MonoBehaviour
{
    public string url = "http://localhost:8084/";

    public void Start()
    {
        texture = new Texture2D(512, 512);
        SetTexture(texture);
    }

    void SetTexture(Texture2D texture)
    {
        var render = GetComponent<Renderer>();
        if (render)
            render.material.mainTexture = texture;
    }

    WWW www;

    float lastImageRequested;
    public float rate = 30;
    // Needs to be dynamic based on focus / input.
    // Would make senses if we just got streams of data instead.
    // Also split receive and decode.

    void Update()
    {
        if (www != null && www.isDone)
        {
            if (string.IsNullOrEmpty(www.error))
            {
                //     SetTexture(www.textureNonReadable);
                www.LoadImageIntoTexture(texture);
            }
            www.Dispose();
            www = null;
        }


        var wantNewFrame = (Time.time-lastImageRequested) > 1 / rate;
        if (www == null && wantNewFrame)
        {
            lastImageRequested = Time.time;
            www = new WWW(url);
        }

        if (Input.GetKeyDown(KeyCode.PageDown))
        {
            www = new WWW(url + "scroll?dir=down");
        }

        if (Input.GetKeyDown(KeyCode.PageUp))
        {
            www = new WWW(url + "scroll?dir=up");
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            www = new WWW(url + "scroll?dir=right");
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            www = new WWW(url + "scroll?dir=left");
        }

        if (Input.GetKeyDown(KeyCode.Home))
        {
            www = new WWW(url + "goto?url=https://google.com");
        }

        /**
         * @todo Add the ability for users to request a url.
         */
    }

    public Texture2D texture;
}
