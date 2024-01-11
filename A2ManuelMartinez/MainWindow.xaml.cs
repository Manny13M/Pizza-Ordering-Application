using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace A2ManuelMartinez
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        //Order? order;
        double orderTotalAmnt = 0; //Before Tax
        double orderTotalAmntAfterTax;

        List<Order> ordersArr = new List<Order>();
        List<object> combinedItems = new List<object>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnConfirmOrder_Click(object sender, RoutedEventArgs e)
        {
            orderTbl.ItemsSource = null;

            List<Pizza> pizzaArr = new List<Pizza>();
            createPizzaObj(pizzaArr);

            List<Topping> toppingsArr = new List<Topping>();
            populateToppingsArr(toppingsArr);

            List<OtherItem> otherItemsArr = new List<OtherItem>();
            populateOtherItemsList(otherItemsArr);

            List<Drink> drinksArr = new List<Drink>();
            populateDrinksList(drinksArr);

            Order? newOrder = new Order(pizzaArr, otherItemsArr, drinksArr, toppingsArr);
            ordersArr.Add(newOrder);
            
            double hst = orderTotalAmnt * 0.13;
            orderTotalAmntAfterTax = orderTotalAmnt + hst;

            amntB4TaxTxtBox.Text = Math.Round(orderTotalAmnt, 2).ToString();
            hstTxtBox.Text = Math.Round(hst, 2).ToString();
            amntTotalTxtBox.Text = Math.Round((orderTotalAmntAfterTax), 2).ToString();

            MessageBox.Show((ordersArr.Count.ToString()));
            combinedItems.Clear();
            
            foreach (var order in ordersArr)
            {
                foreach (var pizza in order.Pizzas)
                {
                    combinedItems.Add(pizza);
                }

                foreach (var topping in order.Toppings)
                {
                    combinedItems.Add(topping);
                }

                foreach (var drink in order.Drinks)
                {
                    combinedItems.Add(drink);
                }

                foreach (var otherItem in order.OtherItems)
                {
                    combinedItems.Add(otherItem);
                }
            }



            orderTbl.ItemsSource = combinedItems;

            tabControl.SelectedIndex = 1;
        }

        private void createCustomer()
        {

            if (!string.IsNullOrEmpty(fnameTxtBox.Text) && !string.IsNullOrEmpty(lnameTxtBox.Text) && !string.IsNullOrEmpty(addressTxtBox.Text) &&
                !string.IsNullOrEmpty(postalCodeTxtBox.Text) &&
                int.TryParse(contactNoTxtBox.Text, out int contactNo) &&
                !string.IsNullOrEmpty(paymentMthdCmbBox.Text) &&
                int.TryParse(cardNoTxtBox.Text, out int cardNo) &&
                double.TryParse(amntPaidTxtBox.Text, out double amntPaid)
                )
            {
                Customer customer = new Customer(fnameTxtBox.Text, lnameTxtBox.Text, addressTxtBox.Text, provinceTxtBox.Text,
                    cityTxtBox.Text, postalCodeTxtBox.Text, contactNo, emailTxtBox.Text, paymentMthdCmbBox.Text, cardNo, double.Parse(amntDueTxtBox.Text), amntPaid);

                MessageBox.Show("Payment Success!");
                MessageBox.Show("Approximate wait time is 20 minutes!");
            }
            else
            {
                MessageBox.Show($"Please ensure the following criteria is met.\nAll field with an asteric (*) are filled in.\nThe Contact No, Card No, and Amount Paid field are number values.");
            }

        }

        private void populateDrinksList(List<Drink> drinksArr)
        {
            double sodaPrice = 2.99;
            double waterPrice = 1.99;

            if (int.TryParse(cokeTxtBox.Text, out int cokeAmnt))
            {
                Drink drink = new Drink("Coke", cokeAmnt, sodaPrice);
                drinksArr.Add(drink);
                orderTotalAmnt += sodaPrice * cokeAmnt;
            }

            if (int.TryParse(dietCokeTxtBox.Text, out int dietCokeAmnt))
            {
                Drink drink = new Drink("Diet Coke", dietCokeAmnt, sodaPrice);
                drinksArr.Add(drink);
                orderTotalAmnt += sodaPrice * dietCokeAmnt;
            }

            if (int.TryParse(icedTeaTxtBox.Text, out int icedTeaAmnt))
            {
                Drink drink = new Drink("Iced Tea", icedTeaAmnt, sodaPrice);
                drinksArr.Add(drink);
                orderTotalAmnt += sodaPrice * icedTeaAmnt;
            }

            if (int.TryParse(gingerAleTxtBox.Text, out int gingerAleAmnt))
            {
                Drink drink = new Drink("Ginger Ale", gingerAleAmnt, sodaPrice);
                drinksArr.Add(drink);
                orderTotalAmnt += sodaPrice * gingerAleAmnt;
            }

            if (int.TryParse(spriteTxtBox.Text, out int spriteAmnt))
            {
                Drink drink = new Drink("Sprite", spriteAmnt, sodaPrice);
                drinksArr.Add(drink);
                orderTotalAmnt += sodaPrice * spriteAmnt;
            }

            if (int.TryParse(rootBeerTxtBox.Text, out int rootBeerAmnt))
            {
                Drink drink = new Drink("Root Beer", rootBeerAmnt, sodaPrice);
                drinksArr.Add(drink);
                orderTotalAmnt += sodaPrice * rootBeerAmnt;
            }

            if (int.TryParse(waterTxtBox.Text, out int waterAmnt))
            {
                Drink drink = new Drink("Water", waterAmnt, waterPrice);
                drinksArr.Add(drink);
                orderTotalAmnt += waterPrice * waterAmnt;
            }
        }

        private void populateOtherItemsList(List<OtherItem> otherItemsArr)
        {
            //Add checkbox Other Items to list
            foreach (var Control in otherItemsPanel.Children)
            {
                if (Control is CheckBox checkBox && checkBox.IsChecked == true) //If Control is of type checkbox, store in CheckBox type variable. 
                {
                    string item = checkBox.Content.ToString();

                    if (item.Contains("Garlic Dip"))
                    {
                        OtherItem otherItem = new OtherItem("Garlic Dip", 0.50);
                        otherItemsArr.Add(otherItem);
                        orderTotalAmnt += 0.50;
                    }
                    else if (item.Contains("BBQ Dip"))
                    {
                        OtherItem otherItem = new OtherItem("BBQ Dip", 0.50);
                        otherItemsArr.Add(otherItem);
                        orderTotalAmnt += 0.50;
                    }
                    else if (item.Contains("Sour Cream"))
                    {
                        OtherItem otherItem = new OtherItem("Sour Cream", 0.50);
                        otherItemsArr.Add(otherItem);
                        orderTotalAmnt += 0.50;
                    }
                    else if (item.Contains("Cheesy Garlic Bread"))
                    {
                        OtherItem otherItem = new OtherItem("Cheesy Garlic Bread", 5.69);
                        otherItemsArr.Add(otherItem);
                        orderTotalAmnt += 5.69;
                    }
                    else if (item.Contains("Poutine"))
                    {
                        OtherItem otherItem = new OtherItem("Poutine", 5.29);
                        otherItemsArr.Add(otherItem);
                        orderTotalAmnt += 5.29;
                    }
                }
            }

            //Add Other Items chichen wings to list
            if (chknWingsCmbBox.Text.Contains("5"))
            {
                OtherItem otherItem = new OtherItem("5 Wings", 6.99);
                otherItemsArr.Add(otherItem);
                orderTotalAmnt += 6.99;
            }
            else if (chknWingsCmbBox.Text.Contains("10"))
            {
                OtherItem otherItem = new OtherItem("10 Wings", 11.99);
                otherItemsArr.Add(otherItem);
                orderTotalAmnt += 11.99;
            }
            else if (chknWingsCmbBox.Text.Contains("20"))
            {
                OtherItem otherItem = new OtherItem("20 Wings", 22.99);
                otherItemsArr.Add(otherItem);
                orderTotalAmnt += 22.99;
            }

            //Add Other Items onion rings to list
            if (onionRngsCmbBox.Text.Contains("Small"))
            {
                OtherItem otherItem = new OtherItem("Small Onion Rings", 3.99);
                otherItemsArr.Add(otherItem);
                orderTotalAmnt += 3.99;
            }
            else if (onionRngsCmbBox.Text.Contains("Medium"))
            {
                OtherItem otherItem = new OtherItem("Medium Onion Rings", 5.99);
                otherItemsArr.Add(otherItem);
                orderTotalAmnt += 5.99;
            }
        }

        private void createPizzaObj(List<Pizza> pizzaArr)
        {
            string pizzaSize = pizzaSizeCmbBox.Text.ToString();
            double price = 0;

            if (pizzaSize.Contains("Small"))
            {
                price = 7.00;
                Pizza pizza = new Pizza($"Small Pizza - {crustTypeCmbBox.Text} crust", price);
                pizzaArr.Add(pizza);
            }
            else if (pizzaSize.Contains("Medium"))
            {
                price = 10.00;
                Pizza pizza = new Pizza($"Medium Pizza - {crustTypeCmbBox.Text} crust", price);
                pizzaArr.Add(pizza);
            }
            else if (pizzaSize.Contains("Extra Large"))
            {
                price = 15.00;
                Pizza pizza = new Pizza($"Extra Large Pizza - {crustTypeCmbBox.Text} crust", price);
                pizzaArr.Add(pizza);
            }
            else if (pizzaSize.Contains("Large"))
            {
                price = 13.00;
                Pizza pizza = new Pizza($"Large Pizza - {crustTypeCmbBox.Text} crust", price);
                pizzaArr.Add(pizza);
            }

            orderTotalAmnt += price;

        }

        private void populateToppingsArr(List<Topping> toppingsArr)
        {

            //Create veggie toppings obj for $1.10 each and add to toppings array
            foreach (var Control in veggiesToppingsPanel.Children)
            {
                if (Control is CheckBox checkBox && checkBox.IsChecked == true) //If Control is of type checkbox, store in CheckBox type variable. 
                {
                    Topping topping = new Topping(checkBox.Content.ToString(), 1.10);
                    toppingsArr.Add(topping);
                    orderTotalAmnt += 1.10;
                }
            }

            //Create meat toppings obj for $2.15 each and add to toppings array
            foreach (var Control in meatToppingsPanel.Children) //Loop though checkboxes
            {
                if (Control is CheckBox checkBox && checkBox.IsChecked == true) //If Control is of type checkbox, store in CheckBox type variable. 
                {
                    Topping topping = new Topping(checkBox.Content.ToString(), 2.15);
                    toppingsArr.Add(topping);
                    orderTotalAmnt += 2.15;
                }
            }
        }

        private void payBtn_Click(object sender, RoutedEventArgs e)
        {
            if (double.Parse(amntPaidTxtBox.Text) > double.Parse(amntDueTxtBox.Text))
            {
                createCustomer();
            }
            else 
            {
                MessageBox.Show("Please ensure that the payment covers the full amount due.");
            }
           
        }

        private void backBtn_Click(object sender, RoutedEventArgs e)
        {
            clearCustomerInfo();
            tabControl.SelectedIndex = 1;
        }

        private void clearCustomerInfo() 
        {
            foreach (var Control in paymentPanel.Children)
            {
                if (Control is TextBox textBox && !string.IsNullOrEmpty(textBox.Text.ToString())) 
                {
                    textBox.Text = "";
                }
                if (Control is ComboBox cmbBox && !string.IsNullOrEmpty(cmbBox.Text.ToString()))
                {
                    cmbBox.Text = "";
                }
            }
        }

        private void orderAgainBtn_Click(object sender, RoutedEventArgs e)
        {
            clearAllControls(placeOrderPanel);
            clearAllControls(veggiesToppingsPanel);
            clearAllControls(meatToppingsPanel);
            clearAllControls(otherItemsPanel);

            tabControl.SelectedIndex = 0;
        }

        private void clearOrderBtn_Click(object sender, RoutedEventArgs e)
        {
            orderTbl.ItemsSource = null;
            ordersArr.Clear();
            combinedItems.Clear();

            orderTotalAmnt = 0;
            amntB4TaxTxtBox.Text = "";
            hstTxtBox.Text = "";
            amntTotalTxtBox.Text = "";

            clearAllControls(placeOrderPanel);
            clearAllControls(veggiesToppingsPanel);
            clearAllControls(meatToppingsPanel);
            clearAllControls(otherItemsPanel);

            tabControl.SelectedIndex = 0;
        }

        private void clearAllControls(Panel panel) 
        {
            foreach (var Control in panel.Children)
            {
                if (Control is TextBox textBox && !string.IsNullOrEmpty(textBox.Text.ToString()))
                {
                    textBox.Text = "";
                }

                if (Control is ComboBox cmbBox && !string.IsNullOrEmpty(cmbBox.Text.ToString()))
                {
                    cmbBox.Text = "";
                }
                if (Control is CheckBox chkBox && chkBox.IsChecked == true)
                {
                    chkBox.IsChecked = false;
                }
            }
        }

        private void checkoutBtn_Click(object sender, RoutedEventArgs e)
        {
            amntDueTxtBox.Text = Math.Round(orderTotalAmntAfterTax, 2).ToString();
            tabControl.SelectedIndex = 2;
        }

        private void amntPaidTxtBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (double.TryParse(amntPaidTxtBox.Text, out double amntPaid) && amntPaid > double.Parse(amntDueTxtBox.Text)) 
            {
                changeTxtBox.Text = Math.Round(double.Parse(amntPaidTxtBox.Text) - double.Parse(amntDueTxtBox.Text), 2).ToString();
            }
            else 
            {
                changeTxtBox.Text = "0.00";
            }
        }
    }
}
