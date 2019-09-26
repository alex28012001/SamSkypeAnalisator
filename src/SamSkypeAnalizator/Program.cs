using System;
using System.Linq;
using System.Collections.Generic;

namespace SamSkypeAnalizator
{
    class Program
    {
        static void Main(string[] args)
        {
            var skypeSearcher = new SkypeSearcher(@"user-data-dir=C:\Users\alest\AppData\Local\Google\Chrome\User Data");
            var bbaseRepository = new MockBbaseRepository();

            var skypeGroupMembers = skypeSearcher.FindGroupMembers("SaM-Solutions .Net");
            var bbaseUsers = bbaseRepository.GetUsers();

            var addingToSkypeGroup = bbaseUsers.Except(skypeGroupMembers);
            var deletingFromSkypeGroup = skypeGroupMembers.Except(bbaseUsers);

            OutputLists(addingToSkypeGroup, deletingFromSkypeGroup);
            Console.ReadKey();
        }

        private static void OutputLists(IEnumerable<string> addingToSkypeGroup, IEnumerable<string> deletingFromSkypeGroup)
        {
            Console.WriteLine("List of users which need to adding:" + Environment.NewLine);
            foreach (var toAdding in addingToSkypeGroup)
            {
                Console.WriteLine(toAdding);
            }

            Console.WriteLine(Environment.NewLine + "-------------------------------------" + Environment.NewLine);
            Console.WriteLine("List of users which need to deleting:" + Environment.NewLine);
            foreach (var toDeleting in deletingFromSkypeGroup)
            {
                Console.WriteLine(toDeleting);
            }
        }
    }
}
