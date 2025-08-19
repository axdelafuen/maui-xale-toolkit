namespace Maui.XaleToolkit.Samples.Models
{
    /// <summary>
    /// A simple test object used for testing purposes.
    /// </summary>
    public class TestObject
    {
        /// <summary>
        /// The text representation of the object.
        /// </summary>
        public required string Text { get; set; }

        /// <summary>
        /// The integer value associated with the object.
        /// </summary>
        public required int Value { get; set; }

        /// <summary>
        /// Returns a string representation of the object.
        /// </summary>
        /// <returns></returns>
        public override string ToString() => Text;

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="obj">The oject to compare.</param>
        /// <returns>True if both object are equal. False otherwise.</returns>
        public override bool Equals(object? obj)
        {
            if (obj is TestObject toCompare)
                return Text == toCompare.Text && Value == toCompare.Value;
            else
                return base.Equals(obj);
        }

        /// <summary>
        /// Returns a hash code for the current object.
        /// </summary>
        /// <returns>A hash code for the current object</returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
