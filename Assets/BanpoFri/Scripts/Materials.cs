using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BanpoFri;

public class Materials : SingletonScriptableObject<Materials>, ILoader
{
    [System.Serializable]
    public class MaterialData
    {
        public string materialKey;
        public Material Material;
    }
    [SerializeField]
    private List<MaterialData> materialDatas = new List<MaterialData>();
    private Dictionary<string, Material> materialDataDic = new Dictionary<string, Material>();
    public List<MaterialData> MaterialDataList { get { return materialDatas; } }

    private List<AudioSource> cachedMaterials = new List<AudioSource>();

    public void Load()
    {
        cachedMaterials.Clear();
        materialDataDic.Clear();
        foreach(var sd in materialDatas)
        {
            materialDataDic.Add(sd.materialKey, sd.Material);
        }
    }

    public Material GetMaterial(string key)
    {
        if(materialDataDic.ContainsKey(key))
            return materialDataDic[key];

        return null;
    }
}
