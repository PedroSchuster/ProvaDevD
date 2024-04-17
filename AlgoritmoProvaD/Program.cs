using System.Collections;

namespace AlgoritmoProvaD
{
    class Program
    {
        /*
         * The Main function is the entry point of the program, that is going to read the input and call the MakeChange method
         * The MakeChange function is going to initialize the coins array and the results Set, and call the MakeChangeHelper method to start the recursion
         * On the MakeChangeHelper:
         * I set the base case to when the amount is 0, I add the currentCombination to the results Set and return to the previous call
         * Another possibility of base case is when the coinIndex is equal to the length of the coins array, in this case, I just return to the previous call
         * Then I interate over the amount divided by the current coin value, and call the MakeChangeHelper recursively with the amount updated and the coinIndex increased by 1
         * Then I set the currentCombination to 0, to be able to use it in the next iteration
         * When thre recursion ends, I return the results Set
         * Back to the main function, I iterate over the results Set and print the combinations
         * Exemple of the output:
         * 12
                0 0 0 12
                0 0 1 7
                0 0 2 2
                0 1 0 2

         */
        static void Main(string[] args)
        {
            int num = int.Parse(Console.ReadLine());
            Set<List<int>> change = MakeChange(num);
            foreach (var item in change)
            {
                foreach (var coin in item)
                {
                    Console.Write(coin + " ");
                }
                Console.WriteLine();
            }
        }

        static Set<List<int>> MakeChange(int n)
        {
            int[] coins = { 25, 10, 5, 1 };
            Set<List<int>> results = new Set<List<int>>();
            List<int> currentCombination = new List<int>(new int[4]);
            MakeChangeHelper(n, 0, currentCombination, coins, results);
            return results;
        }

        static void MakeChangeHelper(int amount, int coinIndex, List<int> currentCombination, int[] coins, Set<List<int>> results)
        {
            if (amount == 0)
            {
                results.Add(new List<int>(currentCombination));
                return;
            }

            if (coinIndex == coins.Length)
            {
                return;
            }

            for (int i = 0; i <= amount / coins[coinIndex]; i++)
            {
                currentCombination[coinIndex] = i;
                MakeChangeHelper(amount - i * coins[coinIndex], coinIndex + 1, currentCombination, coins, results);
                currentCombination[coinIndex] = 0; 
            }
        }
    }

    class Set<T> : IEnumerable<T>
    {
        private ICollection<T> Items { get; set; } = new List<T>();
        public Set()
        {
        }
        public Set(ICollection<T> values)
        {
            Items = values;
        }
        public bool Add(T item)
        {
            if (Items.Contains(item))
            {
                return false;
            }
            else
            {
                Items.Add(item);
                return true;
            }
        }

        public bool AddAll(Set<T> items)
        {
            bool result = false;
            foreach (var item in items)
            {
                if (Items.Contains(item))
                {
                    result = false;
                    break;
                }
                else
                {
                    Items.Add(item);
                    result = true;
                }
            }
            return result;
        }

        public bool Cointains(T item)
        {
            return Items.Contains(item);
        }

        public bool Equals(Set<T> items)
        {
            return Items.Equals(items);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)Items).GetEnumerator();
        }
        public bool Remove(T item)
        {
            if (Items.Contains(item))
            {
                Items.Remove(item);
                return true;
            }
            else
            {
                return false;
            }
        }
        public int Size()
        {
            return Items.Count;
        }

        public T[] ToArray()
        {
            return Items.ToArray();
        }


    }

}
