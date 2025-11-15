using System;
using Imposter.Abstractions;
using Imposter.Tests.Features.ClassImposter.Suts;

[assembly: GenerateImposter(typeof(ClassWithProtectedOverrideableMembers))]

namespace Imposter.Tests.Features.ClassImposter.Suts
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

#pragma warning disable CS0067
        protected virtual event EventHandler? ProtectedVirtualEvent;
#pragma warning disable CS0067

        public virtual int InvokeProtectedMethod(int value) => ProtectedVirtualMethod(value);

        public virtual string ReadProtectedProperty() => ProtectedVirtualProperty;

        public virtual void WriteProtectedProperty(string value) =>
            ProtectedVirtualProperty = value;

        public virtual int ReadProtectedValue(int index) => this[index];

        public virtual void WriteProtectedValue(int index, int value) => this[index] = value;

        public virtual void SubscribeToProtectedEvent(EventHandler handler) =>
            ProtectedVirtualEvent += handler;
    }
}
