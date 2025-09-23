using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardSpawner : MonoBehaviour, IEventReceiver<OnDestroyCardEvent>
{
    [SerializeField] private GameObject cardPrefab;
    [SerializeField] private Transform cardParent;

    public List<CardJson> loadedCards;
    public List<CardJson> availableCards;
    
    private void Start()
    {
        EventBusHolder.instance.eventBus.Register(this);
        LoadCards();
        
        GameObject spawnedCard = Instantiate(cardPrefab, cardParent);
        spawnedCard.GetComponent<SwipeDetector>().Initialize(GetCardFromList());
    }

    public void OnEvent(OnDestroyCardEvent @event)
    {
        GameObject spawnedCard = Instantiate(cardPrefab, cardParent);
        spawnedCard.GetComponent<SwipeDetector>().Initialize(GetCardFromList());
    }

    private CardJson GetCardFromList()
    {
        if (availableCards.Count == 0)
        {
            foreach (CardJson loadedCard in loadedCards)
            {
                availableCards.Add(loadedCard);
            }
        }
        
        int index = Random.Range(0, availableCards.Count);
        CardJson card = availableCards[index];
        availableCards.RemoveAt(index);
            
        return card;
    }

    public void LoadCards()
    {
        loadedCards = new List<CardJson>();
        availableCards = new List<CardJson>();
        TextAsset[] textAssets = Resources.LoadAll<TextAsset>("Cards");

        foreach (TextAsset textAsset in textAssets)
        {
            try
            {
                CardJson card = JsonUtility.FromJson<CardJson>(textAsset.text);
                loadedCards.Add(card);
                availableCards.Add(card);
            }
            catch (System.Exception e)
            {
                Debug.LogError($"Ошибка загрузки карты {textAsset.name}: {e.Message}");
            }

        }
    }
}
