using System;
using System.Text.RegularExpressions;
using Imposter.Abstractions;

#pragma warning disable nullable

namespace Imposter.Ideation.IndexerSetupPoc
{
    [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
    public class IndexerSetupPoc : Imposter.Abstractions.IHaveImposterInstance<IIndexerSetupPoc>
    {
        private ImposterTargetInstance _imposterInstance;
        private readonly IndexerIntStringRegexImposter _indexerIntStringRegexImposter;

        public IndexerSetupPoc()
        {
            this._imposterInstance = new ImposterTargetInstance(this);
        }


        public string this[int name, string lastname, Regex dog]
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class ImposterTargetInstance : IIndexerSetupPoc
        {
            IndexerSetupPoc _imposter;

            public ImposterTargetInstance(IndexerSetupPoc _imposter)
            {
                this._imposter = _imposter;
            }

            public string this[int name, string lastname, Regex dog]
            {
                get => throw new NotImplementedException();
                set => throw new NotImplementedException();
            }

            public string this[string name] => throw new NotImplementedException();
        }

        IIndexerSetupPoc IHaveImposterInstance<IIndexerSetupPoc>.Instance()
        {
            return _imposterInstance;
        }

        class IndexerIntStringRegexImposter
        {
            
        }
    }

    public interface IIndexerSetupPoc
    {
        string this[int name, string lastname, Regex dog] { get; set; }

        string this[string name] { get; }
    }

    public static class Usage
    {
        static void SutUsage()
        {
        }
    }
}