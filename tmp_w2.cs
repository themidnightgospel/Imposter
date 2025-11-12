        }

        public static class Linq
        {
            internal static NameSyntax Namespace = QualifiedName(WellKnownTypes.System.Namespace, IdentifierName("Linq"));
        }

        public static class Diagnostics
        {
            internal static NameSyntax Namespace = QualifiedName(WellKnownTypes.System.Namespace, IdentifierName("Diagnostics"));
        }

        public static class Runtime
        {
            internal static NameSyntax Namespace = QualifiedName(WellKnownTypes.System.Namespace, IdentifierName("Runtime"));

            public static class CompilerServices
            {
                internal static NameSyntax Namespace = QualifiedName(Runtime.Namespace, IdentifierName("CompilerServices"));
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
