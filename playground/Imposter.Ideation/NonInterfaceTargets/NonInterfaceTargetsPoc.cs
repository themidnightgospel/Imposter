using System;
using System.Collections.Generic;
using System.Linq;
using Imposter.Abstractions;

namespace Imposter.Ideation.NonInterfaceTargets
{
    /// <summary>
    /// Represents a concrete system under test that exposes virtual members instead of an interface.
    /// </summary>
    public abstract class WarehouseAllocationService
    {
        /// <summary>
        /// Gets the logical region this allocation service belongs to.
        /// </summary>
        public virtual string Region => "global";

        /// <summary>
        /// Gets or sets the number of reservations currently tracked.
        /// </summary>
        public virtual int ActiveReservations { get; set; }

        /// <summary>
        /// Reserves inventory for the provided SKU/quantity pair.
        /// </summary>
        public abstract int Reserve(string sku, int quantity);

        /// <summary>
        /// Formats a protected audit line. Only accessible to derived imposters.
        /// </summary>
        protected virtual string FormatAudit(string sku, int quantity) => $"{sku}:{quantity}";

        /// <summary>
        /// Helper entry-point that the tests can reach to hit the protected <see cref="FormatAudit"/> override.
        /// </summary>
        public string EmitAuditLine(string sku, int quantity) => FormatAudit(sku, quantity);
    }

    /// <summary>
    /// Proof-of-concept imposter that derives from <see cref="WarehouseAllocationService"/> and overrides
    /// public/protected virtual members using Imposter abstractions.
    /// </summary>
    public sealed class WarehouseAllocationImposter : WarehouseAllocationService, IHaveImposterInstance<WarehouseAllocationService>
    {
        private readonly ReserveMethodImposter _reserve;
        private readonly FormatAuditMethodImposter _formatAudit;
        private readonly VirtualPropertyStore<string> _region;
        private readonly VirtualPropertyStore<int> _activeReservations;

        public WarehouseAllocationImposter(ImposterMode mode = ImposterMode.Explicit)
        {
            _reserve = new ReserveMethodImposter(mode, Member(nameof(Reserve)));
            _formatAudit = new FormatAuditMethodImposter(mode, Member(nameof(FormatAudit)));

            _region = new VirtualPropertyStore<string>(Member(nameof(Region)), mode);
            _region.SetGetter(() => "global");

            _activeReservations = new VirtualPropertyStore<int>(Member(nameof(ActiveReservations)), mode);
            _activeReservations.SetAutoProperty(0);
        }

        public WarehouseAllocationService Instance() => this;

        public IReserveMethodImposterBuilder Reserve(Arg<string> sku, Arg<int> quantity)
            => _reserve.CreateBuilder(new AllocationArgumentsCriteria(sku, quantity));

        public IFormatAuditMethodImposterBuilder FormatAudit(Arg<string> sku, Arg<int> quantity)
            => _formatAudit.CreateBuilder(new AllocationArgumentsCriteria(sku, quantity));

        public IReadOnlyPropertyImposterBuilder<string> RegionProperty() => new ReadOnlyPropertyBuilder<string>(_region);

        public IReadWritePropertyImposterBuilder<int> ActiveReservationsProperty() => new ReadWritePropertyBuilder<int>(_activeReservations);

        public override int Reserve(string sku, int quantity) => _reserve.Invoke(new AllocationArguments(sku, quantity));

        protected override string FormatAudit(string sku, int quantity) => _formatAudit.Invoke(new AllocationArguments(sku, quantity));

        public override string Region => _region.Get();

        public override int ActiveReservations
        {
            get => _activeReservations.Get();
            set => _activeReservations.Set(value);
        }

        private static string Member(string memberName) => $"{typeof(WarehouseAllocationService).FullName}.{memberName}";

        #region Method imposters

        private readonly struct AllocationArguments
        {
            internal AllocationArguments(string sku, int quantity)
            {
                Sku = sku;
                Quantity = quantity;
            }

            internal string Sku { get; }
            internal int Quantity { get; }
        }

        private sealed class AllocationArgumentsCriteria
        {
            private readonly Arg<string> _sku;
            private readonly Arg<int> _quantity;

            internal AllocationArgumentsCriteria(Arg<string> sku, Arg<int> quantity)
            {
                _sku = sku ?? throw new ArgumentNullException(nameof(sku));
                _quantity = quantity ?? throw new ArgumentNullException(nameof(quantity));
            }

            internal bool Matches(AllocationArguments arguments)
                => _sku.Matches(arguments.Sku) && _quantity.Matches(arguments.Quantity);
        }

        public interface IReserveMethodImposterBuilder
        {
            IReserveMethodImposterBuilder Returns(int value);

            IReserveMethodImposterBuilder Returns(Func<string, int, int> generator);

            IReserveMethodImposterBuilder Throws<TException>() where TException : Exception, new();

            IReserveMethodImposterBuilder Throws(Exception exception);

            IReserveMethodImposterBuilder Throws(Func<string, int, Exception> exceptionFactory);

            IReserveMethodImposterBuilder Callback(Action<string, int> callback);

            void Called(Count count);
        }

        public interface IFormatAuditMethodImposterBuilder
        {
            IFormatAuditMethodImposterBuilder Returns(string value);

            IFormatAuditMethodImposterBuilder Returns(Func<string, int, string> generator);

            IFormatAuditMethodImposterBuilder Throws<TException>() where TException : Exception, new();

            IFormatAuditMethodImposterBuilder Throws(Exception exception);

            IFormatAuditMethodImposterBuilder Throws(Func<string, int, Exception> exceptionFactory);

            IFormatAuditMethodImposterBuilder Callback(Action<string, int> callback);

            void Called(Count count);
        }

        private sealed class ReserveMethodImposter
        {
            private readonly VirtualMethodImposter<AllocationArguments, int> _method;

            internal ReserveMethodImposter(ImposterMode behavior, string memberName)
            {
                _method = new VirtualMethodImposter<AllocationArguments, int>(behavior, memberName);
            }

            internal IReserveMethodImposterBuilder CreateBuilder(AllocationArgumentsCriteria criteria)
            {
                var configuration = _method.CreateSetup(criteria.Matches);
                return new Builder(this, configuration, criteria);
            }

            internal int Invoke(AllocationArguments arguments) => _method.Invoke(arguments);

            internal void Verify(AllocationArgumentsCriteria criteria, Count count) => _method.Verify(criteria.Matches, count);

            private sealed class Builder : IReserveMethodImposterBuilder
            {
                private readonly ReserveMethodImposter _owner;
                private readonly AllocationArgumentsCriteria _criteria;
                private readonly VirtualMethodImposter<AllocationArguments, int>.ConfiguredInvocation _configuration;

                internal Builder(ReserveMethodImposter owner, VirtualMethodImposter<AllocationArguments, int>.ConfiguredInvocation configuration, AllocationArgumentsCriteria criteria)
                {
                    _owner = owner;
                    _configuration = configuration;
                    _criteria = criteria;
                }

                public IReserveMethodImposterBuilder Returns(int value)
                {
                    _configuration.Returns(_ => value);
                    return this;
                }

                public IReserveMethodImposterBuilder Returns(Func<string, int, int> generator)
                {
                    if (generator is null) throw new ArgumentNullException(nameof(generator));
                    _configuration.Returns(arguments => generator(arguments.Sku, arguments.Quantity));
                    return this;
                }

                public IReserveMethodImposterBuilder Throws<TException>() where TException : Exception, new()
                {
                    _configuration.Throws(_ => new TException());
                    return this;
                }

                public IReserveMethodImposterBuilder Throws(Exception exception)
                {
                    if (exception is null) throw new ArgumentNullException(nameof(exception));
                    _configuration.Throws(_ => exception);
                    return this;
                }

                public IReserveMethodImposterBuilder Throws(Func<string, int, Exception> exceptionFactory)
                {
                    if (exceptionFactory is null) throw new ArgumentNullException(nameof(exceptionFactory));
                    _configuration.Throws(arguments => exceptionFactory(arguments.Sku, arguments.Quantity));
                    return this;
                }

                public IReserveMethodImposterBuilder Callback(Action<string, int> callback)
                {
                    if (callback is null) throw new ArgumentNullException(nameof(callback));
                    _configuration.Callback(arguments => callback(arguments.Sku, arguments.Quantity));
                    return this;
                }

                public void Called(Count count)
                {
                    if (count is null) throw new ArgumentNullException(nameof(count));
                    _owner.Verify(_criteria, count);
                }
            }
        }

        private sealed class FormatAuditMethodImposter
        {
            private readonly VirtualMethodImposter<AllocationArguments, string> _method;

            internal FormatAuditMethodImposter(ImposterMode behavior, string memberName)
            {
                _method = new VirtualMethodImposter<AllocationArguments, string>(behavior, memberName);
            }

            internal IFormatAuditMethodImposterBuilder CreateBuilder(AllocationArgumentsCriteria criteria)
            {
                var configuration = _method.CreateSetup(criteria.Matches);
                return new Builder(this, configuration, criteria);
            }

            internal string Invoke(AllocationArguments arguments) => _method.Invoke(arguments);

            internal void Verify(AllocationArgumentsCriteria criteria, Count count) => _method.Verify(criteria.Matches, count);

            private sealed class Builder : IFormatAuditMethodImposterBuilder
            {
                private readonly FormatAuditMethodImposter _owner;
                private readonly AllocationArgumentsCriteria _criteria;
                private readonly VirtualMethodImposter<AllocationArguments, string>.ConfiguredInvocation _configuration;

                internal Builder(FormatAuditMethodImposter owner, VirtualMethodImposter<AllocationArguments, string>.ConfiguredInvocation configuration, AllocationArgumentsCriteria criteria)
                {
                    _owner = owner;
                    _configuration = configuration;
                    _criteria = criteria;
                }

                public IFormatAuditMethodImposterBuilder Returns(string value)
                {
                    _configuration.Returns(_ => value);
                    return this;
                }

                public IFormatAuditMethodImposterBuilder Returns(Func<string, int, string> generator)
                {
                    if (generator is null) throw new ArgumentNullException(nameof(generator));
                    _configuration.Returns(arguments => generator(arguments.Sku, arguments.Quantity));
                    return this;
                }

                public IFormatAuditMethodImposterBuilder Throws<TException>() where TException : Exception, new()
                {
                    _configuration.Throws(_ => new TException());
                    return this;
                }

                public IFormatAuditMethodImposterBuilder Throws(Exception exception)
                {
                    if (exception is null) throw new ArgumentNullException(nameof(exception));
                    _configuration.Throws(_ => exception);
                    return this;
                }

                public IFormatAuditMethodImposterBuilder Throws(Func<string, int, Exception> exceptionFactory)
                {
                    if (exceptionFactory is null) throw new ArgumentNullException(nameof(exceptionFactory));
                    _configuration.Throws(arguments => exceptionFactory(arguments.Sku, arguments.Quantity));
                    return this;
                }

                public IFormatAuditMethodImposterBuilder Callback(Action<string, int> callback)
                {
                    if (callback is null) throw new ArgumentNullException(nameof(callback));
                    _configuration.Callback(arguments => callback(arguments.Sku, arguments.Quantity));
                    return this;
                }

                public void Called(Count count)
                {
                    if (count is null) throw new ArgumentNullException(nameof(count));
                    _owner.Verify(_criteria, count);
                }
            }
        }

        private sealed class VirtualMethodImposter<TArguments, TResult>
        {
            private readonly List<ConfiguredInvocation> _setups = new List<ConfiguredInvocation>();
            private readonly List<TArguments> _invocations = new List<TArguments>();
            private readonly ImposterMode _behavior;
            private readonly string _memberName;

            internal VirtualMethodImposter(ImposterMode behavior, string memberName)
            {
                _behavior = behavior;
                _memberName = memberName;
            }

            internal ConfiguredInvocation CreateSetup(Func<TArguments, bool> criteria)
            {
                if (criteria is null) throw new ArgumentNullException(nameof(criteria));
                var configuration = new ConfiguredInvocation(criteria);
                _setups.Add(configuration);
                return configuration;
            }

            internal TResult Invoke(TArguments arguments)
            {
                _invocations.Add(arguments);

                for (var i = _setups.Count - 1; i >= 0; i--)
                {
                    if (_setups[i].Matches(arguments))
                    {
                        return _setups[i].Invoke(arguments);
                    }
                }

                if (_behavior == ImposterMode.Explicit)
                {
                    throw new MissingImposterException(_memberName);
                }

                return default!;
            }

            internal void Verify(Func<TArguments, bool> criteria, Count count)
            {
                if (criteria is null) throw new ArgumentNullException(nameof(criteria));
                if (count is null) throw new ArgumentNullException(nameof(count));

                var actual = _invocations.Count(criteria);
                if (!count.Matches(actual))
                {
                    throw new VerificationFailedException(count, actual);
                }
            }

            internal sealed class ConfiguredInvocation
            {
                private readonly Func<TArguments, bool> _criteria;
                private Func<TArguments, TResult>? _resultGenerator;
                private Func<TArguments, Exception>? _exceptionFactory;
                private Action<TArguments>? _callback;

                internal ConfiguredInvocation(Func<TArguments, bool> criteria)
                {
                    _criteria = criteria ?? throw new ArgumentNullException(nameof(criteria));
                }

                internal ConfiguredInvocation Returns(Func<TArguments, TResult> generator)
                {
                    _resultGenerator = generator ?? throw new ArgumentNullException(nameof(generator));
                    return this;
                }

                internal ConfiguredInvocation Throws(Func<TArguments, Exception> exceptionFactory)
                {
                    _exceptionFactory = exceptionFactory ?? throw new ArgumentNullException(nameof(exceptionFactory));
                    return this;
                }

                internal ConfiguredInvocation Callback(Action<TArguments> callback)
                {
                    if (callback is null) throw new ArgumentNullException(nameof(callback));
                    _callback += callback;
                    return this;
                }

                internal bool Matches(TArguments arguments) => _criteria(arguments);

                internal TResult Invoke(TArguments arguments)
                {
                    _callback?.Invoke(arguments);

                    if (_exceptionFactory != null)
                    {
                        throw _exceptionFactory(arguments);
                    }

                    if (_resultGenerator != null)
                    {
                        return _resultGenerator(arguments);
                    }

                    return default!;
                }
            }
        }

        #endregion

        #region Property imposters

        public interface IReadOnlyPropertyImposterBuilder<T>
        {
            IReadOnlyPropertyImposterBuilder<T> Returns(T value);

            IReadOnlyPropertyImposterBuilder<T> Returns(Func<T> generator);
        }

        public interface IReadWritePropertyImposterBuilder<T>
        {
            IReadWritePropertyImposterBuilder<T> Returns(T value);

            IReadWritePropertyImposterBuilder<T> Returns(Func<T> generator);

            IReadWritePropertyImposterBuilder<T> ActsLikeProperty(T initialValue = default!);

            IReadWritePropertyImposterBuilder<T> SetterCallback(Action<T> callback);
        }

        private sealed class VirtualPropertyStore<T>
        {
            private readonly string _memberName;
            private readonly ImposterMode _behavior;
            private Func<T>? _getter;
            private Action<T>? _setter;
            private Action<T>? _setterCallback;

            internal VirtualPropertyStore(string memberName, ImposterMode behavior)
            {
                _memberName = memberName;
                _behavior = behavior;
            }

            internal void SetGetter(Func<T> getter)
            {
                _getter = getter ?? throw new ArgumentNullException(nameof(getter));
            }

            internal void SetSetter(Action<T> setter)
            {
                _setter = setter ?? throw new ArgumentNullException(nameof(setter));
            }

            internal void SetAutoProperty(T initialValue)
            {
                var current = initialValue;
                _getter = () => current;
                _setter = value => current = value;
            }

            internal void AddSetterCallback(Action<T> callback)
            {
                if (callback is null) throw new ArgumentNullException(nameof(callback));
                _setterCallback += callback;
            }

            internal T Get()
            {
                if (_getter is null)
                {
                    if (_behavior == ImposterMode.Explicit)
                    {
                        throw new MissingImposterException(_memberName);
                    }

                    return default!;
                }

                return _getter();
            }

            internal void Set(T value)
            {
                if (_setter is null)
                {
                    if (_behavior == ImposterMode.Explicit)
                    {
                        throw new MissingImposterException(_memberName);
                    }
                }
                else
                {
                    _setter(value);
                }

                _setterCallback?.Invoke(value);
            }
        }

        private sealed class ReadOnlyPropertyBuilder<T> : IReadOnlyPropertyImposterBuilder<T>
        {
            private readonly VirtualPropertyStore<T> _store;

            internal ReadOnlyPropertyBuilder(VirtualPropertyStore<T> store)
            {
                _store = store;
            }

            public IReadOnlyPropertyImposterBuilder<T> Returns(T value)
            {
                _store.SetGetter(() => value!);
                return this;
            }

            public IReadOnlyPropertyImposterBuilder<T> Returns(Func<T> generator)
            {
                _store.SetGetter(generator ?? throw new ArgumentNullException(nameof(generator)));
                return this;
            }
        }

        private sealed class ReadWritePropertyBuilder<T> : IReadWritePropertyImposterBuilder<T>
        {
            private readonly VirtualPropertyStore<T> _store;

            internal ReadWritePropertyBuilder(VirtualPropertyStore<T> store)
            {
                _store = store;
            }

            public IReadWritePropertyImposterBuilder<T> Returns(T value)
            {
                _store.SetGetter(() => value!);
                return this;
            }

            public IReadWritePropertyImposterBuilder<T> Returns(Func<T> generator)
            {
                _store.SetGetter(generator ?? throw new ArgumentNullException(nameof(generator)));
                return this;
            }

            public IReadWritePropertyImposterBuilder<T> ActsLikeProperty(T initialValue = default!)
            {
                _store.SetAutoProperty(initialValue);
                return this;
            }

            public IReadWritePropertyImposterBuilder<T> SetterCallback(Action<T> callback)
            {
                _store.AddSetterCallback(callback ?? throw new ArgumentNullException(nameof(callback)));
                return this;
            }
        }

        #endregion
    }
}