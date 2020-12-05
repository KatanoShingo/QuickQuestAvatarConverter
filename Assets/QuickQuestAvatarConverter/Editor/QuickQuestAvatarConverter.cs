using UnityEditor;
using UnityEngine;

public class QuickQuestAvatarConverter : EditorWindow
{
    [MenuItem( "Window/Quest Avatar Setup" )]
    public static void Setup()
    {
        GameObject[] allGameObject = FindObjectsOfType( typeof( GameObject ) ) as GameObject[];

        foreach( var item in allGameObject )
        {
            GameObjectUtility.RemoveMonoBehavioursWithMissingScript( item );
            
            ChangeShader( item );
            DeleteBone( item );
        }
    }

    static void ChangeShader( GameObject go )
    {
        var renderer = go.GetComponent<Renderer>();

        if( renderer == null )
        {
            return;
        }

        var materials = renderer.materials;

        foreach( var material in materials )
        {
            material.shader = Shader.Find( "VRChat/Mobile/Toon Lit" );
        }
    }

    static void DeleteBone( GameObject go )
    {
        var dynamicBone = go.GetComponent( "DynamicBone" );
        var dynamicBoneCollider = go.GetComponent( "DynamicBoneCollider" );

        if( dynamicBone == null && dynamicBoneCollider == null )
        {
            return;
        }

        if( dynamicBone != null )
        {
            GameObject.DestroyImmediate( dynamicBone );
        }

        if( dynamicBoneCollider != null )
        {
            GameObject.DestroyImmediate( dynamicBoneCollider );
        }

        DeleteBone( go );
    }
}