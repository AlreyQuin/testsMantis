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
    public class RegistrationHelper : HelperBase
    {
        public RegistrationHelper(ApplicationManager manager) : base(manager) { }

        public void RegistHelper(AccountData account)
        {
            OpenMainPage();
            OpenRegistrationForm();
            FillRegictrationForm(account);
            SubmitRegistration();

        }

        void OpenRegistrationForm()
        {
            driver.FindElement(By.CssSelector("div[class='widget-body'] > [class='toolbar center'] > a")).Click();
        }

        public void SubmitRegistration()
        {
            driver.FindElement(By.CssSelector("input[type='submit']")).Click();
        }

        public void FillRegictrationForm(AccountData account)
        {
            Type(By.Name("username"), account.Name);
            Type(By.Name("email"), account.Email);
        }

        public void OpenMainPage()
        {
            manager.Driver.Url = "http://localhost/mantisbt-2.15.0/login_page.php";
        }
    }
}
