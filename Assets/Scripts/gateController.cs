using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gateController : MonoBehaviour {

	public int[] destination = new int[] {1, 2, 3, 4, 5, 6, 0};	//Our temporary testing array
	int addressLength;
	bool validAddress;
	public int gateStatus;
	int glyphCount;
	public GameObject currentChevron;
	public string chevron;

	// Use this for initialization
	void Start () {
		addressLength = destination.Length;
		validAddress = true;
		glyphCount = 0;

		if (gateStatus == 0) {
			gateStatus = 1;
		}

		foreach (int currentGlyph in destination) {
			//dialGlyph(currentGlyph, glyphCount);
			glyphCount = glyphCount + 1;

			Debug.Log("ForEach pass " + glyphCount);
			currentChevron = GameObject.Find("/LockedChevrons/chevron" + glyphCount);
			currentChevron.gameObject.SetActive (false);
		}

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
