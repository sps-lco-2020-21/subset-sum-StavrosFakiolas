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
            List<int> pog = new List<int> { 4, 10, 6, 17, 43, 7, 1, 9, 85, 10};
            foreach(int i in pog)
            {
                Console.WriteLine(i);
            }
            int target = 29; //breaks on 39? can you even sum that?
            Console.ReadKey(); // numbers larger than like 43 start to break real fast i dont know why
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
            if(original.Count == 0)
            {
                Console.WriteLine("Solution not found");
                return new List<int>{ 0 };
            }
            List<int> resultList = new List<int>();
            while (result != target)
            {
                if (numsStack.Count == 0)
                {
                    List<int> correct = Step3Induction(original, target);
                    return correct;
                }
                result = result + numsStack.Peek();
                if (result < target)
                {
                    int help = numsStack.Pop();
                    resultList.Add(help);
                }
                else if (result > target)
                {
                    int help2 = numsStack.Pop();
                    result = result - help2;
                }
            }
            int help3 = numsStack.Pop();
            resultList.Add(help3);
            return resultList;
        }
    }
}
