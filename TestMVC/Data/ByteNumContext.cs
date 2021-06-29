using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TestMVC.Models;

namespace TestMVC.Data
{
	public class ByteNumContext : DbContext
	{
		public ByteNumContext(DbContextOptions<ByteNumContext> options) :
			base(options)
		{

		}
		public DbSet<ByteNum> ByteNum { get; set; }
	}
}
