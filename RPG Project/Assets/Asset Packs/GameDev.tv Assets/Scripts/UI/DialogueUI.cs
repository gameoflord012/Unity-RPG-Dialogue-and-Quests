using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Dialogue;
using TMPro;
using UnityEngine.UI;

namespace RPG.UI
{
    public class DialogueUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI AIText;
        [SerializeField] Button nextButton;
        [SerializeField] Transform choiceRoot;
        [SerializeField] GameObject choicePrefab;
        [SerializeField] GameObject AIResponse;

        private PlayerConversant playerConversant;

        private void Awake()
        {
            playerConversant = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerConversant>();            
        }

        private void Start()
        {
            nextButton.onClick.AddListener(Next);
        }

        private void OnEnable()
        {
            playerConversant.OnConversationUpdated += UpdateUI;
        }

        private void OnDisable()
        {
            playerConversant.OnConversationUpdated -= UpdateUI;
        }

        private void UpdateUI()
        {
            if (!playerConversant.IsActive()) return;

            AIResponse.SetActive(!playerConversant.IsChoosing());
            choiceRoot.gameObject.SetActive(playerConversant.IsChoosing());            

            if(playerConversant.IsChoosing())
            {
                BuildChoiceList();
            }
            else
            {
                AIText.text = playerConversant.GetText();
                nextButton.gameObject.SetActive(playerConversant.HasNext());
            }

        }

        private void BuildChoiceList()
        {
            DestroyDefaultButton();

            foreach (DialogueNode choiceNode in playerConversant.GetChoiceNode())
            {
                GameObject choiceInstance = Instantiate(choicePrefab, choiceRoot);
                TextMeshProUGUI text = choiceInstance.GetComponentInChildren<TextMeshProUGUI>();
                text.text = choiceNode.GetText();
                Button button = choiceInstance.GetComponentInChildren<Button>();
                button.onClick.AddListener(() => 
                {
                    playerConversant.SelectChoice(choiceNode);
                    Next();
                });
            }
        }

        private void DestroyDefaultButton()
        {
            foreach (Transform item in choiceRoot)
            {
                Destroy(item.gameObject);
            }
        }

        void Next()
        {
            playerConversant.Next();
        }
    }
}