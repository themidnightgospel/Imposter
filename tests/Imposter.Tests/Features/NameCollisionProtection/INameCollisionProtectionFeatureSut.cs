using System;
using Imposter.Abstractions;

namespace Imposter.CodeGenerator.Tests.Features.NameCollisionProtection
{
    // Not need for test, if it builds then there is no collision
    [GenerateImposter(typeof(INameCollisionProtectionFeatureSut))]
    public interface INameCollisionProtectionFeatureSut
    {
        int CollisionWithReturns<TValue>(TValue value);
        
        int CollisionWithReturns(int value);
        
        int CollisionWithReturns(string result);
        
        int CollisionWithReturns(double matchingSetup);

        int CollisionWithThrows<TValue>(TValue ex);
        
        int CollisionWithThrows(Exception ex);

        int MethodWithSameNameDifferentSignature();
        
        int MethodWithSameNameDifferentSignature(int input);
        
        int MethodWithSameNameDifferentSignature<T>(T input);

        void DefaultInvocationSetup(int DefaultInvocationSetup);
        
        void _invocationSetups(int _invocationSetups);
        
        void it(int it);
        
        void invocationHistory(int invocationHistory);
        
        void nextSetup(int nextSetup);
        
        void GetNextSetup(int GetNextSetup);
        
        void exceptionGenerator(int exceptionGenerator);
        
        void callback(int callback);
        
        void resultGenerator(int resultGenerator);
        
        void argumentsCriteria(int argumentsCriteria);
        
        void TException(int TException);
    }
}