using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Extelen.UI;

namespace Extelen.Editor {
		
	[CustomEditor(typeof(ScreenFlash))]
	public class ScreenFlashEditor : UnityEditor.Editor {
			
		//Set Params
		private SerializedProperty m_canvasGroup = null;
		private SerializedProperty m_unscaledTime = null;
		private SerializedProperty m_activationSmoothness = null;
		private SerializedProperty m_inTime = null;
		private SerializedProperty m_holdTime = null;
		private SerializedProperty m_outTime = null;
					
		//Methods
		private void OnEnable() {

			m_canvasGroup = serializedObject.FindProperty("m_canvasGroup");
			m_unscaledTime = serializedObject.FindProperty("m_unscaledTime");
			m_activationSmoothness = serializedObject.FindProperty("m_activationSmoothness");

			m_inTime = serializedObject.FindProperty("m_inTime");
			m_holdTime = serializedObject.FindProperty("m_holdTime");
			m_outTime = serializedObject.FindProperty("m_outTime");
			}

		public override void OnInspectorGUI() {
		
			serializedObject.Update();

			EditorGUILayout.PropertyField(m_canvasGroup);
			EditorGUILayout.PropertyField(m_unscaledTime);
			EditorGUILayout.PropertyField(m_activationSmoothness);
			EditorGUILayout.PropertyField(m_inTime);
			EditorGUILayout.PropertyField(m_holdTime);
			EditorGUILayout.PropertyField(m_outTime);
			
			serializedObject.ApplyModifiedProperties();
			}
		}
	}