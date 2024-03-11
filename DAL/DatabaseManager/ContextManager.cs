using DAL.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DatabaseManager
{
    public class ContextManager
    {
        static ContextManager()
        {
            myContext = new ExamSystemContext();
        }
        
        static ExamSystemContext myContext;

        public static ExamSystemContext MyContext { get { return myContext; } }

        private ContextManager()
        {
            
        }
    }
}