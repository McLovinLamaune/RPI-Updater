using System;
using System.Collections.Generic;
using System.Text;

namespace RPI_Updater.Models
{
    public class ManifestModel
    {
        public string Version { get; set; }
        public List<PackageModel> Packages { get; set; }

        public static bool operator ==(ManifestModel obj1, ManifestModel obj2)
        {
            return obj1.Version == obj2.Version;
        }

        public static bool operator !=(ManifestModel obj1, ManifestModel obj2)
        {
            return obj1.Version != obj2.Version;
        }
    }

    public class PackageModel
    {
        public string Name { get; set; }
        public string Version { get; set; }
    }
}
