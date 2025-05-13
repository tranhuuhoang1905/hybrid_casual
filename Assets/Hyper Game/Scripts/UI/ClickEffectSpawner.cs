using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ClickEffectSpawner : MonoBehaviour
{
    
    public static ClickEffectSpawner Instance { get; private set; }
    public InputActionReference clickAction; // Kéo "Click" từ Input System vào Inspector
    [SerializeField] private GameObject clickEffect;

    private Camera mainCamera;
    

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void OnEnable()
    {
        if (clickAction != null)
        {
            clickAction.action.performed += OnClick;
            clickAction.action.Enable();
        }
    }

    private void OnDisable()
    {
        if (clickAction != null)
        {
            clickAction.action.performed -= OnClick;
            clickAction.action.Disable();
        }
    }

    private void OnClick(InputAction.CallbackContext context)
    {
        if (context.ReadValue<float>() != 1) return;
        if (clickEffect == null) return;
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, 10f));
        clickEffect.transform.position = worldPosition;
        RestartEffect();
    }

    private void RestartEffect()
    {
        ParticleSystem ps = clickEffect.GetComponent<ParticleSystem>();
        if (ps != null)
        {
            ps.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear); // Dừng hoàn toàn
            ps.Play(); // Chạy lại hiệu ứng
        }
    }

}
