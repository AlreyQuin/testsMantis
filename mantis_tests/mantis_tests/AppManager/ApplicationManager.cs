using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace mantis_tests
{
    public class ApplicationManager
    {
        protected IWebDriver driver;
        protected StringBuilder verificationErrors;
        protected string baseURL;

        private static ThreadLocal<ApplicationManager> app = new ThreadLocal<ApplicationManager>();

        private ApplicationManager()
        {
            FirefoxOptions options = new FirefoxOptions();
            options.BrowserExecutableLocation = @"c:\Program Files\Mozilla Firefox\firefox.exe";
            options.UseLegacyImplementation = true;
            driver = new FirefoxDriver(options);
            baseURL = "http://localhost/mantisbt-2.15.0/";
            RegistHelper = new RegistrationHelper(this);
            Ftp = new FtpHelper(this);
            Logon = new LoginHelper(this);
            Project = new ProjectHelper(this, baseURL);
            James = new JamesHelper(this);
            Admin = new AdminHelper(this, baseURL);
            Api = new APIHelper(this);

            verificationErrors = new StringBuilder();

        }

        ~ApplicationManager()
        {
            try
            {
                driver.Quit();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
        }


        public static ApplicationManager GetInstance()
        {
            if (! app.IsValueCreated)
            {
                ApplicationManager newInstance = new ApplicationManager();
                newInstance.driver.Url = newInstance.baseURL + "login_page.php";
                app.Value = newInstance;
            }
            return app.Value;
        }

        public IWebDriver Driver
        {
            get
            { return driver; }
        }

        public RegistrationHelper RegistHelper { get; set; }

        public FtpHelper Ftp { get; set; }

        public LoginHelper Logon { get; set; }

        public ProjectHelper Project { get; set; }

        public JamesHelper James { get; set; }

        public AdminHelper Admin { get; set; }

        public APIHelper Api { get; set; }
    }
}
