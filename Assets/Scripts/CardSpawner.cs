using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CardSpawner : MonoBehaviour, IEventReceiver<OnDestroyCardEvent>
{
    [SerializeField] private GameObject cardPrefab;
    [SerializeField] private Transform cardParent;

    public bool longGame;
    public List<CardJson> loadedCards;
    public List<CardJson> deck;
    public List<int> usedCards;
    
    // Закомментированы поля шансов, так как логика шансов отключена из за пакостей дипсика в генерации json'ов карточек
    /*
    [SerializeField, Range(0f, 1f)] private float chance0;
    [SerializeField, Range(0f, 1f)] private float chance1;
    [SerializeField, Range(0f, 1f)] private float chance2;
    [SerializeField, Range(0f, 1f)] private float chance3;
    */
    
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
        switch (@event.Properties.pos.id_next)
        {
            case -122:
                Application.OpenURL("https://www.gazprombank.ru/personal/page/investment-life-insurance/");
                SceneManager.LoadScene(0);
                break;
            case -300:
                SceneManager.LoadScene(0);
                break;
        }
        
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
                image = "no_image",
                
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
        
        loadedCards = Shuffle(loadedCards);
    }
    
    public List<T> Shuffle<T>(List<T> list)
    {
        System.Random rnd = new System.Random();
        List<T> shuffledList = new List<T>(list);
        int n = shuffledList.Count;
        
        while (n > 1)
        {
            n--;
            int k = rnd.Next(n + 1);
            T value = shuffledList[k];
            shuffledList[k] = shuffledList[n];
            shuffledList[n] = value;
        }
        
        return shuffledList;
    }



    private void SpawnDeck()
    {
        deck = new List<CardJson>();
        usedCards = new List<int>();
        List<int> alreadyAdded = new List<int>();

        // Закомментирована логика шансов спавна по type
        /*
        Dictionary<int, float> typeChances = new Dictionary<int, float>
        {
            { 0, chance0 },
            { 1, chance1 },
            { 2, chance2 },
            { 3, chance3 }
        };

        foreach (var loadedCard in loadedCards)
        {
            if (loadedCard.is_start && typeChances.ContainsKey(loadedCard.type))
            {
                float chance = typeChances[loadedCard.type];
                if (Random.value <= chance && !alreadyAdded.Contains(loadedCard.id))
                {
                    alreadyAdded.Add(loadedCard.id);
                    deck.Add(loadedCard);
                    usedCards.Add(loadedCard.id);
                }
            }
        }
        */

        // Спавним любые стартовые карты без учета шансов
        foreach (var loadedCard in loadedCards)
        {
            if (loadedCard.is_start && !alreadyAdded.Contains(loadedCard.id))
            {
                alreadyAdded.Add(loadedCard.id);
                deck.Add(loadedCard);
                usedCards.Add(loadedCard.id);
            }
        }

        // Удален цикл while, так как теперь все стартовые карты добавляются сразу
        /*
        while (deck.Count < 3)
        {
            var candidates = loadedCards.Where(card => card.is_start && !alreadyAdded.Contains(card.id)).ToList();
            if (candidates.Count == 0)
                break;

            int ind = Random.Range(0, candidates.Count);
            var cardToAdd = candidates[ind];
            alreadyAdded.Add(cardToAdd.id);
            deck.Add(cardToAdd);
            usedCards.Add(cardToAdd.id);
        }
        */
    }
    

}
