namespace Imposter.CodeGenerator.Features.EventImpersonation.Metadata;

// The raise methods declare these locals next to the delegate's parameters, so the names avoid those parameters.
internal readonly struct EventRaiseLocalNames
{
    internal readonly string Callback;

    internal readonly string Handler;

    internal readonly string Task;

    internal readonly string PendingTasks;

    internal EventRaiseLocalNames(in ImposterEventCoreMetadata core)
    {
        var names = core.CreateParameterNameSet();
        Callback = names.Use("callback");
        Handler = names.Use("handler");
        Task = names.Use("task");
        PendingTasks = names.Use("pendingTasks");
    }
}
