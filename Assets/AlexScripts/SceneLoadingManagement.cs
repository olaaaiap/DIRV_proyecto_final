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

    private float timer;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        linkedScenes = new()
        {
            {"Tuto_ExteriorInstituto", "BañosInsti" },
            {"BañosInsti", "BañosInsti_Upside_Down" },
            {"BañosInsti_Upside_Down", "CasaWill2" },
            {"CasaWill2", "Bosque" },
            {"Bosque", "Arcade" },
            {"Arcade", "SotanoArcade"},
            {"SotanoArcade", "Hangar" },
            {"Hangar", "CastilloByers" },
            {"CastilloByers", "Tuto_ExteriorInstituto" }
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

