using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.SyntaxHelpers;

internal static class WellKnownTypes
{
    internal static class System
    {
        internal static NameSyntax Namespace = IdentifierName("System");

        internal static TypeSyntax Exception = QualifiedName(
            Namespace,
            IdentifierName("Exception")
        );

        public static class Threading
        {
            internal static NameSyntax Namespace = QualifiedName(WellKnownTypes.System.Namespace, IdentifierName("Threading"));

            public static class Tasks
            {
                internal static NameSyntax Namespace = QualifiedName(WellKnownTypes.System.Threading.Namespace, IdentifierName("Tasks"));

                internal static TypeSyntax Task = QualifiedName(
                    Namespace,
                    IdentifierName("Exception")
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
                        Identifier("IHaveImposterInstance"),
                        TypeArgumentList(
                            SingletonSeparatedList(instanceType)
                        )
                    )
                );

            internal static NameSyntax TypeCaster => QualifiedName(Namespace, IdentifierName("TypeCaster"));
        }
    }
}