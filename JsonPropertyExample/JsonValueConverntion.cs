using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;

namespace JsonPropertyExample
{
    public class HasJsonConversionAttribute : System.Attribute
    {

    }
    
    public static class ValueConversionExtensions
    {
        public static PropertyBuilder HasJsonConversion(this PropertyBuilder propertyBuilder)
        {
            ParameterExpression parameter1 = Expression.Parameter(propertyBuilder.Metadata.ClrType, "v");

            MethodInfo methodInfo1 = typeof(Newtonsoft.Json.JsonConvert).GetMethod("SerializeObject", types: new Type[] { typeof(object) });
            MethodCallExpression expression1 = Expression.Call(methodInfo1 ?? throw new Exception("Method not found"), parameter1);

            ParameterExpression parameter2 = Expression.Parameter(typeof(string), "v");
            MethodInfo methodInfo2 = typeof(Newtonsoft.Json.JsonConvert).GetMethod("DeserializeObject", 1, BindingFlags.Static | BindingFlags.Public, Type.DefaultBinder, CallingConventions.Any, types: new Type[] { typeof(string) }, null)?.MakeGenericMethod(propertyBuilder.Metadata.ClrType) ?? throw new Exception("Method not found");
            MethodCallExpression expression2 = Expression.Call(methodInfo2, parameter2);

            var converter = Activator.CreateInstance(typeof(ValueConverter<,>).MakeGenericType(propertyBuilder.Metadata.ClrType, typeof(string)), new object[]
            {
                Expression.Lambda( expression1,parameter1),
                Expression.Lambda( expression2,parameter2),
                (ConverterMappingHints) null
            });


            propertyBuilder.HasConversion(converter as ValueConverter);

            return propertyBuilder;
        }
    }
}
