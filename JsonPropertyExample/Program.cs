using System;
using System.Collections.Generic;

namespace JsonPropertyExample
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var dbContext = new MyDbContext())
            {
                dbContext.Database.EnsureDeleted();
                dbContext.Database.EnsureCreated();
                dbContext.Attributes.Add(new Attribute(){Name = "Color", Values = new List<AttributeValue>()
                {
                    new AttributeValue()
                    {
                        Value = "Red",
                        Translations = new List<AttributeValueTranslation>()
                        {
                            new AttributeValueTranslation()
                            {
                                Translation = "Ghermez",
                                CultureName = "fa-IR"
                            },
                            new AttributeValueTranslation()
                            {
                                Translation = "Rood",
                                CultureName = "de.DE"
                            }
                        }
                    },
                    new AttributeValue()
                    {
                        Value = "Blue",
                        Translations = new List<AttributeValueTranslation>()
                        {
                            new AttributeValueTranslation()
                            {
                                Translation = "Abi",
                                CultureName = "fa-IR"
                            },
                            new AttributeValueTranslation()
                            {
                                Translation = "Blauw",
                                CultureName = "de.DE"
                            }
                        }
                    }
                }
                });
                dbContext.SaveChanges();
            }

            using (var dbContext = new MyDbContext())
            {
                foreach (var attribute in dbContext.Attributes)
                {
                    Console.WriteLine(attribute.Name);
                    foreach (var value in attribute.Values)
                    {
                        Console.Write("\t");
                        Console.WriteLine(value.Value);
                        foreach (var translation in value.Translations)
                        {
                            Console.Write("\t\t");
                            Console.WriteLine($"{translation.CultureName} = {translation.Translation}");    
                        }
                    }
                }
            }
        }

    }
}
