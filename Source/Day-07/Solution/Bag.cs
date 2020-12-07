namespace Day7
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    public class Bag
    {
        public string Name { get; init; }

        public Dictionary<string, (Bag Bag, int Count)> ContainableBags { get; } = new Dictionary<string, (Bag Bag, int Count)>();

        public Dictionary<string, Bag> StorableInBags { get; } = new Dictionary<string, Bag>();

        public void AddContainableBag(int count, Bag other)
        {
            this.ContainableBags[other.Name] = (other, count);
            other.StorableInBags[this.Name] = this;
        }

        public int GetAllStorableIn()
        {
            var count = 0;
            HashSet<Bag> encounteredBags = new HashSet<Bag>();
            Stack<Bag> bagsToProcess = new Stack<Bag>();
            foreach(var value in this.StorableInBags.Values)
            {
                if (encounteredBags.Add(value))
                {
                    bagsToProcess.Push(value);
                }
            }

            while(bagsToProcess.Count > 0)
            {
                var bag = bagsToProcess.Pop();
                count++;
                foreach (var child in bag.StorableInBags.Values)
                {
                    if (encounteredBags.Add(child))
                    {
                        bagsToProcess.Push(child);
                    }
                }
            }

            return count;
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
