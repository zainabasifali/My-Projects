using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.MiscClass
{
    public static class LocatorClass
    {
        public const string loginNav = "#login2";
        public const string userName = "#loginusername";
        public const string password = "#loginpassword";
        public const string loginButton = "//button[text()='Log in']";
        public const string loginSuccess = "#nameofuser";
        public const string homeNav = "//*[@id=\"navbarExample\"]/ul/li[1]/a";


        public const string signUpNav = "#signin2";
        public const string SignupUsernameInput = "#sign-username";
        public const string SignupPasswordInput = "#sign-password"; 
        public const string SignupSubmitButton = "//button[text()='Sign up']";


        public const string contactNav = "//a[normalize-space()='Contact']"; 
        public const string ContactEmailInput = "#recipient-email"; 
        public const string ContactNameInput = "#recipient-name";
        public const string ContactMessageTextarea = "#message-text";
        public const string ContactSendMessageButton = "//*[@id='exampleModal']//button[text()='Send message']";


        public const string FirstProductLink = "//a[contains(text(),'Samsung galaxy s6')]";
        public const string SecondProductLink = "//*[@id=\"tbodyid\"]/div[2]/div/a/img";
        public const string AddToCartButton = "//a[text()='Add to cart']";

        public const string cartNav = "#cartur";
        public const string placeOrderButton = "//button[text()='Place Order']";

        public const string orderModal = "#orderModal";
        public const string orderNameInput = "#name";
        public const string orderCountryInput = "#country";
        public const string orderCityInput = "#city";
        public const string orderCardInput = "#card";
        public const string orderMonthInput = "#month";
        public const string orderYearInput = "#year";
        public const string purchaseButton = "//button[text()='Purchase']";

        public const string confirmationPopup = ".sweet-alert.showSweetAlert.visible";
        public const string confirmationMessage = ".sweet-alert.showSweetAlert.visible h2";
        public const string confirmationOKButton = ".confirm.btn.btn-lg.btn-primary";

        public const string TotalAmountinCart = "#totalp";
        public const string TotalAmountinOrderConfirmation = "//html/body/div[10]/p";

        public const string categoryId = "//a[3]";
        public const string categoryLaptop = "//a[normalize-space()='Sony vaio i5']";

        public const string logoutNav = "//a[@id='logout2']";


    }
}
