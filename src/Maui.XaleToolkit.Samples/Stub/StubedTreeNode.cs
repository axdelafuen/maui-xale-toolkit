using Maui.XaleToolkit.Samples.Models;

namespace Maui.XaleToolkit.Samples.Stub
{
    public static class StubedTreeNode
    {
        public static IList<TestTreeNode> GetNodes()
        {
            return
            [
                new TestTreeNode
                {
                    Value = "Fruits",
                    Children =
                    {
                        new TestTreeNode { Value = "Apple" },
                        new TestTreeNode { Value = "Banana" },
                        new TestTreeNode
                        {
                            Value = "Citrus",
                            Children =
                            {
                                new TestTreeNode { Value = "Orange" },
                                new TestTreeNode { Value = "Lemon" }
                            }
                        }
                    }
                },
                new TestTreeNode
                {
                    Value = "Vegetables",
                    Children =
                    {
                        new TestTreeNode { Value = "Carrot" },
                        new TestTreeNode { Value = "Potato" }
                    }
                }
            ];
        }
    }
}
