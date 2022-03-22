using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

public class MeowObjectManager : MonoBehaviour
{
    public Transform spawnPoint;
    [SerializeField]
    private List<MeowObject> defaultMeow;

    private Queue<BaseMeow> activeMeows;
    public Queue<BaseMeow> ActiveMeows { get => activeMeows; }

    public BaseMeow ActiveMeow { get => activeMeows.First(); }

    public CinemachineVirtualCamera virtualCamera;

    private static MeowObjectManager _instance;
    public static MeowObjectManager Instance { get => _instance; }

    public GameObject DeathMenu;

    private void Awake()
    {
        _instance = this;
        activeMeows = new Queue<BaseMeow>();

        List<MeowObject> selection;
        if (SelectionScript.SelectedCards == null)
        {
            selection = defaultMeow;
        } else
        {
            selection = SelectionScript.SelectedCards.Select(c => c.Meow).ToList();
        }
        foreach (var meow in selection)
        {
            var spawned = Instantiate(meow.prefab, spawnPoint.position, Quaternion.identity);
            spawned.name = meow.prefabName;
            spawned.SetActive(activeMeows.Count == 0);

            activeMeows.Enqueue(spawned.GetComponent<BaseMeow>());
        }
        // Set camera to follow the currently active meow
        UpdateCameraFollowing();
    }

    private void Update()
    {
        if(ActiveMeow.Health <= 0)
        {
            DeathMenu.SetActive(true);
        }
    }

    public void Switch()
    {
        var oldActiveMeow = activeMeows.Dequeue();
        // Deactivate the popped meow
        oldActiveMeow.gameObject.SetActive(false);
        // Place to the bottom of the queue
        activeMeows.Enqueue(oldActiveMeow);
        var newlyActivatedMeow = ActiveMeow;
        // Set position
        newlyActivatedMeow.transform.position = oldActiveMeow.transform.position;
        // Activate the newly activated meow
        newlyActivatedMeow.gameObject.SetActive(true);
        // Make the camera the new active meow
        UpdateCameraFollowing();
    }

    public void UpdateCameraFollowing()
    {
        if (virtualCamera == null) return;
        virtualCamera.m_Follow = ActiveMeow.transform;
    }
}


