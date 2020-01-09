using System;
using System.Collections.Generic;
using System.Xaml;
using System.Xaml.Schema;
using NLog;

namespace ParseLogsControls
{
public class JsonXamlType : XamlType {
    private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

    protected JsonXamlType(string typeName, IList<XamlType> typeArguments, XamlSchemaContext schemaContext) : base(typeName, typeArguments, schemaContext)
    {
    }

    public JsonXamlType(string unknownTypeNamespace, string unknownTypeName, IList<XamlType> typeArguments, XamlSchemaContext schemaContext) : base(unknownTypeNamespace, unknownTypeName, typeArguments, schemaContext)
    {
    }

    public JsonXamlType(Type underlyingType, XamlSchemaContext schemaContext) : base(underlyingType, schemaContext)
    {
    }

    public JsonXamlType(Type underlyingType, XamlSchemaContext schemaContext, XamlTypeInvoker invoker) : base(underlyingType, schemaContext, invoker)
    {
    }
}
}
