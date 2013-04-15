﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngryElectron.Domain
{
    class Molecule : ElementGroup
    {
        public int Coefficient = 1;

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (string symbol in this.ListOfContents)
            {
                int count = this.GetShallowCount(symbol);
                if (count == 1)
                    sb.Append(symbol);
                else
                    sb.Append(symbol + count.ToString());
            }
            if (Coefficient != 1)
                sb.Insert(0, this.Coefficient);
            return sb.ToString();
        }

        public override string ToHTML()
        {
            StringBuilder sb = new StringBuilder();
            foreach (string symbol in this.ListOfContents)
            {
                int count = this.GetShallowCount(symbol);
                if (count == 1)
                    sb.Append(symbol);
                else
                    sb.Append(symbol + count.ToString());
            }

            for (int i = sb.Length - 1; i >= 0; i--)
            {
                if (Char.IsDigit(sb[i]))
                {
                    sb.Insert(i + 1, "</sub>");
                    sb.Insert(i, "<sub>");
                }
            }
            if (Coefficient != 1)
                sb.Insert(0, this.Coefficient);
            return sb.ToString();
        }
    }
}