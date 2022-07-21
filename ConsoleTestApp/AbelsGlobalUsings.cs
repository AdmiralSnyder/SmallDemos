global using System.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTestApp
{
    internal class AbelsGlobalUsings
    {

        [Obsolete("Lol")]
        private void
            Something()
        { }

        private void SomethingElse()
        {
            Something();
        }
    }


    class Ledger
    {
        public List<Entry> Entries { get; set; } = new();
        public decimal Value { get; set; }
    }

    abstract class Entry
    {
        public decimal Amount { get; set; }
        public string Order { get; set; }

        public void Apply(Ledger ledger)
        {
            ledger.Value += ApplyAmount;
            ledger.Entries.Add(this);
        }
        public abstract decimal ApplyAmount { get; }
    }

    class Withdrawal : Entry
    {
        public override decimal ApplyAmount => -Amount;
    }
    class Deposit : Entry 
    {
        public override decimal ApplyAmount => Amount;
    }

    record PaymentInfo(decimal MonthlyDebit, decimal MonthlyCredit, decimal TotalDebit, decimal TotalCredit);

    class Handling
    {
        //void HandleInvoice(decimal invoiceAmount, PaymentInfo info, List<Entry> entries, List<Entry> FrontLoadedRecurringJournalEntryLines)
        //{
        //    var creditDiscrepancy = invoiceAmount - info.TotalCredit;
        //    var debitDiscrepancy = invoiceAmount - info.TotalDebit;
        //    while (creditDiscrepancy > 0m)
        //    {
        //        foreach (var x in entries.Where(x => x is Withdrawal && x.Amount > 0).OrderByDescending(x => x.Amount))
        //        {
        //            var percentage = x.Amount / info.MonthlyCredit;
        //            //if (!percentage.HasValue) continue;
        //            var flrjl = FrontLoadedRecurringJournalEntryLines.FirstOrDefault(y => y.Order == x.Order);
        //            var adjustment = Math.Min(Math.Ceiling(percentage.Value * creditDiscrepancy * 100) / 100, creditDiscrepancy);
        //            creditSum += adjustment;
        //            flrjl.ExtraCredit += adjustment;
        //            creditDiscrepancy -= adjustment;
        //        }
        //    }

        //}

        void DoStuff(List<Entry> entries)
        {
            Ledger l = new();
            foreach (var entry in entries)
            {
                entry.Apply(l);
            }
        }

        
    }
}
