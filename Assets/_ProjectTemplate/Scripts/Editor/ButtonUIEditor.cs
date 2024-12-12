using _ProjectTemplate.Scripts.Others;
using UnityEditor;
using UnityEditor.UI;

namespace _ProjectTemplate.Scripts.Editor
{
    [CustomEditor(typeof(ButtonUI))]
    public class ButtonUIEditor : ButtonEditor
    {
        private SerializedProperty _getImage;
        private SerializedProperty _getText;

        private SerializedProperty _image;
        private SerializedProperty _txtButton;

        private SerializedProperty useAudio;
        private SerializedProperty useScaleEffect;

        protected override void OnEnable()
        {
            base.OnEnable();

            _getImage = serializedObject.FindProperty("_getImage");
            _getText = serializedObject.FindProperty("_getText");

            _image = serializedObject.FindProperty("_image");
            _txtButton = serializedObject.FindProperty("_txtButton");

            useAudio = serializedObject.FindProperty("useAudio");
            useScaleEffect = serializedObject.FindProperty("useScaleEffect");
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            EditorGUILayout.Space();

            serializedObject.Update();

            EditorGUILayout.PropertyField(_getImage);
            EditorGUILayout.PropertyField(_getText);

            EditorGUILayout.PropertyField(_image);
            EditorGUILayout.PropertyField(_txtButton);

            EditorGUILayout.PropertyField(useAudio);
            EditorGUILayout.PropertyField(useScaleEffect);


            serializedObject.ApplyModifiedProperties();
        }
    }
}