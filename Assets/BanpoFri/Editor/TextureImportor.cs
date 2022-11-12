using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TextureImportor : AssetPostprocessor
{
    void OnPreprocessTexture ()
    {
        TextureImporter importer = (TextureImporter)assetImporter;
        if(importer != null)
        {
            importer.textureType = TextureImporterType.Sprite;
        }
    }
}
