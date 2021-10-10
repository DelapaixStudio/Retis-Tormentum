using System.Collections;
using UnityEngine;

using System.Collections.Generic;  // Nous permet d'utiliser la liste
using System.Xml;                  // Attribut de base du xml
using System.Xml.Serialization;     // accès à la serialisation
using System.IO;                    // Gestion de fichiers


// https://www.youtube.com/watch?v=GAviWvNRx-A //

public class XMLManager : MonoBehaviour
{

    public static XMLManager ins;

    void Awake()
    {
        ins = this;
    }

    //list of items
    public ItemDatabase itemDB;
}


    [System.Serializable]
    public class ItemEntry
    {
        public string itemName;
      public Material material;
        public int value;
    }

    [System.Serializable]
    public class ItemDatabase{
    public List<ItemEntry> list = new List<ItemEntry>();
        }

    public enum Material
    {
        Wood,
        Copper,
        Iron,
        Steel,
        Obsidian
    }

