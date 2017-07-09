using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GeneByGeneAssessment.Models
{
    public class Sample
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SampleId { get; set; }
        public string Barcode { get; set; }
        public DateTime CreatedAt { get; set; }    
        [ForeignKey("UserId")]
        public int CreatedBy { get; set; }
        public int StatusId { get; set; }

        [ForeignKey("CreatedBy")]
        public User Creator { get; set; }
        public Status Status { get; set; }
    }
}
