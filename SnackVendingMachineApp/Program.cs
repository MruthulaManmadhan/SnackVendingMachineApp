using SnackVendingMachine_Payment;
using SnackVendingMachine_SnackSelectionAndDisplay;
using System;

namespace Snack_Vending_Machine
{
    class Program
    {
        public static void Main(string[] args)
        {

            bool control = true; Payment payment = new Payment();
            SnackVendingMachine snackVendingkMachine = new SnackVendingMachine();
            payment.InitialMachineMoney();
            while (control)
            {

                try
                {

                    Console.WriteLine("Enter -1 to Add:\nEnter any other number to move");
                    int choice = Convert.ToInt32(Console.ReadLine());

                    if (choice == -1)
                    {
                        snackVendingkMachine.AddItem();
                    }

                    Console.WriteLine("Enter 0 to Refill:\nEnter any other number to move");

                    choice = Convert.ToInt32(Console.ReadLine());

                    if (choice == 0)
                    {
                        Console.WriteLine("Enter Rack Number");
                        int rackNumber = Convert.ToInt32(Console.ReadLine());
                        snackVendingkMachine.Refill(rackNumber);
                    }
                    Console.WriteLine("Enter 0 to Refill Money:\nEnter any other number to move");





                    choice = Convert.ToInt32(Console.ReadLine());





                    if (choice == 0)
                    {
                        payment.RefillMoney(payment.machineMoney);
                    }



                    Tuple<int, int, bool> result = snackVendingkMachine.DiplaySnacks();
                    control = result.Item3;
                    int returnrackNumber = (result.Item2) - 1;
                    int returnCount = result.Item1;

                    int Total = payment.Total(returnCount, snackVendingkMachine.itemList[(returnrackNumber)]);
                    Console.WriteLine("Total : {0}", Total);
                    payment.CheckMoney(Total, returnrackNumber, returnCount);
                }

                catch
                {
                    Console.WriteLine("invalid Entry!!");
                }
            }
        }
    }
}
