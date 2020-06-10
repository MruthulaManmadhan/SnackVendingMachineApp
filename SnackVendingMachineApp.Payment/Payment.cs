using SnackVendingMachine_SnackSelectionAndDisplay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace SnackVendingMachine_Payment
{
    public class Money
    {
        public int Fives { get; set; }
        public int Tens { get; set; }
        public int Twenties { get; set; }
        public int Fifties { get; set; }
        public int Total { get; set; }
    }
    public class Payment
    {
        SnackVendingMachine snackVendingkMachine = new SnackVendingMachine();
        public Money machineMoney = new Money();
        public Money userMoney = new Money();
        public Money balanceMoney = new Money();
        public Money money = new Money();
        public void InitialMachineMoney()//initializing money for machine at first
        {
            Console.Write("Enter Number of 50 rupees :");
            money.Fifties = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter Number of 20 rupees :");
            money.Twenties = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter Number of 10 rupees :");
            money.Tens = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter Number of 5 rupees :");
            money.Fives = Convert.ToInt32(Console.ReadLine());
            SetMoney(machineMoney, money);
        }
        public void MoneyTotal(Money money)//finding total 
        {
            money.Total = (money.Fives * 5) + (money.Tens * 10) + (money.Twenties * 20) + (money.Fifties * 50);
        }
        public int Total(int count, Item item)//total amound for number of items selected by the user
        {
            return (item.Price * count);
        }

        public void CheckMoney(int total, int returnrackNumber, int returnCount)
        {
            //getting money from user
            Console.WriteLine("Enter number of 50 rupees you are paying");
            userMoney.Fifties = Convert.ToInt32(Console.ReadLine());
            total = total - (userMoney.Fifties * 50);
            if (total >= 20)
            {
                Console.WriteLine("Enter number of 20 rupees you are paying");
                userMoney.Twenties = Convert.ToInt32(Console.ReadLine());
                total = total - (userMoney.Twenties * 20);
            }
            if (total >= 10)
            {
                Console.WriteLine("Enter number of 10 rupees you are paying");
                userMoney.Tens = Convert.ToInt32(Console.ReadLine());
                total = total - (userMoney.Tens * 10);
            }
            if (total > 5)
            {
                Console.WriteLine("Enter number of 5 rupees you are paying");
                userMoney.Fives = Convert.ToInt32(Console.ReadLine());
                total = total - (userMoney.Fives * 5);
            }
            if (total > 0)// if user didn't gave enough money
            {
                Console.WriteLine("Not enough money entered");
                GetBackMoney(userMoney);//need to add the count of the same item
            }
            else if (total < 0)// if user gave more money... need to give balance
            {
                GetMoney(machineMoney, userMoney);
                int Balance = total * -1;
                balanceMoney = Calculatebalance(Balance, machineMoney);
                if (money.Total == Balance)//got enough balance to give back to user
                {
                    snackVendingkMachine.TakeCount(returnCount, returnrackNumber);
                    GetBackMoney(balanceMoney);
                    SetMoney(machineMoney, money);///need to whrite methode
                }
                else// no much balance to give back to user
                {
                    Console.WriteLine("Sorry machine doesn't have much balance");
                    GetBackMoney(userMoney);
                }
            }
            else
            {
                GetMoney(machineMoney, userMoney);
            }
            Console.WriteLine("Thank you !!!");
        }
        public void SetMoney(Money machineMoney,Money money)
        {
            machineMoney.Fifties = money.Fifties;
            machineMoney.Twenties = money.Twenties;
            machineMoney.Tens = money.Tens;
            machineMoney.Fives = money.Fives;
            MoneyTotal(machineMoney);
        }
        public void GetMoney(Money machineMoney, Money userMoney)
        {
            machineMoney.Fifties += userMoney.Fifties;
            machineMoney.Twenties += userMoney.Twenties;
            machineMoney.Tens += userMoney.Tens;
            machineMoney.Fives += userMoney.Fives;
            MoneyTotal(machineMoney);
        }
        public void GetBackMoney(Money money)// giving money back to user... may be balance or may be returning money
        {
            Console.WriteLine("Get money");
            Console.WriteLine("Fifties  : {0}     Twenties  : {1}     Tens      : {2}     Fives : {3}", money.Fifties, money.Twenties, money.Tens, money.Fives);
        }
        public Money Calculatebalance(int balance, Money machineMoney)// calculating balance
        {
            balanceMoney.Fifties = 0;
            balanceMoney.Twenties = 0;
            balanceMoney.Tens = 0;
            balanceMoney.Fives = 0;
            balanceMoney.Total = 0;

            money.Fifties = machineMoney.Fifties;
            while (balance > 50 && money.Fifties > 0)
            {
                balanceMoney.Fifties++;
                balanceMoney.Total += 50;
                money.Fifties--;
                balance -= 50;
            }
            money.Twenties = machineMoney.Twenties;
            while (balance > 20 && money.Twenties > 0)
            {
                balanceMoney.Twenties++;
                balanceMoney.Total += 20;
                money.Twenties--;
                balance -= 20;
            }
            money.Tens = machineMoney.Tens;
            while (balance > 10 && money.Tens > 0)
            {
                balanceMoney.Tens++;
                balanceMoney.Total += 10;
                money.Tens--;
                balance -= 10;
            }
            money.Fives = machineMoney.Fives;
            while (balance > 5 && money.Fives > 0)
            {
                balanceMoney.Fives++;
                balanceMoney.Total += 5;
                money.Fives--;
                balance -= 5;
            }
            return balanceMoney;
        }
        public void RefillMoney(Money machineMoney)
        {
            PrintMachineMoney(machineMoney);
            Console.WriteLine("Enter 0 for put money to machine");
            Console.WriteLine("enter 1 for collect money from machine");
            int Choice = Convert.ToInt32(Console.ReadLine());
            if (Choice == 0)
            {
                PutMoney(machineMoney);
            }
            else if (Choice == 1)
            {
                CollectMoney(machineMoney);
            }
            else
            {
                Console.WriteLine("Invalid input");
            }
        }
        public void PutMoney(Money machineMoney)//put money to machine
        {
            Console.WriteLine("Enter number of 50 rupees");
            machineMoney.Fifties += Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter number of 20 rupees");
            machineMoney.Twenties += Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter number of 10 rupees");
            machineMoney.Tens += Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter number of 5 rupees");
            machineMoney.Fives += Convert.ToInt32(Console.ReadLine());
            MoneyTotal(machineMoney);
            Console.WriteLine("Money has been entered");
        }
        public void CollectMoney(Money machineMoney)
        {
            Money money = new Money();
            Console.WriteLine("Enter number of 50 rupees");
            money.Fifties = Convert.ToInt32(Console.ReadLine());
            if (money.Fifties <= machineMoney.Fifties)
            {
                machineMoney.Fifties -= money.Fifties;
            }
            else
            {
                Console.WriteLine("{0} fifty rupees is not available");
            }
            Console.WriteLine("Enter number of 50 rupees");
            money.Fifties = Convert.ToInt32(Console.ReadLine());
            if (money.Fifties <= machineMoney.Fifties)
            {
                machineMoney.Fifties -= money.Fifties;
            }
            else
            {
                Console.WriteLine("{0} fifty rupees is not available");
            }
            Console.WriteLine("Enter number of 20 rupees");
            money.Twenties = Convert.ToInt32(Console.ReadLine());
            if (money.Twenties <= machineMoney.Twenties)
            {
                machineMoney.Twenties -= money.Twenties;
            }
            else
            {
                Console.WriteLine("{0} twenty rupees is not available");
            }
            Console.WriteLine("Enter number of 10 rupees");
            money.Tens = Convert.ToInt32(Console.ReadLine());
            if (money.Tens <= machineMoney.Tens)
            {
                machineMoney.Tens -= money.Tens;
            }
            else
            {
                Console.WriteLine("{0} ten rupees is not available");
            }
            Console.WriteLine("Enter number of 5 rupees");
            money.Fives = Convert.ToInt32(Console.ReadLine());
            if (money.Fives <= machineMoney.Fives)
            {
                machineMoney.Fives -= money.Fives;
            }
            else
            {
                Console.WriteLine("{0} five rupees is not available");
            }
            MoneyTotal(machineMoney);
            GetBackMoney(money);
        }
        public void PrintMachineMoney(Money machineMoney)// printing machine money
        {
            Console.WriteLine("Fifties  :  {0}", machineMoney.Fifties);
            Console.WriteLine("Twenties :  {0}", machineMoney.Twenties);
            Console.WriteLine("Tens     :  {0}", machineMoney.Tens);
            Console.WriteLine("Fives    :  {0}", machineMoney.Fives);
            Console.WriteLine("Total    :  {0}", machineMoney.Total);



        }
    }
}
