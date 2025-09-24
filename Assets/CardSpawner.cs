using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardSpawner : MonoBehaviour, IEventReceiver<OnDestroyCardEvent>
{
    [SerializeField] private GameObject cardPrefab;
    [SerializeField] private Transform cardParent;


    public bool longGame;
    public List<CardJson> loadedCards;
    public List<CardJson> deck;
    public List<int> usedCards;
    
    private void Start()
    {
        EventBusHolder.instance.eventBus.Register(this);
        LoadCards();
        SpawnDeck();
        
        GameObject spawnedCard = Instantiate(cardPrefab, cardParent);
        spawnedCard.GetComponent<SwipeDetector>().Initialize(GetCardFromDeck());
    }

    public void OnEvent(OnDestroyCardEvent @event)
    {
        AddCardToDeckIfNeeded(@event.Properties);
        GameObject spawnedCard = Instantiate(cardPrefab, cardParent);
        spawnedCard.GetComponent<SwipeDetector>().Initialize(GetCardFromDeck());
    }

    private void AddCardToDeckIfNeeded(CardChoice cardChoice)
    {
        float val = Random.value;
        PosNegNeu posNegNeu;
        if (Random.value < cardChoice.pos.chance) // positive
        {
            posNegNeu = cardChoice.pos;
        }
        else if (Random.value < cardChoice.pos.chance + cardChoice.neu.chance) // neutral
        {
            posNegNeu = cardChoice.neu;
        }
        else // negative
        {
            posNegNeu = cardChoice.neg;
        }
        
        if (posNegNeu.id_next >= 0)
        {
            foreach (var cardL in loadedCards)
            {
                if (cardL.id == posNegNeu.id_next)
                {
                    AddCardToDeck(cardChoice.pos.moves_next, cardL);
                }
            }
        }
        else if (longGame)
        {
            int takes = 0;
            while (takes < 10000)
            {
                takes++;
                int v = Random.Range(0, loadedCards.Count);
                if (loadedCards[v].is_start && !usedCards.Contains(loadedCards[v].id))
                {
                    usedCards.Add(loadedCards[v].id);
                    AddCardToDeck(3, loadedCards[v]);
                    break;
                }
            }
        }

        EventBusHolder.instance.eventBus.Raise(new SetParametersEvent(posNegNeu.properties));
    }
    
    public void AddCardToDeck(int moves, CardJson cardToAdd)
    {
        List<CardJson> tempDeck = new List<CardJson>();
        
        for (int i = 0; i < Mathf.Min(moves, deck.Count); i++)
        {
            tempDeck.Add(deck[i]);
        }
        
        tempDeck.Add(cardToAdd);
        
        for (int i = Mathf.Min(moves, deck.Count); i < deck.Count; i++)
        { 
            tempDeck.Add(deck[i]);
        }

        deck.Clear();
        foreach (CardJson card in tempDeck)
        {
            deck.Add(card);
        }
    }
    
    private CardJson GetCardFromDeck()
    {
        if (deck.Count == 0)
        {
            return new CardJson()
            {
                name = "Empy card",
                description = "No deck, Game over",
                id = -100,
                image = "image_name",
                
            };
        }
        
        CardJson card = deck[0];
        deck.RemoveAt(0);
            
        return card;
    }

    public void LoadCards()
    {
        loadedCards = new List<CardJson>();
        TextAsset[] textAssets = Resources.LoadAll<TextAsset>("Cards");

        foreach (TextAsset textAsset in textAssets)
        {
            try
            {
                CardJson card = JsonUtility.FromJson<CardJson>(textAsset.text);
                loadedCards.Add(card);
            }
            catch (System.Exception e)
            {
                Debug.LogError($"Ошибка загрузки карты {textAsset.name}: {e.Message}");
            }
        }
    }

    private void SpawnDeck()
    {
        deck = new List<CardJson>();
        List<int> alreadyAdded = new List<int>();
        while (deck.Count < 3)
        { 
            int ind = Random.Range(0, loadedCards.Count);
            if (loadedCards[ind].is_start && !alreadyAdded.Contains(ind))
            {
                alreadyAdded.Add(ind);
                deck.Add(loadedCards[ind]);
                usedCards.Add(loadedCards[ind].id);
            }
        }
    }
}
