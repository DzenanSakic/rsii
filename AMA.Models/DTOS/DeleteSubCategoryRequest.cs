using ExpressiveAnnotations.Attributes;

namespace AMA.Models.DTOS
{
    public class DeleteSubCategoryRequest
    {
        [RequiredIf("Name == null")]
        public int? Id { get; set; }
        [RequiredIf("Id == null")]
        public string Name { get; set; }
    }
}
