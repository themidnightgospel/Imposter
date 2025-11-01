using System;
using System.Runtime.CompilerServices;
using BenchmarkDotNet.Attributes;

namespace Imposter.Benchmarks;

[MemoryDiagnoser]
public class ReadonlyStructBenchmarks
{
    private readonly BigStruct _bigStruct = new BigStruct(
        "First", "Second", "Third", "Fourth", "Fifth",
        "Sixth", "Seventh", "Eighth", "Ninth", "Tenth",
        1, 2, 3, 4, 5,
        6, 7, 8, 9, 10,
        DateTime.Now, DateTime.UtcNow, DateTime.Today,
        Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid());
    
    [Params(1, 10, 1000)]
    public int Iteration;
    
    [Benchmark]
    public void PassWithoutInParameter()
    {
        for (var i = 0; i < Iteration; i++)
        {
            MethodWithoutIn(_bigStruct);
        }
    }
    
    [Benchmark]
    public void PassWithInParameter()
    {
        for (var i = 0; i < Iteration; i++)
        {
            MethodWithIn(_bigStruct);
        }
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public int MethodWithoutIn(BigStruct bigStruct) => bigStruct.Number1 + bigStruct.Number2;

    
    [MethodImpl(MethodImplOptions.NoInlining)]
    public int MethodWithIn(in BigStruct bigStruct) => bigStruct.Number1 + bigStruct.Number2;


    public readonly struct BigStruct
    {
        public readonly string String1;
        public readonly string String2;
        public readonly string String3;
        public readonly string String4;
        public readonly string String5;
        public readonly string String6;
        public readonly string String7;
        public readonly string String8;
        public readonly string String9;
        public readonly string String10;
        public readonly int Number1;
        public readonly int Number2;
        public readonly int Number3;
        public readonly int Number4;
        public readonly int Number5;
        public readonly int Number6;
        public readonly int Number7;
        public readonly int Number8;
        public readonly int Number9;
        public readonly int Number10;
        public readonly DateTime Date1;
        public readonly DateTime Date2;
        public readonly DateTime Date3;
        public readonly Guid Id1;
        public readonly Guid Id2;
        public readonly Guid Id3;

        public BigStruct(
            string str1, string str2, string str3, string str4, string str5,
            string str6, string str7, string str8, string str9, string str10,
            int num1, int num2, int num3, int num4, int num5,
            int num6, int num7, int num8, int num9, int num10,
            DateTime date1, DateTime date2, DateTime date3,
            Guid id1, Guid id2, Guid id3)
        {
            String1 = str1;
            String2 = str2;
            String3 = str3;
            String4 = str4;
            String5 = str5;
            String6 = str6;
            String7 = str7;
            String8 = str8;
            String9 = str9;
            String10 = str10;
            Number1 = num1;
            Number2 = num2;
            Number3 = num3;
            Number4 = num4;
            Number5 = num5;
            Number6 = num6;
            Number7 = num7;
            Number8 = num8;
            Number9 = num9;
            Number10 = num10;
            Date1 = date1;
            Date2 = date2;
            Date3 = date3;
            Id1 = id1;
            Id2 = id2;
            Id3 = id3;
        }
    }
}