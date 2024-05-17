using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ColorManagement;

[CreateAssetMenu(fileName = "ColorSO", menuName = "Color")]
public class ColorSO : ScriptableObject
{
    public List<Material> _colorMaterials;
    public Material GetMaterial(ColorType ctype)
    {
        if ((int)ctype < 0 || (int)ctype >= _colorMaterials.Count)
        {
            Debug.LogError("Invalid color type index: " + ctype);
            return null;
        }
        return _colorMaterials[(int)ctype];
    }

}
