using System.Collections.Generic;

namespace SamSkypeAnalizator
{
    public class MockBbaseRepository
    {
        private IEnumerable<string> _mockUsers = new List<string>()
               { "Vadzim Papko", "Victor Klyuenkov", "Dmitry Veresov", "Ledyaev Dmitry", "Aleksey Starchenko", "Alexander Dunaev", "Boris Burenkov", "Vasy Pupkin", "Vandam Ellipse", "Jose Mourinho"};

        public IEnumerable<string> GetUsers()
        {
            return _mockUsers;
        }
    }
}
