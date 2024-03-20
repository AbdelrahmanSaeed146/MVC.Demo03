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
    internal class DepartmentConfigurations :IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
           builder.Property(D=>D.Id).UseIdentityColumn(10,10);
           builder.Property(D=>D.Code).IsRequired().HasColumnType("varchar").HasMaxLength(50);
           builder.Property(D=>D.Name).IsRequired().HasColumnType("varchar").HasMaxLength(50);
        }

      
    }
}
