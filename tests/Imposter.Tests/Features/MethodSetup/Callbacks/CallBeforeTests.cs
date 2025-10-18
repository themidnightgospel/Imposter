using System.Collections.Generic;
using System.Linq;
using Imposter.Abstractions;
using Shouldly;
using System.Text.RegularExpressions;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Features.MethodSetup.Callbacks
{
    public class CallBeforeTests
    {
        private readonly IMethodSetupFeatureSutImposter _sut = new IMethodSetupFeatureSutImposter();

        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        public void GivenCallBeforeCallbackSetupOnVoid_NoParams_MethodInvokedTwice_CallbackInvokedTwice(int invocationCount)
        {
            var callBeforeCallbackInvokedCount = 0;

            _sut
                .Void_NoParams()
                .CallBefore(() => ++callBeforeCallbackInvokedCount);

            for (var i = 0; i < invocationCount; i++)
            {
                _sut.Instance().Void_NoParams();
            }

            callBeforeCallbackInvokedCount.ShouldBe(invocationCount);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        public void GivenCallBeforeSetupOnInt_NoParams_WhenMethodInvoked_CallbackIsInvoked(int invocationCount)
        {
            var callBeforeCallbackInvokedCount = 0;

            _sut
                .Int_NoParams()
                .CallBefore(() => ++callBeforeCallbackInvokedCount);

            for (var i = 0; i < invocationCount; i++)
            {
                _sut.Instance().Int_NoParams();
            }

            callBeforeCallbackInvokedCount.ShouldBe(invocationCount);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        public void GivenCallBeforeSetupOnInt_SingleParam_WhenMethodInvoked_CallbackIsInvokedWithCorrectParameter(int invocationCount)
        {
            var callBeforeCallbackInvokedCount = 0;
            var captured = new List<int>();

            _sut
                .Int_SingleParam(Arg<int>.Any())
                .CallBefore(age =>
                {
                    callBeforeCallbackInvokedCount++;
                    captured.Add(age);
                });

            for (var i = 0; i < invocationCount; i++)
            {
                _sut.Instance().Int_SingleParam(42 + i);
            }

            callBeforeCallbackInvokedCount.ShouldBe(invocationCount);
            captured.ShouldBe(Enumerable.Range(42, invocationCount).ToList());
        }

        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        public void GivenCallBeforeSetupOnInt_Params_WhenMethodInvoked_CallbackIsInvokedWithCorrectParameters(int invocationCount)
        {
            var callBeforeCallbackInvokedCount = 0;
            var ageList = new List<int>();
            var nameList = new List<string>();
            var regexList = new List<Regex>();

            _sut
                .Int_Params(Arg<int>.Any(), Arg<string>.Any(), Arg<Regex>.Any())
                .CallBefore((age, name, regex) =>
                {
                    callBeforeCallbackInvokedCount++;
                    ageList.Add(age);
                    nameList.Add(name);
                    regexList.Add(regex);
                });

            for (var i = 0; i < invocationCount; i++)
            {
                var regex = new Regex($"abc{i}");
                _sut.Instance().Int_Params(i, $"name{i}", regex);
            }

            callBeforeCallbackInvokedCount.ShouldBe(invocationCount);
            ageList.ShouldBe(Enumerable.Range(0, invocationCount).ToList());
            nameList.ShouldBe(Enumerable.Range(0, invocationCount).Select(i => $"name{i}").ToList());
            regexList.Select(r => r.ToString()).ShouldBe(Enumerable.Range(0, invocationCount).Select(i => $"abc{i}"));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        public void GivenCallBeforeSetupOnInt_OutParam_WhenMethodInvoked_CallbackIsInvokedWithCorrectParameters(int invocationCount)
        {
            var callBeforeCallbackInvokedCount = 0;
            var outList = new List<int>();

            _sut
                .Int_OutParam(OutArg<int>.Any())
                .CallBefore((out int o) =>
                {
                    o = 123;
                    callBeforeCallbackInvokedCount++;
                    outList.Add(o);
                });

            for (var i = 0; i < invocationCount; i++)
            {
                _sut.Instance().Int_OutParam(out var outVal);
            }

            callBeforeCallbackInvokedCount.ShouldBe(invocationCount);
            outList.ShouldBe(Enumerable.Repeat(123, invocationCount));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        public void GivenCallBeforeSetupOnInt_RefParam_WhenMethodInvoked_CallbackIsInvokedWithCorrectParameters(int invocationCount)
        {
            var callBeforeCallbackInvokedCount = 0;
            var refList = new List<int>();

            _sut
                .Int_RefParam(Arg<int>.Any())
                .CallBefore((ref int r) =>
                {
                    callBeforeCallbackInvokedCount++;
                    refList.Add(r);
                });

            for (var i = 0; i < invocationCount; i++)
            {
                var v = 10 + i;
                _sut.Instance().Int_RefParam(ref v);
            }

            callBeforeCallbackInvokedCount.ShouldBe(invocationCount);
            refList.ShouldBe(Enumerable.Range(10, invocationCount).ToList());
        }

        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        public void GivenCallBeforeSetupOnInt_ParamsParam_WhenMethodInvoked_CallbackIsInvokedWithCorrectParameters(int invocationCount)
        {
            var callBeforeCallbackInvokedCount = 0;
            var arrs = new List<string[]>();

            _sut
                .Int_ParamsParam(Arg<string[]>.Any())
                .CallBefore(arr =>
                {
                    callBeforeCallbackInvokedCount++;
                    arrs.Add(arr);
                });

            for (var i = 0; i < invocationCount; i++)
            {
                var inputArr = new[] { $"a{i}", $"b{i}" };
                _sut.Instance().Int_ParamsParam(inputArr);
            }

            callBeforeCallbackInvokedCount.ShouldBe(invocationCount);
            arrs.ShouldBe(Enumerable.Range(0, invocationCount).Select(i => new[] { $"a{i}", $"b{i}" }), ignoreOrder: false);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        public void GivenCallBeforeSetupOnInt_InParam_WhenMethodInvoked_CallbackIsInvokedWithCorrectParameters(int invocationCount)
        {
            var callBeforeCallbackInvokedCount = 0;
            var stringList = new List<string>();

            _sut
                .Int_InParam(Arg<string>.Any())
                .CallBefore((in string s) =>
                {
                    callBeforeCallbackInvokedCount++;
                    stringList.Add(s);
                });

            for (var i = 0; i < invocationCount; i++)
            {
                _sut.Instance().Int_InParam($"in{i}");
            }

            callBeforeCallbackInvokedCount.ShouldBe(invocationCount);
            stringList.ShouldBe(Enumerable.Range(0, invocationCount).Select(i => $"in{i}").ToList());
        }

        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        public void GivenCallBeforeSetupOnInt_AllRefKinds_WhenMethodInvoked_CallbackIsInvokedWithCorrectParameters(int invocationCount)
        {
            var callBeforeCallbackInvokedCount = 0;
            var outList = new List<int>();
            var refList = new List<int>();
            var inList = new List<int>();
            var strList = new List<string>();
            var paramsList = new List<string[]>();

            _sut
                .Int_AllRefKinds(OutArg<int>.Any(), Arg<int>.Any(), Arg<int>.Any(), Arg<string>.Any(), Arg<string[]>.Any())
                .CallBefore((out int o, ref int r, in int i, string s, string[] p) =>
                {
                    o = 999;
                    callBeforeCallbackInvokedCount++;
                    outList.Add(o);
                    refList.Add(r);
                    inList.Add(i);
                    strList.Add(s);
                    paramsList.Add(p);
                });

            for (var i = 0; i < invocationCount; i++)
            {
                var refVal = 20 + i;
                var inVal = 100 + i;
                var str = $"val-{i}";
                var arr = new[] { $"p{i}-1", $"p{i}-2" };
                _sut.Instance().Int_AllRefKinds(out var outVal, ref refVal, in inVal, str, arr);
            }

            callBeforeCallbackInvokedCount.ShouldBe(invocationCount);
            outList.ShouldBe(Enumerable.Repeat(999, invocationCount));
            refList.ShouldBe(Enumerable.Range(20, invocationCount).ToList());
            inList.ShouldBe(Enumerable.Range(100, invocationCount).ToList());
            strList.ShouldBe(Enumerable.Range(0, invocationCount).Select(i => $"val-{i}"));
            paramsList.ShouldBe(Enumerable.Range(0, invocationCount).Select(i => new[] { $"p{i}-1", $"p{i}-2" }), ignoreOrder: false);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        public void GivenCallBeforeSetupOnGeneric_SingleParam_WhenMethodInvoked_CallbackIsInvokedWithCorrectParameter(int invocationCount)
        {
            var callBeforeCallbackInvokedCount = 0;
            var captured = new List<int>();

            _sut
                .Generic_SingleParam<int>(Arg<int>.Any())
                .CallBefore(value =>
                {
                    callBeforeCallbackInvokedCount++;
                    captured.Add(value);
                });

            for (var i = 0; i < invocationCount; i++)
            {
                _sut.Instance().Generic_SingleParam<int>(100 + i);
            }

            callBeforeCallbackInvokedCount.ShouldBe(invocationCount);
            captured.ShouldBe(Enumerable.Range(100, invocationCount).ToList());
        }

        [Theory]
        [InlineData(1)]
        [InlineData(4)]
        public void GivenCallBeforeSetupOnGeneric_OutParam_WhenMethodInvoked_CallbackIsInvokedWithCorrectParameter(int invocationCount)
        {
            var callBeforeCallbackInvokedCount = 0;
            var outParams = new List<string>();

            _sut
                .Generic_OutParam<string, int>(OutArg<string>.Any())
                .CallBefore((out string s) =>
                {
                    s = "hello";
                    callBeforeCallbackInvokedCount++;
                    outParams.Add(s);
                });

            for (var i = 0; i < invocationCount; i++)
            {
                _sut.Instance().Generic_OutParam<string, int>(out var val);
            }

            callBeforeCallbackInvokedCount.ShouldBe(invocationCount);
            outParams.ShouldBe(Enumerable.Repeat("hello", invocationCount));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(4)]
        public void GivenCallBeforeSetupOnGeneric_RefParam_WhenMethodInvoked_CallbackIsInvokedWithCorrectParameter(int invocationCount)
        {
            var callBeforeCallbackInvokedCount = 0;
            var refParams = new List<double>();

            _sut
                .Generic_RefParam<double, string>(Arg<double>.Any())
                .CallBefore((ref double d) =>
                {
                    callBeforeCallbackInvokedCount++;
                    refParams.Add(d);
                });

            for (var i = 0; i < invocationCount; i++)
            {
                double v = 1.1 + i;
                _sut.Instance().Generic_RefParam<double, string>(ref v);
            }

            callBeforeCallbackInvokedCount.ShouldBe(invocationCount);
            refParams.ShouldBe(Enumerable.Range(0, invocationCount).Select(i => 1.1 + i).ToList());
        }

        [Theory]
        [InlineData(1)]
        [InlineData(3)]
        public void GivenCallBeforeSetupOnGeneric_ParamsParam_WhenMethodInvoked_CallbackIsInvokedWithCorrectParameters(int invocationCount)
        {
            var callBeforeCallbackInvokedCount = 0;
            var allArrays = new List<string[]>();

            _sut
                .Generic_ParamsParam<string, int>(Arg<string[]>.Any())
                .CallBefore(arr =>
                {
                    callBeforeCallbackInvokedCount++;
                    allArrays.Add(arr);
                });

            for (var i = 0; i < invocationCount; i++)
            {
                var values = new[] { $"str{i}_1", $"str{i}_2" };
                _sut.Instance().Generic_ParamsParam<string, int>(values);
            }

            callBeforeCallbackInvokedCount.ShouldBe(invocationCount);
            allArrays.ShouldBe(Enumerable.Range(0, invocationCount).Select(i => new[] { $"str{i}_1", $"str{i}_2" }), ignoreOrder: false);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void GivenCallBeforeSetupOnGeneric_AllRefKind_WhenMethodInvoked_CallbackIsInvokedWithCorrectParameters(int invocationCount)
        {
            var callBeforeCallbackInvokedCount = 0;
            var outParams = new List<int>();
            var refParams = new List<string>();
            var inParams = new List<double>();
            var paramsParams = new List<bool[]>();

            _sut
                .Generic_AllRefKind<int, string, double, bool, long>(
                    OutArg<int>.Any(),
                    Arg<string>.Any(),
                    Arg<double>.Any(),
                    Arg<bool[]>.Any()
                )
                .CallBefore((out int o, ref string r, in double i, bool[] p) =>
                {
                    o = 111;
                    callBeforeCallbackInvokedCount++;
                    outParams.Add(o);
                    refParams.Add(r);
                    inParams.Add(i);
                    paramsParams.Add(p);
                });

            for (var idx = 0; idx < invocationCount; idx++)
            {
                int refValOut;
                string refVal = $"s{idx}";
                double inVal = idx + 0.5;
                bool[] arr = new[] { idx % 2 == 0, idx % 3 == 0 };
                _sut.Instance().Generic_AllRefKind<int, string, double, bool, long>(
                    out refValOut,
                    ref refVal,
                    in inVal,
                    arr
                );
            }

            callBeforeCallbackInvokedCount.ShouldBe(invocationCount);
            outParams.ShouldBe(Enumerable.Repeat(111, invocationCount));
            refParams.ShouldBe(Enumerable.Range(0, invocationCount).Select(idx => $"s{idx}").ToList());
            inParams.ShouldBe(Enumerable.Range(0, invocationCount).Select(idx => idx + 0.5).ToList());
            paramsParams.ShouldBe(Enumerable.Range(0, invocationCount).Select(idx => new[] { idx % 2 == 0, idx % 3 == 0 }), ignoreOrder: false);
        }
    }
}