using System.ComponentModel.DataAnnotations;

namespace LQMSApplication.Model.Master
{
        public class MasterModel
        { }
        public class RigNameModel
        {
            [Key]
            public string Id { get; set; }
            public string? Name { get; set; }

        }

        public class DrillingMethodModel
        {
            [Key]
            public string Id { get; set; }
            public string? Name { get; set; }

        }

        public class FluidTypeModel
        {
            [Key]
            public string Id { get; set; }
            public string? Name { get; set; }

        }

        public class BitTypeModel
        {
            [Key]
            public string Id { get; set; }
            public string? Name { get; set; }

        }

        public class CoreBarrellTypeModel
        {
            [Key]
            public string Id { get; set; }
            public string? Name { get; set; }

        }

        public class WhetherConditionModel
        {
            [Key]
            public string Id { get; set; }
            public string? Name { get; set; }

        }

        public class SampleTypeModel
        {
            [Key]
            public string Id { get; set; }
            public string? Name { get; set; }

        }

        public class PressureMeterModel
        {
            [Key]
            public string Id { get; set; }
            public string? Name { get; set; }

        }

        public class EquipmentModel
        {
            [Key]
            public string Id { get; set; }
            public string? Name { get; set; }

        }
        public class PartsModel
        {
            [Key]
            public string Id { get; set; }
            public string? Name { get; set; }

        }

        public class CostsModel
        {
            [Key]
            public string Id { get; set; }
            public string? Name { get; set; }

        }

        public class ItemModel
        {
            [Key]
            public string Id { get; set; }
            public string? Name { get; set; }

        }

        public class TypeofIncidentModel
        {
            [Key]
            public string Id { get; set; }
            public string? Name { get; set; }

        }

        public class TypeOfActivityModel
        {
            [Key]
            public string Id { get; set; }
            public string? Name { get; set; }

        }

        public class DescriptionDataModel
        {
            [Key]
            public string Id { get; set; }
            public string? Name { get; set; }

        }

        public class EquipmentConditionModel
        {
            [Key]
            public string Id { get; set; }
            public string? Name { get; set; }
            //public string? EquipmentName { get; set; }
            //public string? ReferenceNumber { get; set; }
            //public string? Status { get; set; }
            //public int?  DrilsheetId { get; set; }

        }

        public class UrlRequest
        {
            public string? URL { get; set; }
        }

        public class ImageModel
        {
            [Key]
            public int ImageID { get; set; }
            public string? ImageName { get; set; }

        }
}
