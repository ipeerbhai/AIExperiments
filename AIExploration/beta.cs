using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIExploration
{
    class beta
    {
        public int betaValue { get; set; } // The assigned beta value for the regressor.
        public int mode { get; set; } // The most common value of the input from history.
        public int bottomLimit { get; set; } // the minimm allowed input limit.
        public int topLimit { get; set; } // the maximum allowed limit.  Exceeding means the input = 0,

        public beta()
        {
            betaValue = 10; // default to a "binary" beta with a fixed value.
            mode = 0;
            bottomLimit = 0;
            topLimit = 1;
        }

        public static int operator *(beta thisBeta, int input)
        {
            if (input == 0)
            {
                return (0);
            }
            else if (input < thisBeta.bottomLimit)
            {
                return (0);
            }
            else if (input > thisBeta.topLimit)
            {
                return (0);
            }
            else
            {
                return (thisBeta.betaValue * input);
            }
        }
    }
}
