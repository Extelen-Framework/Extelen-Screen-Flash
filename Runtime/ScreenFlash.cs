using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Extelen.UI {
		
	[RequireComponent(typeof(CanvasGroup), typeof(RectTransform))]
	public class ScreenFlash : MonoBehaviour {
				
		//Set Params
						
			//Non Static
			[Header("Flash References")]
			[SerializeField] private CanvasGroup m_canvasGroup = null;

			[Header("Flash Animation")]
			[SerializeField] private bool m_unscaledTime = true;
			public bool UnscaledDeltaTime { set => m_unscaledTime = value; }

			[SerializeField] private AnimationCurve m_activationSmoothness = 
				new AnimationCurve(new Keyframe[2] {

				new Keyframe(0, 0),
				new Keyframe(1, 1),
				});
			
			[SerializeField] private float m_inTime = 0.05f;
			public float InTime { set => m_inTime = value; }

			[SerializeField] private float m_holdTime = 0f;
			public float HoldTime { set => m_holdTime = value; }

			[SerializeField] private float m_outTime = 0.35f;
			public float OutTime { set => m_outTime = value; }
					
			private float m_flashValue = 0;
			private Coroutine m_flashRoutine = null;

		//Methods
		
			//Non Static
			private void OnValidate() {

				if (m_canvasGroup == null) TryGetComponent<CanvasGroup>(out m_canvasGroup);
				}

			public void ActiveScreenFlash() => ActiveScreenFlash(m_inTime, m_holdTime, m_outTime);
			public void ActiveScreenFlash(float outTime) => ActiveScreenFlash(m_inTime, m_holdTime, outTime);
			public void ActiveScreenFlash(float inTime, float holdTime, float outTime) => ActiveRoutine(inTime, holdTime, outTime);

			private void ActiveRoutine(float inTime, float holdTime, float outTime) {

				gameObject.SetActive(true);

				if (m_flashRoutine != null) StopCoroutine(m_flashRoutine);
				m_flashRoutine = StartCoroutine(FlashRoutine(inTime, holdTime, outTime));
				}
			private void SetFlashIntensity(float value) {

				m_flashValue = Mathf.Clamp01(value);
				m_canvasGroup.alpha = m_flashValue;
				}
			
		//Coroutines
		private IEnumerator FlashRoutine(float inTime, float holdTime, float outTime) {
			
			float m_startValue = m_flashValue;
			float m_finalValue = 1f;

			for(float i = 0; i < inTime; i += m_unscaledTime ? Time.unscaledDeltaTime : Time.deltaTime) {
				
				SetFlashIntensity(Mathf.Lerp(m_startValue, m_finalValue, m_activationSmoothness.Evaluate(i / inTime)));
				yield return null;
				}

			SetFlashIntensity(m_finalValue);

			yield return new WaitForSeconds(holdTime);

			m_startValue = 1;
			m_finalValue = 0;
			
			for(float i = 0; i < outTime; i += m_unscaledTime ? Time.unscaledDeltaTime : Time.deltaTime) {

				SetFlashIntensity(Mathf.Lerp(m_startValue, m_finalValue, m_activationSmoothness.Evaluate(i / outTime)));
				yield return null;
				}

			SetFlashIntensity(m_finalValue);
			gameObject.SetActive(false);
			}
		}
	}