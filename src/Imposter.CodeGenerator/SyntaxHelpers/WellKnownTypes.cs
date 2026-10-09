using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.SyntaxHelpers;

internal static class WellKnownTypes
{
    internal static readonly TypeSyntax Void = PredefinedType(Token(SyntaxKind.VoidKeyword));

    internal static readonly TypeSyntax Int = PredefinedType(Token(SyntaxKind.IntKeyword));

    internal static readonly TypeSyntax Bool = PredefinedType(Token(SyntaxKind.BoolKeyword));

    internal static readonly TypeSyntax String = PredefinedType(Token(SyntaxKind.StringKeyword));

    internal static class System
    {
        internal static readonly NameSyntax Namespace = AliasQualifiedName(
            IdentifierName(Token(SyntaxKind.GlobalKeyword)),
            IdentifierName("System")
        );

        internal static readonly TypeSyntax Exception = QualifiedName(
            Namespace,
            IdentifierName("Exception")
        );

        internal static readonly TypeSyntax ArgumentNullException = QualifiedName(
            Namespace,
            IdentifierName("ArgumentNullException")
        );

        internal static readonly TypeSyntax NotImplementedException = QualifiedName(
            Namespace,
            IdentifierName("NotImplementedException")
        );

        internal static readonly TypeSyntax Environment = QualifiedName(
            Namespace,
            IdentifierName("Environment")
        );

        internal static readonly TypeSyntax String = QualifiedName(
            Namespace,
            IdentifierName("String")
        );

        internal static readonly TypeSyntax Delegate = QualifiedName(
            Namespace,
            IdentifierName("Delegate")
        );

        internal static readonly TypeSyntax Action = QualifiedName(
            Namespace,
            IdentifierName("Action")
        );

        internal static TypeSyntax ActionOfT(TypeSyntax typeArgument) =>
            GenericType(Namespace, "Action", typeArgument);

        // The parameter types, then the result type.
        internal static TypeSyntax Func(params TypeSyntax[] typeArguments) =>
            GenericType(Namespace, "Func", typeArguments);

        internal static TypeSyntax IEquatable(TypeSyntax typeArgument) =>
            GenericType(Namespace, "IEquatable", typeArgument);

        internal static TypeSyntax Tuple(TypeSyntax item1Type, TypeSyntax item2Type) =>
            GenericType(Namespace, "Tuple", item1Type, item2Type);

        public static class Collections
        {
            internal static readonly NameSyntax Namespace = QualifiedName(
                WellKnownTypes.System.Namespace,
                IdentifierName("Collections")
            );

            public static class Concurrent
            {
                internal static readonly NameSyntax Namespace = QualifiedName(
                    WellKnownTypes.System.Collections.Namespace,
                    IdentifierName("Concurrent")
                );

                internal static TypeSyntax ConcurrentQueue(TypeSyntax typeArgument) =>
                    GenericType(Namespace, "ConcurrentQueue", typeArgument);

                internal static TypeSyntax ConcurrentStack(TypeSyntax typeArgument) =>
                    GenericType(Namespace, "ConcurrentStack", typeArgument);

                internal static TypeSyntax ConcurrentDictionary(
                    TypeSyntax keyType,
                    TypeSyntax valueType
                ) => GenericType(Namespace, "ConcurrentDictionary", keyType, valueType);
            }

            public static class Generic
            {
                internal static readonly NameSyntax Namespace = QualifiedName(
                    WellKnownTypes.System.Collections.Namespace,
                    IdentifierName("Generic")
                );

                internal static TypeSyntax IEnumerable(TypeSyntax typeArgument) =>
                    GenericType(Namespace, "IEnumerable", typeArgument);

                internal static TypeSyntax List(TypeSyntax typeArgument) =>
                    GenericType(Namespace, "List", typeArgument);

                internal static TypeSyntax EqualityComparer(TypeSyntax typeArgument) =>
                    GenericType(Namespace, "EqualityComparer", typeArgument);
            }
        }

        public static class Linq
        {
            internal static readonly NameSyntax Namespace = QualifiedName(
                WellKnownTypes.System.Namespace,
                IdentifierName("Linq")
            );
        }

        public static class Diagnostics
        {
            internal static readonly NameSyntax Namespace = QualifiedName(
                WellKnownTypes.System.Namespace,
                IdentifierName("Diagnostics")
            );
        }

        public static class Runtime
        {
            internal static readonly NameSyntax Namespace = QualifiedName(
                WellKnownTypes.System.Namespace,
                IdentifierName("Runtime")
            );

            public static class CompilerServices
            {
                internal static readonly NameSyntax Namespace = QualifiedName(
                    Runtime.Namespace,
                    IdentifierName("CompilerServices")
                );
            }
        }

        public static class Threading
        {
            internal static readonly NameSyntax Namespace = QualifiedName(
                WellKnownTypes.System.Namespace,
                IdentifierName("Threading")
            );

            internal static readonly TypeSyntax Interlocked = QualifiedName(
                Namespace,
                IdentifierName("Interlocked")
            );

            internal static readonly TypeSyntax Volatile = QualifiedName(
                Namespace,
                IdentifierName("Volatile")
            );

            public static class Tasks
            {
                internal static readonly NameSyntax Namespace = QualifiedName(
                    WellKnownTypes.System.Threading.Namespace,
                    IdentifierName("Tasks")
                );

                internal static readonly TypeSyntax Task = QualifiedName(
                    Namespace,
                    IdentifierName("Task")
                );

                internal static TypeSyntax TaskOfT(TypeSyntax typeArgument) =>
                    GenericType(Namespace, "Task", typeArgument);
            }
        }
    }

    internal static class Imposter
    {
        internal static readonly NameSyntax Namespace = AliasQualifiedName(
            IdentifierName(Token(SyntaxKind.GlobalKeyword)),
            IdentifierName("Imposter")
        );

        internal static class Abstractions
        {
            internal static readonly NameSyntax Namespace = QualifiedName(
                WellKnownTypes.Imposter.Namespace,
                IdentifierName("Abstractions")
            );

            internal static readonly NameSyntax VerificationFailedException = QualifiedName(
                Namespace,
                IdentifierName("VerificationFailedException")
            );

            internal static readonly NameSyntax Count = QualifiedName(
                Namespace,
                IdentifierName("Count")
            );

            internal static readonly NameSyntax TypeCaster = QualifiedName(
                Namespace,
                IdentifierName("TypeCaster")
            );

            internal static readonly NameSyntax MissingImposterException = QualifiedName(
                Namespace,
                IdentifierName("MissingImposterException")
            );

            internal static readonly NameSyntax ImposterMode = QualifiedName(
                Namespace,
                IdentifierName("ImposterMode")
            );

            internal static NameSyntax IHaveImposterInstance(TypeSyntax instanceType) =>
                GenericType(Namespace, nameof(IHaveImposterInstance), instanceType);

            internal static NameSyntax OutArg(TypeSyntax type) =>
                GenericType(Namespace, nameof(OutArg), type);

            internal static NameSyntax Arg(TypeSyntax type) =>
                GenericType(Namespace, nameof(Arg), type);

            internal static NameSyntax SpanArg(TypeSyntax elementType) =>
                GenericType(Namespace, nameof(SpanArg), elementType);

            internal static NameSyntax ReadOnlySpanArg(TypeSyntax elementType) =>
                GenericType(Namespace, nameof(ReadOnlySpanArg), elementType);

            internal static NameSyntax OutSpanArg(TypeSyntax elementType) =>
                GenericType(Namespace, nameof(OutSpanArg), elementType);

            internal static NameSyntax OutReadOnlySpanArg(TypeSyntax elementType) =>
                GenericType(Namespace, nameof(OutReadOnlySpanArg), elementType);

            internal static NameSyntax SpanElementsComparer(TypeSyntax elementType) =>
                GenericType(Namespace, nameof(SpanElementsComparer), elementType);
        }
    }

    // A generic type in a namespace, such as global::System.Func<T, TResult>.
    private static QualifiedNameSyntax GenericType(
        NameSyntax @namespace,
        string name,
        params TypeSyntax[] typeArguments
    ) =>
        QualifiedName(
            @namespace,
            GenericName(Identifier(name), SyntaxFactoryHelper.TypeArguments(typeArguments))
        );
}
