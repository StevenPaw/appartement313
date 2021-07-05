using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class Dialogue
{
    public string name;
    [TextArea(3,10)] public string[] sentences;

    public Sprite npcSprite;
}
