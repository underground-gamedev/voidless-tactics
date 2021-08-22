using UnityEngine;

public class PortraitComponent: ScriptableObject
{
    [SerializeField]
    private Texture portraitSmall;
    public Texture PortraitSmall => portraitSmall;

    [SerializeField]
    private Texture portraitBig;
    public Texture PortraitBig => portraitBig;
}