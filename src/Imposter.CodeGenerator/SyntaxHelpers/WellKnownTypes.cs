using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.SyntaxHelpers;

internal static class WellKnownTypes
{
    internal static readonly TypeSyntax Void = PredefinedType(Token(SyntaxKind.VoidKeyword));
    
    internal static readonly TypeSyntax Int = PredefinedType(Token(SyntaxKind.IntKeyword));
    
    internal static readonly TypeSyntax Bool = PredefinedType(Token(SyntaxKind.BoolKeyword));
        
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
        
        internal static TypeSyntax Tuple(TypeSyntax item1Type, TypeSyntax item2Type) => QualifiedName(
            Namespace,
            GenericName(
                Identifier("Tuple"),
                TypeArgumentList(
                    SeparatedList<TypeSyntax>(new SyntaxNodeOrToken[]
                    {
                        item1Type,
                        Token(SyntaxKind.CommaToken),
                        item2Type
                    })
                )
            )
        );

        public static class Collections
        {
            internal static NameSyntax Namespace = QualifiedName(WellKnownTypes.System.Namespace, IdentifierName("Collections"));

            public static class Concurrent
            {
                internal static NameSyntax Namespace = QualifiedName(WellKnownTypes.System.Collections.Namespace, IdentifierName("Concurrent"));

                internal static TypeSyntax ConcurrentQueue(TypeSyntax typeArgument) => QualifiedName(
                    Namespace,
                    GenericName(
                        Identifier("ConcurrentQueue"),
                        TypeArgumentList(SingletonSeparatedList(typeArgument))
                    )
                );
                
                internal static TypeSyntax ConcurrentStack(TypeSyntax typeArgument) => QualifiedName(
                    Namespace,
                    GenericName(
                        Identifier("ConcurrentStack"),
                        TypeArgumentList(SingletonSeparatedList(typeArgument))
                    )
                );

                internal static TypeSyntax ConcurrentBag(TypeSyntax typeArgument) => QualifiedName(
                    Namespace,
                    GenericName(
                        Identifier("ConcurrentBag"),
                        TypeArgumentList(SingletonSeparatedList(typeArgument))
                    )
                );

                internal static TypeSyntax ConcurrentDictionary(TypeSyntax keyType, TypeSyntax valueType) => QualifiedName(
                    Namespace,
                    GenericName(
                        Identifier("ConcurrentDictionary"),
                        TypeArgumentList(
                            SeparatedList<TypeSyntax>(new SyntaxNodeOrToken[]
                            {
                                keyType,
                                Token(SyntaxKind.CommaToken),
                                valueType
                            })
                        )
                    )
                );
            }

            public static class Generic
            {
                internal static NameSyntax Namespace = QualifiedName(WellKnownTypes.System.Collections.Namespace, IdentifierName("Generic"));

                internal static TypeSyntax Dictionary(TypeSyntax keyType, TypeSyntax valueType) => QualifiedName(
                    Namespace,
                    GenericName(
                        Identifier("Dictionary"),
                        TypeArgumentList(
                            SeparatedList<TypeSyntax>(new SyntaxNodeOrToken[]
                            {
                                keyType,
                                Token(SyntaxKind.CommaToken),
                                valueType
                            })
                        )
                    )
                );
            }
        }

        public static class Threading
        {
            internal static NameSyntax Namespace = QualifiedName(WellKnownTypes.System.Namespace, IdentifierName("Threading"));
            
            internal static TypeSyntax Interlocked = QualifiedName(
                Namespace,
                IdentifierName("Interlocked")
            );

            internal static TypeSyntax Volatile = QualifiedName(
                Namespace,
                IdentifierName("Volatile")
            );

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
        internal static NameSyntax Namespace = AliasQualifiedName(
            IdentifierName(Token(SyntaxKind.GlobalKeyword)),
            IdentifierName("Imposter"));

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

            internal static NameSyntax MissingImposterException => QualifiedName(Namespace, IdentifierName("MissingImposterException"));

            internal static NameSyntax ImposterInvocationBehavior => QualifiedName(Namespace, IdentifierName("ImposterInvocationBehavior"));

            internal static NameSyntax OutArg(TypeSyntax type)
                => QualifiedName(Namespace, GenericName(Identifier(nameof(OutArg)), TypeArgumentList(SingletonSeparatedList(type))));

            internal static NameSyntax Arg(TypeSyntax type)
                => QualifiedName(Namespace, GenericName(Identifier(nameof(Arg)), TypeArgumentList(SingletonSeparatedList(type))));
        }
    }
}
