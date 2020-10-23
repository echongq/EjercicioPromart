using Microsoft.EntityFrameworkCore;
using EjercicioPromart.Context;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace EjercicioPromart.Models
{
    public class EmployeeDAO
    {
        private readonly ApplicationDbContext context;
        public EmployeeDAO(ApplicationDbContext context)
        {
            this.context = context;
        }
        public DataTable GetEmployee()
        {
            using (var command = context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "exec [dbo].[sp_Employee]";

                context.Database.OpenConnection();
                var result = command.ExecuteReader();
                var dataTable = new DataTable();
                dataTable.Load(result);
                return (dataTable);
            }
        }
        public  DataTable GetSalaryRange(int value1, int value2)
        {
            using (var command = context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "exec [dbo].[sp_EmployeeRangeSalary]  @PsearchValue1 , @PsearchValue2";

                DbParameter PsearchValue1 = command.CreateParameter();
                PsearchValue1.ParameterName = "@PsearchValue1";
                PsearchValue1.Value = value1;
                command.Parameters.Add(PsearchValue1);

                DbParameter PsearchValue2 = command.CreateParameter();
                PsearchValue2.ParameterName = "@PsearchValue2";
                PsearchValue2.Value = value2;
                command.Parameters.Add(PsearchValue2);

                context.Database.OpenConnection();
                var result = command.ExecuteReader();
                var dataTable = new DataTable();
                dataTable.Load(result);
                return (dataTable);
            }
        }
    }
}
