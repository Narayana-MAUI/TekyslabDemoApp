using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TekyslabDemo.Models
{
    public class SaveImage
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Base64Image { get; set; }
        public string ImageName { get; set; }
    }

    public class DisplayImages
    {
        public int Id { get; set; }
        public ImageSource Image { get; set; }
        public string ImageName { get; set; }

        public string Base64String { get; set; }
    }
}
