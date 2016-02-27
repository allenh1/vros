using UnityEngine;
using System.Collections;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using UnityEngine.Experimental.Networking;

public class StreamImage : MonoBehaviour
{
    public string url = "http://localhost:8084/";

    public void Start()
    {
        //if (!texture)
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
    // Would make senses if we just got streams of data insted.
    // Also split recive and decode.

    void Update()
    {
        if (www != null && www.isDone)
        {
            if (string.IsNullOrEmpty(www.error))
            {
                //     SetTexture(www.textureNonReadable);
                www.LoadImageIntoTexture(texture);
            }
            //texture.Apply();
            www.Dispose();
            www = null;
        }


        var wantNewFrame = (Time.time-lastImageRequested) > 1 / rate;
        //wantNewFrame = true;
        if (www == null && wantNewFrame)
        {
            lastImageRequested = Time.time;
            www = new WWW(url);
        }

        if (Input.GetKeyDown(KeyCode.PageDown))
        {
            var w = new WWW(url + "scroll?dir=down");
        }

        if (Input.GetKeyDown(KeyCode.PageUp))
        {
            var w = new WWW(url + "scroll?dir=up");
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            var w = new WWW(url + "scroll?dir=right");
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            var w = new WWW(url + "scroll?dir=left");
        }

        if (Input.GetKeyDown(KeyCode.Home))
        {
            var w = new WWW(url + "goto?url=https://google.com");
        }

        /**
         * @todo Add the ability for users to request a url.
         */
    }

    public Texture2D texture;
}
