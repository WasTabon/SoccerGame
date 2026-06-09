using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class SkinManagerSetup : EditorWindow
{
    [MenuItem("SoccerGame/Setup Default Skins")]
    public static void Setup()
    {
        GameObject obj = GameObject.Find("SkinManager");
        if (obj == null)
        {
            obj = new GameObject("SkinManager");
            obj.AddComponent<SkinManager>();
        }

        SkinManager sm = obj.GetComponent<SkinManager>();
        if (sm == null) sm = obj.AddComponent<SkinManager>();

        sm.allSkins = new List<SkinData>
        {
            new SkinData { id = "default", displayName = "Classic", price = 0, color = Color.white },
            new SkinData { id = "fire", displayName = "Fire", price = 10, color = new Color(1f, 0.4f, 0.1f) },
            new SkinData { id = "ice", displayName = "Ice", price = 15, color = new Color(0.3f, 0.7f, 1f) },
            new SkinData { id = "toxic", displayName = "Toxic", price = 20, color = new Color(0.3f, 1f, 0.3f) },
            new SkinData { id = "gold", displayName = "Gold", price = 30, color = new Color(1f, 0.85f, 0f) },
            new SkinData { id = "shadow", displayName = "Shadow", price = 50, color = new Color(0.4f, 0.2f, 0.6f) }
        };

        EditorUtility.SetDirty(sm);
        Debug.Log("Default skins set on SkinManager. Now assign Ball Sprite for each skin in Inspector.");
    }
}
