using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Perebor.Logic.Models
{
    internal class ValidPlan
    {
        public List<Semester> Plan { get; set; }
        public double Coefficient1 { get; set; }
        public double Coefficient2 { get; set; }

        public ValidPlan(List<Semester> plan, double coeff1, double coeff2)
        {
            Plan = plan;
            Coefficient1 = coeff1;
            Coefficient2 = coeff2;
        }
    }
}
