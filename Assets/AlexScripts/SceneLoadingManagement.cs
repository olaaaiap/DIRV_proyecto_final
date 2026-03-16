using DG.Tweening;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoadingManagement : Singleton<SceneLoadingManagement>
{
    private Dictionary<string, string> linkedScenes = new Dictionary<string, string>();

    [SerializeField] private Image fade;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        linkedScenes = new()
        {
            {"Tuto_ExteriorInstituto", "BañosInsti" },
            {"BañosInsti", "Bosque" },
            {"Bosque", "Arcade" },
            {"Arcade", "Hangar" },
            {"Hangar", "CastilloByers" },
            {"CastilloByers", "PasilloPortalOscuro" },
            {"PasilloPortalOscuro", "VecnaBossfight" }
        };
    }

    public void LoadNextScene()
    {
        print(linkedScenes[SceneManager.GetActiveScene().name]);
        DOTween.Sequence().Append(
        fade.DOFade(1f, 1f).OnComplete(() =>
        {
            SceneManager.LoadScene(linkedScenes[SceneManager.GetActiveScene().name]);
        }))
            .Append(fade.DOFade(0f, 1f));
    }
}

