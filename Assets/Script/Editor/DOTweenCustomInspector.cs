using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using DG.DOTweenEditor;
using DG.Tweening;


[CustomEditor(typeof(DotweenComponent))]
public class DOTweenCustomInspector : Editor
{



    DotweenComponent targetComponent;

    private void OnDisable()
    {
        if (targetComponent == null) { return; }
        DOTweenEditorPreview.Stop(true);
    }
    public void SetAnimation_Play()
    {
        if (targetComponent == null) { return; }

        DOTweenEditorPreview.Stop(true);

        targetComponent.InitSequence();
        var seq = targetComponent.GetSequence();

        DOTweenEditorPreview.PrepareTweenForPreview(seq);
        DOTweenEditorPreview.Start(() => {
            EditorUtility.SetDirty(targetComponent.gameObject);
        });

        if (seq != null) { seq.Play(); }


    }

    public void SetAnimation_Pause()
    {
        if (targetComponent == null) { return; }
        // var seq = targetComponent.GetSequence();
        // if (seq != null){ seq.Pause(); }
    }

    public void SetAnimation_Stop()
    {
        if (targetComponent == null) { return; }

        var seq = targetComponent.GetSequence();
        DOTweenEditorPreview.Stop(true);

        EditorUtility.SetDirty(targetComponent.gameObject);
    }


    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        targetComponent = (DotweenComponent)target;


        EditorGUILayout.Space();
        EditorGUILayout.Space();

        var styleText = new GUIStyle(GUI.skin.label);
        styleText.normal.textColor = Color.green;
        EditorGUILayout.LabelField("EDITOR ONLY", styleText);

        var style = new GUIStyle(GUI.skin.button);
        EditorGUILayout.BeginHorizontal();
        style.fixedHeight = 40;
        style.fixedWidth = 80;



        if (GUILayout.Button(new GUIContent(EditorGUIUtility.IconContent("PlayButton On").image), style)) { SetAnimation_Play(); }
        // if(GUILayout.Button(new GUIContent(EditorGUIUtility.IconContent("PauseButton On").image), style)) { SetAnimation_Pause(); }
        if (GUILayout.Button(new GUIContent(EditorGUIUtility.IconContent("animationdopesheetkeyframe").image), style)) { SetAnimation_Stop(); }


        EditorGUILayout.EndHorizontal();



    }
}
