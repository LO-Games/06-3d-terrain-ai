using UnityEngine;
using UnityEngine.UI;
/**
 * This component animates a door depending on whether a player has a keycard.
 */
public class Card_Door : MonoBehaviour {
    private Animator _animator;
    public string promptText = "You need a card to open this door.";

    private GameObject doorPrompt;

    void Start()  {
        _animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other){
        if(other.tag == "Player"){
            if(other.GetComponent<PlayerInventory>().HasCard()){
                _animator.SetBool("character_nearby", true);
            }
            else{
                CreateDoorPrompt();
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.tag == "Player"){
            _animator.SetBool("character_nearby", false);
            if(!other.GetComponent<PlayerInventory>().HasCard()){
                DestroyDoorPrompt();
            }
        }
    }

    private void CreateDoorPrompt()
    {
        // Create a canvas if one doesn't exist
        if (GameObject.Find("Canvas") == null)
        {
            GameObject canvasObj = new GameObject("Canvas");
            canvasObj.AddComponent<Canvas>();
            canvasObj.AddComponent<CanvasScaler>();
            canvasObj.AddComponent<GraphicRaycaster>();
            canvasObj.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
        }

        // Create the door prompt text UI element
        doorPrompt = new GameObject("DoorPrompt");
        doorPrompt.transform.SetParent(GameObject.Find("Canvas").transform);

        RectTransform rectTransform = doorPrompt.AddComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(400f, 100f);
        rectTransform.localPosition = new Vector3(0f, 0f, 0f);

        Text textComponent = doorPrompt.AddComponent<Text>();
        textComponent.text = promptText;
        textComponent.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        textComponent.fontSize = 24;
        textComponent.color = Color.black;
        textComponent.alignment = TextAnchor.LowerCenter;
    }

    private void DestroyDoorPrompt()
    {
        // Destroy the door prompt UI
        if (doorPrompt != null)
        {
            Destroy(doorPrompt);
        }
    }
}
