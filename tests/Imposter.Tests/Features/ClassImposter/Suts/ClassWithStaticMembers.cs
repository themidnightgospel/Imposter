using System;
using Imposter.Abstractions;
using Imposter.Tests.Features.ClassImposter.Suts;

[assembly: GenerateImposter(typeof(ClassWithStaticMembers))]

namespace Imposter.Tests.Features.ClassImposter.Suts
{
    public class ClassWithStaticMembers
    {
        public static int StaticMethodCallCount { get; private set; }

        public static string StaticProperty { get; set; } = "static-default";

        public static event EventHandler? StaticEvent;

        public static int StaticMethod(int value)
        {
            StaticMethodCallCount++;
            return value * 3;
        }

        public static void ResetStatics()
        {
            StaticMethodCallCount = 0;
            StaticProperty = "static-default";
            StaticEvent = null;
        }

        public static void RaiseStaticEvent()
        {
            StaticEvent?.Invoke(null, EventArgs.Empty);
        }

        public void SubscribeToStaticEvent(EventHandler handler) => StaticEvent += handler;

        public void UnsubscribeFromStaticEvent(EventHandler handler) => StaticEvent -= handler;
    }
}
