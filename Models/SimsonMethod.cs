using IntegralAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;

namespace IntegralAPI.Models
{

    public class SimsonMethod : ICalculator
    {
        private int SplitNumbers;
        private double UpLim;
        private double LowLim;
        private Func<double, double> integral;
        public double res { get; set; }

        public SimsonMethod(int SplitNumbers, double UpLim, double LowLim) 
        {
            this.SplitNumbers = SplitNumbers;
            this.UpLim = UpLim;
            this.LowLim = LowLim;
            this.integral = x => (x > 0 ? Math.Pow(Math.Log(x), 2) / x : 0);
        }
        public void Calculate()
        {
            double h = (UpLim - LowLim) / SplitNumbers;
            double sum = 0.0;

            for (int i = 0; i < SplitNumbers; i++)
            {
                sum += integral(LowLim + h * i) + 2 * integral(LowLim + i * h + h / 2);
            }
            
            res = h / 3 * ((integral(UpLim) - integral(LowLim)) / 2 + sum);
        }
 
    }
}
