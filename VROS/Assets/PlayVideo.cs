using UnityEngine;
using System.Collections;

public class PlayVideo : MonoBehaviour {
	// Use this for initialization
	void Start () {
        var mat = GetComponent<Renderer>().material;
        var movie = (MovieTexture)mat.mainTexture;
        if (!movie.isPlaying)
        {
            movie.Play();
            GetComponent<AudioSource>().Play();
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
