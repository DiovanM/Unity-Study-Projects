using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour {

    [SerializeField] private float speed = 0.5f;
    private Material backgroundMaterial;
    private Vector2 offSet;

	// Use this for initialization
	void Start () {
        backgroundMaterial = GetComponent<Renderer>().material;
        offSet = new Vector2(0, speed);

    }
	
	// Update is called once per frame
	void Update () {
        backgroundMaterial.mainTextureOffset += offSet * Time.deltaTime;
	}
}
