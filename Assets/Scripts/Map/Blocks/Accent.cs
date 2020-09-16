using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Accent : MonoBehaviour
{
    SpriteMask mask;

    public void setMask(Sprite maskSprite) {
        if (mask == null) {
            mask = GetComponent<SpriteMask>();
        }
        mask.sprite = maskSprite;
    }

}
