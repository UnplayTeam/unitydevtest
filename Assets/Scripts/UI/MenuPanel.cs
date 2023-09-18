using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace RPG.UI {
  public class MenuPanel : MonoBehaviour {
    [SerializeField] private CanvasGroup _CanvasGroup;
    [SerializeField] private float _FadeDuration = 1.0f;

    public UnityEvent OnShow = new ();
    public UnityEvent OnShowComplete = new ();
    public UnityEvent OnHide = new ();
    public UnityEvent OnHideComplete = new ();
    
    public void Show (Action callback = null) {
      if (_CanvasGroup != null) {
        StartCoroutine (FadeInCoroutine ());
      }
      
      IEnumerator FadeInCoroutine () {
        OnShow.Invoke ();
        _CanvasGroup.interactable = false;
        _CanvasGroup.blocksRaycasts = false;

        float startAlpha = _CanvasGroup.alpha;
        for (float t = 0.0f; t < _FadeDuration; t += Time.deltaTime) {
          _CanvasGroup.alpha = Mathf.Lerp (startAlpha, 1f, t / _FadeDuration);
          yield return null;
        }

        _CanvasGroup.alpha = 1f;

        _CanvasGroup.interactable = true;
        _CanvasGroup.blocksRaycasts = true;
        OnShowComplete.Invoke ();
        callback?.Invoke ();
      }
    }

    public void Hide (Action callback = null) {
      if (_CanvasGroup != null) {
        StartCoroutine (FadeOutCoroutine ());
      }
      
      IEnumerator FadeOutCoroutine () {
        OnHide.Invoke ();
        _CanvasGroup.interactable = false;
        _CanvasGroup.blocksRaycasts = false;

        float startAlpha = _CanvasGroup.alpha;
        for (float t = 0.0f; t < _FadeDuration; t += Time.deltaTime) {
          _CanvasGroup.alpha = Mathf.Lerp (startAlpha, 0f, t / _FadeDuration);
          yield return null;
        }

        _CanvasGroup.alpha = 0f;
        OnHideComplete.Invoke ();
        callback?.Invoke ();
      }
    }
    
    // Internal
    private void Awake () {
      if (_CanvasGroup != null) {
        _CanvasGroup.alpha = 0f;
        _CanvasGroup.interactable = false;
        _CanvasGroup.blocksRaycasts = false;
      }
    }
  }
}