﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLerpGrayscaleRatio : MonoBehaviour {
    private Shader m_CurShader;
    private Material m_CurMaterial;
    private string m_ShaderName = "Hidden/LerpGrayScaleImageEffectShader";
    [SerializeField]
    [Range(0.0f, 1.0f), Tooltip("grayscale ratio")]
    private float m_GrayscaleRatio = 0.0f;
    #region MaterialGetAndSet
    Material material
    {
        get
        {
            if (m_CurMaterial == null)
            {
                m_CurMaterial = new Material(m_CurShader);
                m_CurMaterial.hideFlags = HideFlags.HideAndDontSave;
            }
            return m_CurMaterial;
        }
    }
    #endregion

    public float grayscaleRatio
    {
        set
        {
            m_GrayscaleRatio = value;
        }
    }
    // Use this for initialization
    void Start () {
        m_CurShader = Shader.Find(m_ShaderName);
        if (!SystemInfo.supportsImageEffects)
        {
            enabled = false;
            return;
        }
    }

    void OnValidate()
    {
    }

    void OnRenderImage(RenderTexture sourceTexture, RenderTexture destTexture)
    {
        if (m_CurShader != null)
        {
            material.SetFloat("_GrayscaleRatio", m_GrayscaleRatio);
            Graphics.Blit(sourceTexture, destTexture, material);
        }
        else
        {
            Graphics.Blit(sourceTexture, destTexture);
        }
    }

    void OnDisable()
    {
        if (m_CurMaterial)
        {
            DestroyImmediate(m_CurMaterial);
        }

    }
}
