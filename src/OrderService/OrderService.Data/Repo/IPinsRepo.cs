using System.Collections.Generic;
using OrderService.Data.Models;

namespace OrderService.Data.Repo
{
    public interface IPinsRepo
    {
        List<Pin> CreatePins(int numberofpins);
        Pin GetPin();
        List<Pin> GetPins();
        List<Pin> GetUseablePins();
        Pin UnUsePin(int value);
        Pin GetByPinValue(int pinValue);
        void MakePinUsed(Pin pinToUpdate);

    }
}