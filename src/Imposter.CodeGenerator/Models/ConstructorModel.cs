namespace Imposter.CodeGenerator.Models;

/// <summary>
/// A constructor of a class target that the imposter's assembly can call.
/// </summary>
internal sealed record ConstructorModel(EquatableArray<ParameterModel> Parameters);
