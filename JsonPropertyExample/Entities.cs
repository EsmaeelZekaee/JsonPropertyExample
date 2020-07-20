using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace JsonPropertyExample
{
    public class Attribute
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }

        [HasJsonConversion]
        public List<AttributeValue> Values { get; set; }
    }

    public class AttributeValue
    {
        public string Value { get; set; }
        public IList<AttributeValueTranslation> Translations { get; set; }
    }

    public class AttributeValueTranslation
    {
        public string Translation { get; set; }

        public string CultureName { get; set; }
    }
}
