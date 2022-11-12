using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CommonCreator 
{
    [MenuItem("GameObject/BanpoFri Common UI/Dim", false, 8)]
    public static void CommonDim()
    {
        var loadResource = 
            AssetDatabase.LoadAssetAtPath<GameObject>(
                "Assets/Arts/Prefabs/Common/Dim.prefab");
        
        var obj = PrefabUtility.InstantiatePrefab(loadResource) as GameObject;
        if(Selection.activeGameObject != null)
            obj.transform.SetParent(Selection.activeGameObject.transform, false);
    }

        [MenuItem("GameObject/BanpoFri Common UI/Bg", false, 8)]
    public static void CommonBg()
    {
        var loadResource = 
            AssetDatabase.LoadAssetAtPath<GameObject>(
                "Assets/Arts/Prefabs/Common/Bg.prefab");
        
        var obj = PrefabUtility.InstantiatePrefab(loadResource) as GameObject;
        if(Selection.activeGameObject != null)
            obj.transform.SetParent(Selection.activeGameObject.transform, false);
    }

    [MenuItem("GameObject/BanpoFri Common UI/BtnClose", false, 8)]
    public static void CommonBtnClose()
    {
        var loadResource = 
            AssetDatabase.LoadAssetAtPath<GameObject>(
                "Assets/Arts/Prefabs/Common/Btn_Close.prefab");
        
        var obj = PrefabUtility.InstantiatePrefab(loadResource) as GameObject;
        if(Selection.activeGameObject != null)
            obj.transform.SetParent(Selection.activeGameObject.transform, false);
    }
    
        [MenuItem("GameObject/BanpoFri Common UI/BtnImage", false, 8)]
    public static void CommonBtnImage()
    {
        var loadResource = 
            AssetDatabase.LoadAssetAtPath<GameObject>(
                "Assets/Arts/Prefabs/Common/Btn_Image.prefab");
        
        var obj = PrefabUtility.InstantiatePrefab(loadResource) as GameObject;
        if(Selection.activeGameObject != null)
            obj.transform.SetParent(Selection.activeGameObject.transform, false);
    }

    [MenuItem("GameObject/BanpoFri Common UI/BtnIcon", false, 8)]
    public static void CommonBtnIcon()
    {
        var loadResource = 
            AssetDatabase.LoadAssetAtPath<GameObject>(
                "Assets/Arts/Prefabs/Common/Btn_Icon.prefab");
        
        var obj = PrefabUtility.InstantiatePrefab(loadResource) as GameObject;
        if(Selection.activeGameObject != null)
            obj.transform.SetParent(Selection.activeGameObject.transform, false);        
    }

    [MenuItem("GameObject/BanpoFri Common UI/BtnIconTMP", false, 8)]
    public static void CommonBtnIconTMP()
    {
        var loadResource = 
            AssetDatabase.LoadAssetAtPath<GameObject>(
                "Assets/Arts/Prefabs/Common/Btn_IconTMP.prefab");
        
        var obj = PrefabUtility.InstantiatePrefab(loadResource) as GameObject;
        if(Selection.activeGameObject != null)
            obj.transform.SetParent(Selection.activeGameObject.transform, false);        
    }

    [MenuItem("GameObject/BanpoFri Common UI/BtnTMP", false, 8)]
    public static void CommonBtnTMP()
    {
        var loadResource = 
            AssetDatabase.LoadAssetAtPath<GameObject>(
                "Assets/Arts/Prefabs/Common/Btn_TMP.prefab");
        
        var obj = PrefabUtility.InstantiatePrefab(loadResource) as GameObject;
        if(Selection.activeGameObject != null)
            obj.transform.SetParent(Selection.activeGameObject.transform, false);        
    }

        [MenuItem("GameObject/BanpoFri Common UI/BtnVertical", false, 8)]
    public static void CommonBtnVertical()
    {
        var loadResource = 
            AssetDatabase.LoadAssetAtPath<GameObject>(
                "Assets/Arts/Prefabs/Common/Btn_Vertical.prefab");
        
        var obj = PrefabUtility.InstantiatePrefab(loadResource) as GameObject;
        if(Selection.activeGameObject != null)
            obj.transform.SetParent(Selection.activeGameObject.transform, false);        
    }


    [MenuItem("GameObject/BanpoFri Common UI/BtnYES", false, 8)]
    public static void CommonBtnYes()
    {
        var loadResource = 
            AssetDatabase.LoadAssetAtPath<GameObject>(
                "Assets/Arts/Prefabs/Common/Btn_Yes.prefab");
        
        var obj = PrefabUtility.InstantiatePrefab(loadResource) as GameObject;
        if(Selection.activeGameObject != null)
            obj.transform.SetParent(Selection.activeGameObject.transform, false);        
    }

        [MenuItem("GameObject/BanpoFri Common UI/BtnNo", false, 8)]
    public static void CommonBtnNo()
    {
        var loadResource = 
            AssetDatabase.LoadAssetAtPath<GameObject>(
                "Assets/Arts/Prefabs/Common/Btn_No.prefab");
        
        var obj = PrefabUtility.InstantiatePrefab(loadResource) as GameObject;
        if(Selection.activeGameObject != null)
            obj.transform.SetParent(Selection.activeGameObject.transform, false);        
    }

    [MenuItem("GameObject/BanpoFri Common UI/BtnPoint", false, 8)]
    public static void CommonBtnPoint()
    {
        var loadResource = 
            AssetDatabase.LoadAssetAtPath<GameObject>(
                "Assets/Arts/Prefabs/Common/Btn_Point.prefab");
        
        var obj = PrefabUtility.InstantiatePrefab(loadResource) as GameObject;
        if(Selection.activeGameObject != null)
            obj.transform.SetParent(Selection.activeGameObject.transform, false);        
    }

        [MenuItem("GameObject/BanpoFri Common UI/BtnVIP", false, 8)]
    public static void CommonBtnVIP()
    {
        var loadResource = 
            AssetDatabase.LoadAssetAtPath<GameObject>(
                "Assets/Arts/Prefabs/Common/Btn_VIP.prefab");
        
        var obj = PrefabUtility.InstantiatePrefab(loadResource) as GameObject;
        if(Selection.activeGameObject != null)
            obj.transform.SetParent(Selection.activeGameObject.transform, false);        
    }

            [MenuItem("GameObject/BanpoFri Common UI/BtnGem", false, 8)]
    public static void CommonBtnGem()
    {
        var loadResource = 
            AssetDatabase.LoadAssetAtPath<GameObject>(
                "Assets/Arts/Prefabs/Common/Btn_Gem.prefab");
        
        var obj = PrefabUtility.InstantiatePrefab(loadResource) as GameObject;
        if(Selection.activeGameObject != null)
            obj.transform.SetParent(Selection.activeGameObject.transform, false);        
    }

            [MenuItem("GameObject/BanpoFri Common UI/BtnCash", false, 8)]
    public static void CommonBtnCash()
    {
        var loadResource = 
            AssetDatabase.LoadAssetAtPath<GameObject>(
                "Assets/Arts/Prefabs/Common/Btn_Cash.prefab");
        
        var obj = PrefabUtility.InstantiatePrefab(loadResource) as GameObject;
        if(Selection.activeGameObject != null)
            obj.transform.SetParent(Selection.activeGameObject.transform, false);        
    }

                [MenuItem("GameObject/BanpoFri Common UI/BtnReward", false, 8)]
    public static void CommonBtnReward()
    {
        var loadResource = 
            AssetDatabase.LoadAssetAtPath<GameObject>(
                "Assets/Arts/Prefabs/Common/Btn_Reward.prefab");
        
        var obj = PrefabUtility.InstantiatePrefab(loadResource) as GameObject;
        if(Selection.activeGameObject != null)
            obj.transform.SetParent(Selection.activeGameObject.transform, false);        
    }

    [MenuItem("GameObject/BanpoFri Common UI/Popup", false, 8)]
    public static void CommonPopup()
    {
        var loadResource = 
            AssetDatabase.LoadAssetAtPath<GameObject>(
                "Assets/Arts/Prefabs/Common/Popup.prefab");
        
        var obj = PrefabUtility.InstantiatePrefab(loadResource) as GameObject;
        if(Selection.activeGameObject != null)
            obj.transform.SetParent(Selection.activeGameObject.transform, false);        
    }

    
    [MenuItem("GameObject/BanpoFri Common UI/Progress", false, 8)]
    public static void CommonProgress()
    {
        var loadResource = 
            AssetDatabase.LoadAssetAtPath<GameObject>(
                "Assets/Arts/Prefabs/Common/Progress.prefab");
        
        var obj = PrefabUtility.InstantiatePrefab(loadResource) as GameObject;
        if(Selection.activeGameObject != null)
            obj.transform.SetParent(Selection.activeGameObject.transform, false);        
    }
    
    [MenuItem("GameObject/BanpoFri Common UI/Reddot", false, 8)]
    public static void CommonReddot()
    {
        var loadResource = 
            AssetDatabase.LoadAssetAtPath<GameObject>(
                "Assets/Arts/Prefabs/Common/Reddot.prefab");
        
        var obj = PrefabUtility.InstantiatePrefab(loadResource) as GameObject;
        if(Selection.activeGameObject != null)
            obj.transform.SetParent(Selection.activeGameObject.transform, false);        
    }

        [MenuItem("GameObject/BanpoFri Common UI/TagSales", false, 8)]
    public static void CommonTagSales()
    {
        var loadResource = 
            AssetDatabase.LoadAssetAtPath<GameObject>(
                "Assets/Arts/Prefabs/Common/Tag_Sales.prefab");
        
        var obj = PrefabUtility.InstantiatePrefab(loadResource) as GameObject;
        if(Selection.activeGameObject != null)
            obj.transform.SetParent(Selection.activeGameObject.transform, false);        
    }

            [MenuItem("GameObject/BanpoFri Common UI/TabList", false, 8)]
    public static void CommonTabList()
    {
        var loadResource = 
            AssetDatabase.LoadAssetAtPath<GameObject>(
                "Assets/Arts/Prefabs/Common/TabList.prefab");
        
        var obj = PrefabUtility.InstantiatePrefab(loadResource) as GameObject;
        if(Selection.activeGameObject != null)
            obj.transform.SetParent(Selection.activeGameObject.transform, false);        
    }

    [MenuItem("GameObject/BanpoFri Common UI/TMP", false, 8)]
    public static void CommonTMP()
    {
        var loadResource = 
            AssetDatabase.LoadAssetAtPath<GameObject>(
                "Assets/Arts/Prefabs/Common/Text.prefab");
        
        var obj = PrefabUtility.InstantiatePrefab(loadResource) as GameObject;
        if(Selection.activeGameObject != null)
            obj.transform.SetParent(Selection.activeGameObject.transform, false);        
    }

    [MenuItem("GameObject/BanpoFri Common UI/TMP_SprtieRenderer", false, 8)]
    public static void CommonTMPSpriteRenderer()
    {
        var loadResource = 
            AssetDatabase.LoadAssetAtPath<GameObject>(
                "Assets/Arts/Prefabs/Common/Text_SprtieRenderer.prefab");
        
        var obj = PrefabUtility.InstantiatePrefab(loadResource) as GameObject;
        if(Selection.activeGameObject != null)
            obj.transform.SetParent(Selection.activeGameObject.transform, false);        
    }

    [MenuItem("GameObject/BanpoFri Common UI/ToggleBtn", false, 8)]
    public static void CommonToggleBtn()
    {
        var loadResource = 
            AssetDatabase.LoadAssetAtPath<GameObject>(
                "Assets/Arts/Prefabs/Common/ToggleBtn.prefab");
        
        var obj = PrefabUtility.InstantiatePrefab(loadResource) as GameObject;
        if(Selection.activeGameObject != null)
            obj.transform.SetParent(Selection.activeGameObject.transform, false);        
    }

    [MenuItem("GameObject/BanpoFri Common UI/IconTextInfo", false, 8)]
    public static void CommonIconTextInfo()
    {
        var loadResource = 
            AssetDatabase.LoadAssetAtPath<GameObject>(
                "Assets/Arts/Prefabs/Common/IconTextInfo.prefab");
        
        var obj = PrefabUtility.InstantiatePrefab(loadResource) as GameObject;
        if(Selection.activeGameObject != null)
            obj.transform.SetParent(Selection.activeGameObject.transform, false);        
    }
}
