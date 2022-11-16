using UnityEditor;
using UnityEngine;

namespace Voxell.Inspector
{
    [CustomPropertyDrawer(typeof(StreamingAssetFilePathAttribute))]
    public class StreamingAssetFilePathDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect rect, SerializedProperty property, GUIContent label)
        {
            if (property.propertyType == SerializedPropertyType.String)
            {
                rect.size = new Vector2(rect.size.x, 20.0f);
                property.isExpanded = EditorGUI.Foldout(rect, property.isExpanded, label, true, VXEditorStyles.SubFoldoutStyle);
                if (property.isExpanded)
                {
                    rect.y += 20.0f;
                    if (GUI.Button(new Rect(rect.position, new Vector2(20.0f, 20.0f)), EditorGUIUtility.IconContent("Folder Icon").image))
                    {
                        string filePath = EditorUtility.OpenFilePanel("Asset File", Application.streamingAssetsPath, "");
                        if (filePath != "") property.stringValue = filePath.Substring(Application.streamingAssetsPath.Length + 1);
                        property.serializedObject.ApplyModifiedProperties();
                        GUIUtility.ExitGUI();
                    }
                    Rect assetLabelRect = new Rect(new Vector2(rect.x + 22.0f, rect.y), new Vector2(116.0f, rect.size.y));
                    EditorGUI.LabelField(assetLabelRect, "StreamingAssets/");
                    Rect filePathRect = new Rect(new Vector2(assetLabelRect.x + assetLabelRect.size.x, rect.y), new Vector2(rect.size.x - assetLabelRect.size.x - 22.0f, rect.size.y));
                    GUI.enabled = false;
                    property.stringValue = EditorGUI.TextField(filePathRect, property.stringValue);
                    GUI.enabled = true;
                }
            }
            else EditorGUI.LabelField(rect, label.text, "Use [StreamingAssetFilePath] with strings.");
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
          => property.isExpanded ? 40.0f : 20.0f;
    }

    [CustomPropertyDrawer(typeof(StreamingAssetFolderPathAttribute))]
    public class StreamingAssetFolderPathPathDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect rect, SerializedProperty property, GUIContent label)
        {
            if (property.propertyType == SerializedPropertyType.String)
            {
                rect.size = new Vector2(rect.size.x, 20.0f);
                property.isExpanded = EditorGUI.Foldout(rect, property.isExpanded, label, true, VXEditorStyles.SubFoldoutStyle);
                if (property.isExpanded)
                {
                    rect.y += 20.0f;
                    if (GUI.Button(new Rect(rect.position, new Vector2(20.0f, 20.0f)), EditorGUIUtility.IconContent("Folder Icon").image))
                    {
                        string filePath = EditorUtility.OpenFolderPanel("Asset Folder", Application.streamingAssetsPath, "");
                        if (filePath != "") property.stringValue = filePath.Substring(Application.streamingAssetsPath.Length + 1);
                        property.serializedObject.ApplyModifiedProperties();
                        GUIUtility.ExitGUI();
                    }
                    Rect assetLabelRect = new Rect(new Vector2(rect.x + 22.0f, rect.y), new Vector2(116.0f, rect.size.y));
                    EditorGUI.LabelField(assetLabelRect, "StreamingAssets/");
                    Rect filePathRect = new Rect(new Vector2(assetLabelRect.x + assetLabelRect.size.x, rect.y), new Vector2(rect.size.x - assetLabelRect.size.x - 22.0f, rect.size.y));
                    GUI.enabled = false;
                    property.stringValue = EditorGUI.TextField(filePathRect, property.stringValue);
                    GUI.enabled = true;
                }
            }
            else EditorGUI.LabelField(rect, label.text, "Use [StreamingAssetFilePath] with strings.");
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
          => property.isExpanded ? 40.0f : 20.0f;
    }
}