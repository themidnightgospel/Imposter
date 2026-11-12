namespace Imposter.Abstractions;

/// <summary>A recorded event subscription or unsubscription.</summary>
/// <typeparam name="THandler">The event delegate type.</typeparam>
public sealed class EventSubscriptionInvocation<THandler> : InvocationRecord
{
    private readonly InvocationBuffer<THandler>.Entry _entry;

    /// <summary>Materializes a completed event interaction from compact history storage.</summary>
    /// <param name="member">The event name.</param>
    /// <param name="entry">The completed entry and its shared verification coverage.</param>
    /// <param name="isSubscription">Whether the handler was added.</param>
    private EventSubscriptionInvocation(
        string member,
        InvocationBuffer<THandler>.Entry entry,
        bool isSubscription
    )
        : this(member, entry.Value, isSubscription, entry.Sequence, entry.Exception)
    {
        _entry = entry;
    }

    /// <summary>Creates a completed event interaction.</summary>
    /// <param name="member">The event name.</param>
    /// <param name="handler">The handler supplied to the event accessor.</param>
    /// <param name="isSubscription">Whether the handler was added.</param>
    /// <param name="sequence">The entry stamp.</param>
    /// <param name="exception">The exception thrown by the accessor.</param>
    private EventSubscriptionInvocation(
        string member,
        THandler handler,
        bool isSubscription,
        long sequence,
        Exception? exception
    )
        : base(sequence, exception)
    {
        Member = member;
        Handler = handler;
        IsSubscription = isSubscription;
    }

    /// <inheritdoc />
    public override string Member { get; }

    /// <summary>Gets the supplied handler.</summary>
    public THandler Handler { get; }

    /// <summary>Gets whether this invocation added the handler.</summary>
    public bool IsSubscription { get; }

    /// <inheritdoc />
    protected override bool IsVerified => _entry.IsValid ? _entry.IsVerified : base.IsVerified;

    /// <inheritdoc />
    protected override void MarkVerified()
    {
        if (_entry.IsValid)
            _entry.MarkVerified();
        else
            base.MarkVerified();
    }

    /// <summary>Materializes an entry-ordered snapshot without changing coverage.</summary>
    /// <param name="history">The event action history.</param>
    /// <param name="member">The event name.</param>
    /// <param name="isSubscription">Whether these interactions added handlers.</param>
    public static IReadOnlyList<EventSubscriptionInvocation<THandler>> Snapshot(
        InvocationBuffer<THandler>? history,
        string member,
        bool isSubscription
    ) => Snapshot(history, member, isSubscription, true);

    /// <summary>Materializes interactions, optionally leaving ordering to an enclosing aggregate.</summary>
    /// <param name="history">The event action history.</param>
    /// <param name="member">The event name.</param>
    /// <param name="isSubscription">Whether these interactions added handlers.</param>
    /// <param name="orderBySequence">Whether to order this snapshot by entry stamp.</param>
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    public static IReadOnlyList<EventSubscriptionInvocation<THandler>> Snapshot(
        InvocationBuffer<THandler>? history,
        string member,
        bool isSubscription,
        bool orderBySequence
    )
    {
        var result = new List<EventSubscriptionInvocation<THandler>>();
        if (history is not null)
            foreach (var entry in history)
                result.Add(
                    new EventSubscriptionInvocation<THandler>(member, entry, isSubscription)
                );
        return InvocationSnapshots.Order(result, orderBySequence);
    }

    /// <inheritdoc />
    public override string ToString() =>
        Member
        + (IsSubscription ? " subscribed " : " unsubscribed ")
        + Handler
        + (Exception is null ? string.Empty : " threw " + Exception);
}
