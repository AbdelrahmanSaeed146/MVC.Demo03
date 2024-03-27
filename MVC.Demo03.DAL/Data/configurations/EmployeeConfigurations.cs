using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MVC.Demo03.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Demo03.DAL.Data.configurations
{
    internal class EmployeeConfigurations : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(E => E.Name).HasColumnType("varchar").HasMaxLength(50).IsRequired();

            builder.Property(E => E.Address).IsRequired();

            builder.Property(E => E.Salary).HasColumnType("decimal(12,2)");

            builder.Property(E => E.Gender)
                .HasConversion
                (
                (Gender) => Gender.ToString(), 
                (Gender) => (Gender)Enum.Parse(typeof(Gender), Gender, true)
                );

            builder.Property(E => E.EmployeeType)
                .HasConversion(
                (EmployeeType) => EmployeeType.ToString(),
                (EmployeeType) => (EmpType)Enum.Parse(typeof(EmpType), EmployeeType, true)
                );

            builder.Property(e => e.Name).IsRequired().HasMaxLength(50);

        }
    }
}
