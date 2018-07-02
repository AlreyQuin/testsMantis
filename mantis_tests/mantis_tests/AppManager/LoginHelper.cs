using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace mantis_tests
{
    public class LoginHelper : HelperBase
    {

        public LoginHelper(ApplicationManager manager) 
            : base(manager)
        {}

        public void Login(AccountData account)
        {
            if (IsLoggedIn())
            {
                if (IsLoggedIn(account))
                {
                    return;
                }

                Logout();
            }
            LoginForm(account);
        }

        private void LoginForm(AccountData account)
        {
            Type(By.Name("username"), account.Name);
            driver.FindElement(By.CssSelector("input[value='Войти'] ")).Click();
            Type(By.Name("password"), account.Pass);
            driver.FindElement(By.CssSelector("input[value='Войти'] ")).Click();
        }

        public void Logout()
        {
            if (IsLoggedIn())
            {
                driver.FindElement(By.CssSelector("span[class='user-info']")).Click();
                driver.FindElement(By.CssSelector("ul[class='nav ace-nav'] > li[class='grey open'] > ul > li:nth-child(4) > a")).Click();
            }
        }

        public bool IsLoggedIn()
        {
            return IsElementPresent(By.CssSelector("span[class='user-info']"));
        }

        public bool IsLoggedIn(AccountData account)
        {
            return IsLoggedIn()
                && GetLoggetUserName() == account.Name;
        }

        private string GetLoggetUserName()
        {
            string text = driver.FindElement(By.CssSelector("span[class='user-info']")).Text;
            return text;
                
        }
    }
}
