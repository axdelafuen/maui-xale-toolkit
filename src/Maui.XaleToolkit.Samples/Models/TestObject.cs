namespace Maui.XaleToolkit.Samples.Models
{
    public class TestObject
    {
        public required string Text { get; set; }
        public required int Value { get; set; }
        public override string ToString() => Text;

        public override bool Equals(object? obj)
        {
            if (obj is TestObject toCompare)
                return Text == toCompare.Text && Value == toCompare.Value;
            else
                return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
