using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MainMenuView : MonoBehaviour
{
    [SerializeField] private Button lightSceneButton;
    [SerializeField] private Button heavySceneButton;

    [SerializeField] private SceneLoader sceneLoader;
    
    public event UnityAction loadLightScene;

    private void Awake()
    {
        loadLightScene += TestFunction;
        lightSceneButton.onClick.AddListener(loadLightScene);
    }

    private void TestFunction()
    {
        sceneLoader.LoadScene().Forget();
    }
}
