using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Dialogue : ScriptableObject
{
    public Sprite imageTop;
    public Sprite phraseBottom;
    [TextArea(12, 16)]
    public string[] texte;
}
