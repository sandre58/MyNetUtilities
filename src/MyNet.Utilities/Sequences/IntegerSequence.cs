// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

namespace MyNet.Utilities.Sequences
{
    /// <summary>
    /// A simple sequence generator. Note: it is NOT thread-safe.
    /// </summary>
    public class IntegerSequence : ISequence<uint>
    {
        private uint _currentValue;

        /// <summary>
        /// Initializes a new instance of the <see cref="IntegerSequence"/> class. 
        /// The sequence's initial current value is 0.
        /// </summary>
        public IntegerSequence() : this(0u) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="IntegerSequence"/> class.
        /// </summary>
        /// <param name="seed">The sequence initial value.</param>
        public IntegerSequence(uint seed) => _currentValue = seed;

        /// <summary>
        /// Gets this sequence's current value.
        /// </summary>
        public uint CurrentValue => _currentValue;

        /// <summary>
        /// Sets the current value to the specified value.
        /// Subsequent call to <see cref="NextValue"/> will return this <paramref name="value"/> + 1
        /// </summary>
        /// <param name="value">The new sequence current value.</param>
        public void SetCurrentValue(uint value) => _currentValue = value;

        /// <summary>
        /// Computes and retrieves this sequence's next value.
        /// </summary>
        /// <remarks>
        /// When called, the value of <see cref="CurrentValue"/> is updated.
        /// </remarks>
        public uint NextValue => ++_currentValue;
    }
}

