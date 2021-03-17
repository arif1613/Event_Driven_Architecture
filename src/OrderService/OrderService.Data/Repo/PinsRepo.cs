using System;
using System.Collections.Generic;
using System.Linq;
using OrderService.Data.Context;
using OrderService.Data.Models;

namespace OrderService.Data.Repo
{
    public class PinsRepo : IPinsRepo
    {
        private readonly IOrderContext Context;

        public PinsRepo(IOrderContext context)
        {
            Context = context;

            //Prepopulate pins for better performance
            PopulatePinsInDatabase();
        }

        private void PopulatePinsInDatabase()
        {
            var pins = Context.Pins.ToList();
            if (!pins.Any())
            {
                //Total number can be calcalated from n!/(n-c)!
                //Here n=9 as pins from 1,2,3,4,5,6,7,8,9
                //Here c=4 as we are creating 4 digit number
                //so max entry: 9!(9-4)!=9!/5!=3024
                int totalPinsInDb = 3024 - 17; //here 17 is pre-defined ovious number list
                pins = CreatePins(totalPinsInDb);
            }

        }

        public List<Pin> CreatePins(int numberofpins)
        {
            //Check for already created pins in Database
            var existingPins = Context.Pins.ToList();
            if (!existingPins.Any())
            {
                var pins = new List<Pin>();
                Random rand = new Random();

                do
                {
                    var randomNumber = rand.Next(1001, 9999);
                    if (!IsOviousNumbers(randomNumber))
                    {
                        if (pins.Any() && pins.Any(r => r.Value != randomNumber))
                        {
                            pins.Add(new Pin
                            {
                                Value = randomNumber,
                                IsUsed = false
                            });
                        }
                        else
                        {
                            pins.Add(new Pin
                            {
                                Value = randomNumber,
                                IsUsed = false
                            });
                        }
                    }
                } while (pins.Count < numberofpins);

                Context.Pins.AddRange(pins);
                Context.SaveDatabase();
                return pins;

            }

            return existingPins;
        }

        private bool IsOviousNumbers(int randomNumber)
        {

            //Pre-defined 17 ovious numbers
            var oviousNumbers = new List<int> { 1111, 2222, 3333, 5555, 6666, 7777, 8888, 9999, 1000, 2000, 3000, 4000, 5000, 6000, 7000, 8000, 9000 };
            return oviousNumbers.Contains(randomNumber);
        }

        public Pin GetPin()
        {
            var pin = Context.Pins.FirstOrDefault(r => !r.IsUsed);
            if (pin == null) return null;
            MakePinUsed(pin);
            return pin;

        }

        public void MakePinUsed(Pin pinToUpdate)
        {
            pinToUpdate.IsUsed = true;
            Context.UpdateEntry(pinToUpdate);
        }

        public List<Pin> GetPins()
        {
            return Context.Pins.ToList();
        }

        public List<Pin> GetUseablePins()
        {
            return Context.Pins.Where(r => !r.IsUsed).ToList();
        }

        public Pin UnUsePin(int value)
        {
            var pinToUpdate = Context.Pins.FirstOrDefault(r => r.Value == value);
            if (pinToUpdate == null || !pinToUpdate.IsUsed)
            {
                return null;
            }
            pinToUpdate.IsUsed = false;
            Context.UpdateEntry(pinToUpdate);
            return pinToUpdate;
        }

        public Pin GetByPinValue(int pinValue)
        {
            return Context.Pins.FirstOrDefault(r => r.Value == pinValue);
        }
    }
}
