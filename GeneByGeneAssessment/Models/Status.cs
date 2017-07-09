using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GeneByGeneAssessment.Models
{
    public class Status
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int StatusId { get; set; }
        public string StatusName { get; set; }

        public ICollection<Sample> Samples { get; set; }
    }
}
