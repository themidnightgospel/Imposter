using System;
using Imposter.Abstractions;
using Imposter.Tests.Features.ClassImposter;

[assembly: GenerateImposter(typeof(ClassWithProtectedOverrideableMembers))]

namespace Imposter.Tests.Features.ClassImposter
{
    public class ClassWithProtectedOverrideableMembers
    {
        protected virtual int ProtectedVirtualMethod(int value) => value * 2;

        protected virtual string ProtectedVirtualProperty { get; set; } = "protected-default";

        protected virtual int this[int index]
        {
            get => index;
            set { }
        }

        protected virtual event EventHandler? ProtectedVirtualEvent;

        public int InvokeProtectedMethod(int value) => ProtectedVirtualMethod(value);

        public string ReadProtectedProperty() => ProtectedVirtualProperty;

        public void WriteProtectedProperty(string value) => ProtectedVirtualProperty = value;

        public int ReadProtectedValue(int index) => this[index];

        public void WriteProtectedValue(int index, int value) => this[index] = value;

        public void SubscribeToProtectedEvent(EventHandler handler) => ProtectedVirtualEvent += handler;
    }
}
