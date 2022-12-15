using System.Linq;
using System.Net.Sockets;

namespace _13
{
    public class Item { }

    public class ItemList : Item
    {
        public List<Item> Items = new List<Item>();
        public ItemList Parent = null;

        public override string ToString()
        {
            var str = "[";
            foreach (var item in Items)
            {
                str += item.ToString();
            }
            str += "]";
            return str;
        }
    }

    public class ItemNum : Item
    {
        public int Value;

        public override string ToString()
        {
            return Value.ToString();
        }
    }




    public class Processor : IComparer<ItemList>
    {
        public List<Tuple<ItemList, ItemList>> Packets = new List<Tuple<ItemList, ItemList>>();
        public int Result = 0;
        public int MarkerResult = 0;

        public Processor(string filePath)
        {
            var packetsStr = ParseFile(filePath);
            ProcessPackets(packetsStr);
        }

        private List<Tuple<string, string>> ParseFile(string filePath)
        {
            List<Tuple<string, string>> packets = new List<Tuple<string, string>>();
            var lines = Utilities.FileHelper.GetFileAsRows(filePath, true);
            for (int i = 0; i < lines.Count; i++)
            {
                string? line = lines[i];
                if (line == string.Empty)
                {
                    packets.Add(Tuple.Create(lines[i-2], lines[i-1]));
                }
            }
            Console.WriteLine($"Packets: {Packets.Count}");
            return packets;
        }


        private void ProcessPackets(List<Tuple<string, string>> PacketStrings)
        {
            int sumOfIndexes = 0;
            var list = new List<ItemList>();
            for (int i = 0; i < PacketStrings.Count; i++)
            {
                Tuple<string, string>? packet = PacketStrings[i];
                Console.WriteLine($"{Environment.NewLine}Evaluating:{Environment.NewLine}{packet.Item1}{Environment.NewLine}{packet.Item2}{Environment.NewLine} ");
                var left = ProcessLineToLists(packet.Item1);
                list.Add(left);
                var right = ProcessLineToLists(packet.Item2);
                list.Add(right);
                Packets.Add(Tuple.Create<ItemList, ItemList>(left, right));
                var result = CompareAndScore(left, right);
                if (result.HasValue && result.Value)
                {
                    sumOfIndexes += (i + 1);
                }
            }

            // add divider packets
            list.Add(ProcessLineToLists("[[2]]"));
            list.Add(ProcessLineToLists("[[6]]"));

            list.Sort(this);
            Result = sumOfIndexes;


            Console.WriteLine("########### Sorted List: ");
            int firstMarker = 0, secondMarker = 0;
            for (int i = 0; i < list.Count; i++)
            {
                ItemList? item = list[i];
                Console.WriteLine(item.ToString() + Environment.NewLine);

                if (item.Items.Count == 1
                    && item.Items[0] is ItemList)
                {
                    var childCol = ((ItemList)item.Items[0]);
                    if (childCol.Items.Count == 1
                        && childCol.Items[0] is ItemNum)
                    {
                        int val = ((ItemNum)childCol.Items[0]).Value;
                        if (val == 2)
                            firstMarker = (i+1);
                        if (val == 6)
                            secondMarker = (i + 1);
                    }

                }
            }
            MarkerResult = firstMarker * secondMarker;
        }

        private bool? CompareAndScore(ItemList left, ItemList right)
        {
            int minSize = (left.Items.Count < right.Items.Count) ? left.Items.Count : right.Items.Count;
            for (int i = 0; i < minSize; i++)
            {
                // Both numbers
                if(left.Items[i] is ItemNum && right.Items[i] is ItemNum)
                {
                    Console.WriteLine($"Comparing numbers l:{left.Items[i]}, r: {right.Items[i]}");
                    bool? res = CompareAsInt(((ItemNum)left.Items[i]).Value, ((ItemNum)right.Items[i]).Value);
                    if(res.HasValue)
                    {
                        return res.Value;
                    }
                    // else same, so continue
                } 
                else if (left.Items[i] is ItemList && right.Items[i] is ItemList)
                {
                    Console.WriteLine($"Comparing two lists");
                    var res = CompareAndScore((ItemList)left.Items[i], (ItemList)right.Items[i]);
                    if (res.HasValue)
                    {
                        return res.Value;
                    }
                    // else same, so continue
                }
                else if (left.Items[i] is ItemNum)
                {
                    Console.WriteLine($"Mixed, converting left to list");
                    // mixed, so convert int to list
                    var res = CompareAndScore(
                        new ItemList() { Items = new List<Item> { left.Items[i] }, Parent = left },
                        (ItemList)right.Items[i]);

                    if (res.HasValue)
                    {
                        return res.Value;
                    }
                }
                else if (right.Items[i] is ItemNum)
                {
                    Console.WriteLine($"Mixed converting right to list");
                    // mixed, so convert int to list
                    var res = CompareAndScore(
                        (ItemList)left.Items[i],
                        new ItemList() { Items = new List<Item> { right.Items[i] }, Parent = left });

                    if (res.HasValue)
                    {
                        return res.Value;
                    }
                }
            }
            // if we have compared both lists, but they are the same and we have made it this far
            // now check the length of each list. 
            Console.WriteLine($"Comparing list sizes. l:{left.Items.Count}, r:{right.Items.Count}");
            if (left.Items.Count == right.Items.Count)
                return null;

            return left.Items.Count < right.Items.Count;
        }

        private bool? CompareAsInt(int left, int right)
        {
            if (left == right) return null;
            return left < right;
        }

        private ItemList ProcessLineToLists(string line)
        {
            ItemList currentList = new ItemList();
            string buffer = string.Empty;

            if (line.Length <= 0 || line[0] != '[')
            {
                throw new ArgumentException("Unexpected start of line");
            }

            for (int i = 1; i < line.Length; i++) // start at 1 as we have already processed '['
            {
                char c = line[i];

                if (c == '[')
                {
                    // if [ create new list, down level
                    var newItem = new ItemList() { Parent = currentList };
                    currentList.Items.Add(newItem);
                    currentList = newItem;
                }
                else if (c == ']')
                {
                    // if ] up level
                    if(buffer.Length > 0)
                    {
                        var val = int.Parse(buffer);
                        currentList.Items.Add(new ItemNum { Value = val });
                        buffer = string.Empty;
                    }
                    if(i != line.Length-1)
                        currentList = currentList.Parent;
                }
                else if (c == ',')
                {
                    // if ',' and buffer > 0, parse int, put in list of ints
                    if (buffer.Length > 0)
                    {
                        var val = int.Parse(buffer);
                        currentList.Items.Add(new ItemNum { Value = val });
                        buffer = string.Empty;
                    }
                }
                else
                {
                    // if int keep reading into buffer
                    buffer += c;
                }
            }
            return currentList;
        }

        public int Compare(ItemList? x, ItemList? y)
        {
            if(x == null || y == null) return -1;
            var result = CompareAndScore(x, y);
            if (!result.HasValue) return 0;
            if (result.Value)
                return -1;
            else
                return 1;
        }
    }
}