using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxTrigger : MonoBehaviour
{
    [SerializeField] private Material skybox_dark;
    [SerializeField] private Material skybox_light;

    [SerializeField] private Light dirLight;

    [SerializeField] private GameObject[] railPrefabs;

    void Start()
    {
        RenderSettings.skybox = skybox_dark;
        dirLight.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        RenderSettings.skybox = skybox_light;
        dirLight.gameObject.SetActive(true);

        foreach (GameObject railPrefab in railPrefabs)
        {
            railPrefab.SetActive(true);
        }
    }
}
