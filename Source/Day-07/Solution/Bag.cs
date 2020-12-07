namespace Day7
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    public class Bag
    {
        private Dictionary<string, (Bag Bag, int Count)> storableIn = new Dictionary<string, (Bag Bag, int Count)>();
        private Dictionary<string, (Bag Bag, int Count)> containableBags = new Dictionary<string, (Bag Bag, int Count)>();

        public string Name { get; init; }

        public ReadOnlyDictionary<string, (Bag Bag, int Count)> ContainableBags => new ReadOnlyDictionary<string, (Bag Bag, int Count)>(containableBags);

        public ReadOnlyDictionary<string, (Bag Bag, int Count)> StorableInBags => new ReadOnlyDictionary<string, (Bag Bag, int Count)>(containableBags);

        public void AddContainableBag(int count, Bag other)
        {
            this.containableBags[other.Name] = (other, count);
            other.storableIn[this.Name] = (this, count);
        }

        public IEnumerable<Bag> GetAllStorableIn()
        {
            return this.storableIn.Values.SelectMany(c => c.Bag.GetAllStorableIn()).Concat(storableIn.Values.Select(v => v.Bag)).Distinct();
        }
    }
}
