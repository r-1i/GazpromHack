public readonly struct OnDestroyCardEvent : IEvent
{
    public readonly CardJsonProperties Properties;
    public OnDestroyCardEvent(CardJsonProperties properties)
    {
        Properties = new CardJsonProperties();
        Properties = properties;
    }
}
