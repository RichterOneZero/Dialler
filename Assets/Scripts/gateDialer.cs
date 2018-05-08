using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gateDialer : MonoBehaviour {

	public int[] destination = new int[] {1, 2, 3, 4, 5, 6, 0};	//Our temporary testing array
	int addressLength;
	bool validAddress;
	int glyphCount;
	public GameObject currentChevron;
    public GameObject currentGlyph;
    public GameObject lockedGlyph;
    public int gateState;
    float waitTime;
    AudioSource chevronLockSound;
    AudioSource gateRollSound;
    AudioSource gateOpenSound;
    AudioSource gateCloseSound;
    public Text statusText;



	void Start () {
		addressLength = destination.Length;
		validAddress = true;
		glyphCount = 0;
        gateState = 1;
        statusText = "";

        gateRollSound = GameObject.Find("audioGateRoll").GetComponent<AudioSource>();
        gateOpenSound = GameObject.Find("audioGateOpen").GetComponent<AudioSource>();

	}

    void FixedUpdate()
    {
        if (gateState == 1) {   //If gate is waiting to dial...
           StartCoroutine(dialAddress());  //begin dialing address
        }
    }

    IEnumerator dialAddress() {
        gateState = 2;  //Gate is now dialing

        yield return new WaitForSeconds(5); //TEST WAIT

        foreach (int glyphNo in destination) {
			glyphCount = glyphCount + 1;
			currentChevron = GameObject.Find("/lockedChevrons/chevron" + glyphCount);
            lockedGlyph = GameObject.Find("/lockedGlyphs/glyphLocked" + glyphCount);
            chevronLockSound = currentChevron.GetComponent<AudioSource>();

            //Create currentGlyph GameObject
            //Set currentGlyph GameObject Sprite
            gateRollSound.Play();
            StartCoroutine(fadeInAndOut(currentGlyph, true, 1.5f));    //Fade in our currentGlyph
            Debug.Log("Chevron" + glyphCount + " encoded");

            waitTime = 2;
            yield return new WaitForSeconds(waitTime);  //Wait for chevron lock

            gateRollSound.Stop();
            if (glyphCount != addressLength) {
                lockChevron();
            }
            else {
                if (validAddress == true);
                lockChevron();
                yield return new WaitForSeconds(2);
                gateOpenSound.Play();
                statusText.text = "Wormhole Engaged";
                gateState = 4;
            }
            yield return new WaitForSeconds(2);  //Wait before next chevron
		}
    }

    void lockChevron() {
            chevronLockSound.Play();
            StartCoroutine(fadeInAndOut(currentChevron, true, 0.1f));
            Debug.Log("Chevron" + glyphCount + " locked!");

            StartCoroutine(fadeInAndOut(currentGlyph, false, 1.5f));    //Fade out our currentGlyph
            StartCoroutine(fadeInAndOut(lockedGlyph, true, 1.5f));      //Fade in our lockedGlyph
    }

IEnumerator fadeInAndOut(GameObject objectToFade, bool fadeIn, float duration) {  //Function for fading in/out objects
        float counter = 0f;

        //Set Values depending on if fadeIn or fadeOut
        float a, b;
        if (fadeIn)
        {
            a = 0;
            b = 1;
        }
        else
        {
            a = 1;
            b = 0;
        }

        Color currentColor = Color.clear;
        SpriteRenderer tempSPRenderer = objectToFade.GetComponent<SpriteRenderer>();
        currentColor = tempSPRenderer.color;

        while (counter < duration)
        {
            counter += Time.deltaTime;
            float alpha = Mathf.Lerp(a, b, counter / duration);
            tempSPRenderer.color = new Color(currentColor.r, currentColor.g, currentColor.b, alpha);
            yield return null;
        }
    }

}