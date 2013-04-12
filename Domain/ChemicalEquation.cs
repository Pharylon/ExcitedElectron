﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngryElectron.Domain
{
    public delegate void EquationDelegate(IParsableSymbols symbol, Side side);

    public class ChemicalEquation : IEquation
    {
        private ElementGroup reactants = new ElementGroup(GroupType.Reactants);
        private ElementGroup products = new ElementGroup(GroupType.Products);

        public IParsableSymbols Reactants { get { return reactants; } }
        public IParsableSymbols Products { get { return products; } }

        public int MoleculeCount { get { return reactants.Count + products.Count; } }
        public int ReactantCount { get { return reactants.Count; } }
        public int ProductCount { get { return products.Count; } }

        public List<string> ListOfContents
        {
            get
            {
                List<string> content = reactants.ListOfContents.Union(products.ListOfContents).ToList();
                return content;
            }
        }

        public ChemicalEquation(ref EquationDelegate eqCallback)
        {
            eqCallback = AddToEquation;
        }

        private void AddToEquation(IParsableSymbols chemical, Side side)
        {
            if (side == Side.Reactants)
                reactants.Add(chemical);
            if (side == Side.Products)
                products.Add(chemical);
        }
        
        public IEnumerable<string> ParsableSymbols
        {
            get
            {
                List<string> symbols = new List<string>();
                foreach (IParsableSymbols reactant in reactants)
                {
                    foreach (string symbol in reactant.ParsableSymbols)
                        symbols.Add(symbol);
                    symbols.Add("+");
                }
                symbols.RemoveAt(symbols.Count - 1);
                symbols.Add("->");
                foreach (IParsableSymbols product in products)
                {
                    foreach (string symbol in product.ParsableSymbols)
                        symbols.Add(symbol);
                    symbols.Add("+");
                }
                symbols.RemoveAt(symbols.Count - 1);

                return symbols;
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Reactants.ToString());
            sb.Append(" -> ");
            sb.Append(Products.ToString());
            return sb.ToString();
        }

        public string ToHTML()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Reactants.ToHTML());
            sb.Append(" -> ");
            sb.Append(Products.ToHTML());
            return sb.ToString();
        }

        public int GetSubscriptCount(string key)
        {
            return products.GetSubscriptCount(key) + reactants.GetSubscriptCount(key);
        }
    }
}
