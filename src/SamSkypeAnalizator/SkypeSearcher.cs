using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SamSkypeAnalizator
{
    public class SkypeSearcher
    {
        private readonly ChromeDriver _driver;
        private const string _skypeUrl = "https://web.skype.com/";

        public SkypeSearcher(string pathToBrowserSeetings)
        {
            var curDir = AppDomain.CurrentDomain.BaseDirectory;
            var options = new ChromeOptions();
            options.AddArgument(pathToBrowserSeetings);
            _driver = new ChromeDriver(curDir, options);
        }

        public IEnumerable<string> FindGroupMembers(string groupTitle)
        {
            _driver.Manage().Window.Maximize();
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);
            _driver.Navigate().GoToUrl(_skypeUrl);

            var groups = _driver.FindElementsByCssSelector(".rxCustomScroll.rxCustomScrollV.active div[role = button]");
            IWebElement findedGroup = null;

            foreach (var group in groups)
            {
                var title = group.GetAttribute("aria-label");
                if (title.IndexOf(groupTitle) >= 0)
                {
                    findedGroup = group;
                    break;
                }
            }

            findedGroup.Click();

            var membersButton = _driver.FindElementByCssSelector("div[data-text-as-pseudo-element$='участник']");
            membersButton.Click();

            var membersAdditionalButton = _driver.FindElementByCssSelector("div[data-text-as-pseudo-element$='Дополнительно']");
            membersAdditionalButton.Click();

            var membersHtmlElements = _driver.FindElementsByCssSelector("div[role=group] > div[role=button]");
            var members = membersHtmlElements.Select(m => m.GetAttribute("aria-label").Replace(", ", string.Empty)).ToList();

            _driver.Close();
            return members;
        }
    }
}
