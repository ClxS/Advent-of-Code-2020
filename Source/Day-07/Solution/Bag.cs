namespace Day7
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    public class Bag
    {
        public string Name { get; init; }

        public Dictionary<string, (Bag Bag, int Count)> ContainableBags { get; } = new Dictionary<string, (Bag Bag, int Count)>();

        public Dictionary<string, (Bag Bag, int Count)> StorableInBags { get; } = new Dictionary<string, (Bag Bag, int Count)>();

        public void AddContainableBag(int count, Bag other)
        {
            this.ContainableBags[other.Name] = (other, count);
            other.StorableInBags[this.Name] = (this, count);
        }

        public IEnumerable<Bag> GetAllStorableIn()
        {
            foreach(var bag in this.StorableInBags.Values)
            {
                yield return bag.Bag;
                foreach(var child in bag.Bag.GetAllStorableIn())
                {
                    yield return child;
                }
            }

            //return this.StorableInBags.Values.SelectMany(c => c.Bag.GetAllStorableIn()).Concat(StorableInBags.Values.Select(v => v.Bag)).Distinct();
        }

        public int GetContainable()
        {
            var sum = 0;
            foreach(var bag in this.ContainableBags.Values)
            {
                sum += bag.Bag.GetContainable() * bag.Count + bag.Count;
            }

            return sum;
        }
    }
}
