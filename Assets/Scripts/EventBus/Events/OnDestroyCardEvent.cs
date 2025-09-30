public readonly struct OnDestroyCardEvent : IEvent
{
    public readonly CardChoice Properties;
    public OnDestroyCardEvent(CardChoice properties)
    {
        Properties = properties;
    }
}

public readonly struct SetParametersEvent : IEvent
{
    public readonly CardJsonProperties Properties;
    public SetParametersEvent(CardJsonProperties properties)
    {
        Properties = properties;
    }
}