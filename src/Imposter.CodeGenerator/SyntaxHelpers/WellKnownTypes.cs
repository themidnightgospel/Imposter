using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.SyntaxHelpers;

internal static class WellKnownTypes
{
    internal static TypeSyntax Void = PredefinedType(Token(SyntaxKind.VoidKeyword));
        
    internal static class System
    {
        internal static NameSyntax Namespace = IdentifierName("System");

        internal static TypeSyntax Exception = QualifiedName(
            Namespace,
            IdentifierName("Exception")
        );

        internal static TypeSyntax Action = QualifiedName(
            Namespace,
            IdentifierName("Action")
        );

        internal static TypeSyntax ActionOfT(TypeSyntax typeArgument) => QualifiedName(
            Namespace,
            GenericName(
                Identifier("Action"),
                TypeArgumentList(SingletonSeparatedList(typeArgument))
            )
        );

        internal static TypeSyntax FuncOfT(TypeSyntax returnType) => QualifiedName(
            Namespace,
            GenericName(
                Identifier("Func"),
                TypeArgumentList(SingletonSeparatedList(returnType))
            )
        );

        public static class Threading
        {
            internal static NameSyntax Namespace = QualifiedName(WellKnownTypes.System.Namespace, IdentifierName("Threading"));

            public static class Tasks
            {
                internal static NameSyntax Namespace = QualifiedName(WellKnownTypes.System.Threading.Namespace, IdentifierName("Tasks"));

                internal static TypeSyntax Task = QualifiedName(
                    Namespace,
                    IdentifierName("Task")
                );

                internal static TypeSyntax TaskOfT(TypeSyntax typeArgument) => QualifiedName(
                    Namespace,
                    GenericName(
                        Identifier("Task"),
                        TypeArgumentList(SingletonSeparatedList(typeArgument))
                    )
                );

                internal static TypeSyntax ValueTask = QualifiedName(
                    Namespace,
                    IdentifierName("ValueTask")
                );

                internal static TypeSyntax ValueTaskOfT(TypeSyntax typeArgument) => QualifiedName(
                    Namespace,
                    GenericName(
                        Identifier("ValueTask"),
                        TypeArgumentList(SingletonSeparatedList(typeArgument))
                    )
                );
            }
        }
    }

    internal static class Imposter
    {
        internal static NameSyntax Namespace = IdentifierName("Imposter");

        internal static class Abstractions
        {
            internal static NameSyntax Namespace = QualifiedName(WellKnownTypes.Imposter.Namespace, IdentifierName("Abstractions"));

            internal static NameSyntax IHaveImposterInstance(TypeSyntax instanceType) =>
                QualifiedName(
                    Namespace,
                    GenericName(
                        Identifier(nameof(IHaveImposterInstance)),
                        TypeArgumentList(
                            SingletonSeparatedList(instanceType)
                        )
                    )
                );

            internal static NameSyntax VerificationFailedException => QualifiedName(Namespace, IdentifierName("VerificationFailedException"));

            internal static NameSyntax Count => QualifiedName(Namespace, IdentifierName("Count"));

            internal static NameSyntax TypeCaster => QualifiedName(Namespace, IdentifierName("TypeCaster"));

            internal static NameSyntax OutArg(SyntaxNode type)
                => QualifiedName(Namespace, GenericName(Identifier(nameof(OutArg)), TypeArgumentList(SingletonSeparatedList(type))));

            internal static NameSyntax Arg(SyntaxNode type)
                => QualifiedName(Namespace, GenericName(Identifier(nameof(Arg)), TypeArgumentList(SingletonSeparatedList(type))));
        }
    }
}