using System;
using System.Linq;

namespace TCNAImmersiveExperiencesTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //initialize parameters to be used for calculation
            int[] ingredientQuantities = { 38, 93, 48, 54, 95 };
            float[] ingredientCosts = { .30f, .55f, .44f, .22f, .11f };

            var results = ToyotasTacomaTacoTruckTrunkTrouble(ingredientQuantities, ingredientCosts);

            Console.WriteLine($"Total Available Tacos = {results.TotalAvailableTacos}");
            Console.WriteLine($"Total Taco Ingredient Cost = {results.OurTotalIngredientCosts:C}");
            Console.WriteLine($"Total Taco Revenue = {results.TotalChargeForTacos:C}");
            Console.WriteLine($"Total Taco Profit = {(results.TotalChargeForTacos - results.OurTotalIngredientCosts):C}");

            Console.Read();
        }

        /// <summary>
        ///
        /// We have a bunch of ingredients in the trunk of our taco Taco truck, and we're trying to see
        /// our expected revenue from making tacos, assuming we sell out of all tacos.
        ///
        /// Tacos sell at a fixed costs except on Tuesdays, when they're half off.
        ///
        /// A taco consists of one of each ingredient: meat, cheese, lettuce, tomato, and sauce.
        /// You can not make a taco if not all ingredients are present.
        ///
        /// We see a tip of 8-16 cents, so we like to calculate that in our total expected revenue
        /// (Don't tell accounting)
        ///
        /// You've been asked to code review what Tommy wrote below. While the code below works, what
        /// comments, suggestions, rewrites would you bring up?
        ///
        /// </summary>
        /// <param name="ingredientQuantity">Each of the ingredients sent in.</param>
        /// <param name="ingredientCost">Each of the costs of the ingredients sent in (matches ingredient list order)</param>
        /// <returns></returns>
        static TacoInformation ToyotasTacomaTacoTruckTrunkTrouble(int[] ingredientQuantity, float[] ingredientCost)
        {
            //initialize TacoInformation to hold totals
            TacoInformation returnInfo = new TacoInformation();
            float baseChargePricePerTaco = 2.34f;

            //set flag for sale day.  Currently sale day is only Tuesday
            bool isSaleHappening = (DateTime.Now.DayOfWeek.ToString() == "Tuesday");

            //find minimum ingredient quantity for all ingredients
            int totalTacosAvailable = ingredientQuantity.Min();

            //set the total cost of ingredients per taco
            float totalTacoIncredientCost = ingredientCost.Sum();

            //because available taco's are governed by the lowest number of ingredients
            //set the total available tacos to the minimum ingredient level
            returnInfo.TotalAvailableTacos = totalTacosAvailable;
            returnInfo.OurTotalIngredientCosts = totalTacoIncredientCost * totalTacosAvailable;

            //set the the taco price for the current day
            //if it's a sale day, cut price in half
            float todaysTacoChargePrice = isSaleHappening ? baseChargePricePerTaco / 2 : baseChargePricePerTaco;

            //loop through the number of tacos that are available to build out totals
            for (int i = 0; i < totalTacosAvailable; i++)
            {
                //use random number generator to create random tip between .08 and .16
                Random randomGen = new Random();
                float tip = (float)(randomGen.Next(8, 16) * .01);

                returnInfo.TotalChargeForTacos += todaysTacoChargePrice + tip;
            }

            return returnInfo;
        }
    }

    public struct TacoInformation
    {
        public float TotalAvailableTacos;
        public float TotalChargeForTacos;
        public float OurTotalIngredientCosts;
    }
}
