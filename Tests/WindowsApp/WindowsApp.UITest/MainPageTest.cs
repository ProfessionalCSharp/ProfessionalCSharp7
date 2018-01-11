using System;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UITest.Input;
using Microsoft.VisualStudio.TestTools.UITest.Extension;
using Microsoft.VisualStudio.TestTools.UITesting.HtmlControls;
using Microsoft.VisualStudio.TestTools.UITesting.DirectUIControls;
using Microsoft.VisualStudio.TestTools.UITesting.WindowsRuntimeControls;
using Keyboard = Microsoft.VisualStudio.TestTools.UITesting.Keyboard;


namespace WindowsApp.UITest
{
    [CodedUITest(CodedUITestType.WindowsStore)]
    public class MainPageTest
    {
        private string TileAutomationId = "0e07ecab-af0f-4129-965b-eed7a5beef75_p2wxv0ry6mv8g!App";
        public MainPageTest()
        {
        }

        [TestMethod]
        public void EnterTextAndButtonClick()
        {
            string inText = "Hello, Windows!";
            XamlWindow xamlWindow = XamlWindow.Launch(TileAutomationId);
            UIMap.UIWindowsAppWindow.UITextInEdit.Text = inText;
            Gesture.Tap(UIMap.UIWindowsAppWindow.UIClickMeButton);
            string outText = UIMap.UIWindowsAppWindow.UITextOutText.DisplayText;
            xamlWindow.Close();
            Assert.AreEqual(inText, outText);
        }

        #region Additional test attributes

        // You can use the following additional attributes as you write your tests:

        ////Use TestInitialize to run code before running each test 
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{        
        //    // To generate code for this test, select "Generate Code for Coded UI Test" from the shortcut menu and select one of the menu items.
        //}

        ////Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{        
        //    // To generate code for this test, select "Generate Code for Coded UI Test" from the shortcut menu and select one of the menu items.
        //}

        #endregion

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get => testContextInstance;
            set => testContextInstance = value;
        }
        private TestContext testContextInstance;

        public UIMap UIMap
        {
            get
            {
                if (this.map == null)
                {
                    this.map = new UIMap();
                }

                return this.map;
            }
        }

        private UIMap map;
    }
}
