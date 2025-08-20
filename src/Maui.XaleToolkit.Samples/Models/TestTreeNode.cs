namespace Maui.XaleToolkit.Samples.Models
{
    /// <summary>
    ///  A simple tree test object used for testing purposes.
    /// </summary>
    public class TestTreeNode
    {
        /// <summary>
        /// Current node value
        /// </summary>
        public string Value { get; set; } = string.Empty;

        /// <summary>
        /// TreeNode childrens
        /// </summary>
        public List<TestTreeNode> Children { get; set; } = [];

        /// <summary>
        /// Returns a string representation of the object.
        /// </summary>
        /// <returns></returns>
        public override string ToString() => Value;
    }
}
