using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SumsOfSets.App
{ // 13-38
    class Program
    {
        static void Main(string[] args)
        {
            List<int> pog = new List<int> { 43, 8, 7, 6 };
            foreach (int i in pog)
            {
                Console.WriteLine(i);
            }
            int target = 56; 
            Console.ReadKey(); 
            List<int> pog2 = Step1(pog, target);
            Console.WriteLine("\n");
            foreach(int i in pog2)
            {
                Console.WriteLine(i);
            }
            Console.ReadKey();
            List<int> pog3 = Step2(pog2, target);
            Console.WriteLine("\n");
            foreach (int i in pog3)
            {
                Console.WriteLine(i);
            }
            Console.ReadKey();
        }

        static List<int> Step1(List<int> nums, int target) //removes all numbers larger than target, first checks if there is a number equal
        {
            if (nums.Contains(target))
            {
                return new List<int> { target };
            }
            //Remove all numbers larger than target using LINQ
            List<int> removals = nums.Where(x => x < target).ToList();
            return removals;
        }

        static List<int> Step2(List<int> nums, int target)
        {
            Stack<int> original = new Stack<int>();
            Stack<int> numsStack = new Stack<int>();
            nums.Sort();
            for(int i = 0; i < nums.Count; ++i)
            {
                numsStack.Push(nums[i]);
                original.Push(nums[i]);

            }
            original.Pop();
            int result = 0;
            List<int> resultList = new List<int>();
            while(result != target)
            {
                if (numsStack.Count == 0)
                {
                    List<int> correct = Step3Induction(original, target);
                    return correct;
                }
                result = result + numsStack.Peek();
                if(result < target)
                {
                    int help = numsStack.Pop();
                    resultList.Add(help);
                    if (numsStack.Count >= 2 && numsStack.Count < original.Count) //stack big enough for more combinations to need to be adressed -second case?
                    {
                        Stack<int> temp = numsStack;
                        List<int> empty = new List<int> { 0 };
                        temp.Push(result - help);
                        List<int> momentOfTruth = Step3Induction(temp, target);
                        if (momentOfTruth != empty)
                        {
                            return momentOfTruth;
                        }
                    }
                }
                else if(result > target)
                {
                    int help2 = numsStack.Pop();
                    result = result - help2;
                }
                
            }
            int help3 = numsStack.Pop();
            resultList.Add(help3);
            return resultList;
            
        }

        static List<int> Step3Induction(Stack<int> numsStack, int target)
        {
            int result = 0;
            Stack<int> original = numsStack;
            if(numsStack.Count == 0)
            {
                return new List<int>{ 0 };
            }
            List<int> resultList = new List<int>();
            while (result != target)
            {
                if (original.Count == 0)
                {
                    List<int> correct = Step3Induction(numsStack, target);
                    return correct;
                }
                result = result + original.Peek();
                if (result < target)
                {
                    int help = original.Pop();
                    resultList.Add(help);
                    if (original.Count >= 2 && original.Count < numsStack.Count) //stack big enough for more combinations to need to be adressed -second case?
                    {
                        Stack<int> temp = original;
                        List<int> empty = new List<int> { 0 };
                        temp.Push(result - help);
                        List<int> momentOfTruth = Step3Induction(temp, target);
                        if (momentOfTruth != empty)
                        {
                            return momentOfTruth;
                        }
                    }

                }
                else if (result > target)
                {
                    int help2 = original.Pop();
                    result = result - help2;
                }
            }
            int help3 = original.Pop();
            resultList.Add(help3);
            return resultList;
        }
    }
}
