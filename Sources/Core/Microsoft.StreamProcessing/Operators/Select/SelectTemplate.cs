﻿// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 15.0.0.0
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------
namespace Microsoft.StreamProcessing
{
    using System.Linq;
    using System;
    
    /// <summary>
    /// Class to produce the template output
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "15.0.0.0")]
    internal partial class SelectTemplate : CommonUnaryTemplate
    {
        /// <summary>
        /// Create the template output
        /// </summary>
        public override string TransformText()
        {
            this.Write(@"// *********************************************************************
// Copyright (c) Microsoft Corporation.  All rights reserved.
// Licensed under the MIT License
// *********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using Microsoft.StreamProcessing;
using Microsoft.StreamProcessing.Internal;
using Microsoft.StreamProcessing.Internal.Collections;

");
 if (this.keyType.Namespace != null) { 
            this.Write("using ");
            this.Write(this.ToStringHelper.ToStringWithCulture(this.keyType.Namespace));
            this.Write(";\r\n");
 } 
 if (this.payloadType.Namespace != null && this.payloadType.Namespace != this.keyType.Namespace) { 
            this.Write("using ");
            this.Write(this.ToStringHelper.ToStringWithCulture(this.payloadType.Namespace));
            this.Write(";\r\n");
 } 
 if (this.resultType.Namespace != null && this.resultType.Namespace != this.keyType.Namespace && this.resultType.Namespace != this.payloadType.Namespace) { 
            this.Write("using ");
            this.Write(this.ToStringHelper.ToStringWithCulture(this.resultType.Namespace));
            this.Write(";\r\n");
 } 
            this.Write("[assembly: IgnoresAccessChecksTo(\"Microsoft.StreamProcessing\")]\r\n\r\n// Source Fiel" +
                    "ds: ");
            this.Write(this.ToStringHelper.ToStringWithCulture(String.Join(",", this.fields.Select(f => f.OriginalName))));
            this.Write("\r\n// Destination Fields: ");
            this.Write(this.ToStringHelper.ToStringWithCulture(String.Join(",", this.destinationFields.Select(f => f.OriginalName))));
            this.Write("\r\n// Computed Fields: ");
            this.Write(this.ToStringHelper.ToStringWithCulture(String.Join(",", this.computedFields.Keys.Select(f => f.OriginalName))));
            this.Write("\r\n// Swinging Fields: ");
            this.Write(this.ToStringHelper.ToStringWithCulture(String.Join(",", this.swingingFields.Select(tup => string.Format("<{0},{1}>", tup.Item1.OriginalName, tup.Item2.Name)))));
            this.Write("\r\n\r\n[DataContract]\r\ninternal sealed class ");
            this.Write(this.ToStringHelper.ToStringWithCulture(className));
            this.Write(this.ToStringHelper.ToStringWithCulture(genericParameters));
            this.Write(" : UnaryPipe<");
            this.Write(this.ToStringHelper.ToStringWithCulture(TKey));
            this.Write(", ");
            this.Write(this.ToStringHelper.ToStringWithCulture(TPayload));
            this.Write(", ");
            this.Write(this.ToStringHelper.ToStringWithCulture(TResult));
            this.Write(">\r\n{\r\n    private ");
            this.Write(this.ToStringHelper.ToStringWithCulture(Transformer.GetMemoryPoolClassName(this.keyType, this.resultType)));
            this.Write(this.ToStringHelper.ToStringWithCulture(MemoryPoolGenericParameters));
            this.Write(" pool;\r\n    private readonly Func<PlanNode, IQueryObject, PlanNode> queryPlanGene" +
                    "rator;\r\n");
 foreach (var f in this.unassignedFields) { 
 if (!f.OptimizeString()) { 
            this.Write("\r\n    private readonly ColumnBatch<");
            this.Write(this.ToStringHelper.ToStringWithCulture(f.TypeName));
            this.Write("> sharedDefaultColumnFor_");
            this.Write(this.ToStringHelper.ToStringWithCulture(f.Name));
            this.Write(";\r\n");
 } 
 } 
            this.Write("\r\n    ");
            this.Write(this.ToStringHelper.ToStringWithCulture(staticCtor));
            this.Write("\r\n\r\n    public ");
            this.Write(this.ToStringHelper.ToStringWithCulture(className));
            this.Write("() { }\r\n\r\n    public ");
            this.Write(this.ToStringHelper.ToStringWithCulture(className));
            this.Write("(\r\n        IStreamable<");
            this.Write(this.ToStringHelper.ToStringWithCulture(TKey));
            this.Write(", ");
            this.Write(this.ToStringHelper.ToStringWithCulture(TResult));
            this.Write("> stream,\r\n        IStreamObserver<");
            this.Write(this.ToStringHelper.ToStringWithCulture(TKey));
            this.Write(", ");
            this.Write(this.ToStringHelper.ToStringWithCulture(TResult));
            this.Write("> observer,\r\n        Func<PlanNode, IQueryObject, PlanNode> queryPlanGenerator)\r\n" +
                    "        : base(stream, observer)\r\n    {\r\n        pool = MemoryManager.GetMemoryP" +
                    "ool<");
            this.Write(this.ToStringHelper.ToStringWithCulture(TKey));
            this.Write(", ");
            this.Write(this.ToStringHelper.ToStringWithCulture(TResult));
            this.Write(">() as ");
            this.Write(this.ToStringHelper.ToStringWithCulture(Transformer.GetMemoryPoolClassName(this.keyType, this.resultType)));
            this.Write(this.ToStringHelper.ToStringWithCulture(MemoryPoolGenericParameters));
            this.Write(";\r\n        this.queryPlanGenerator = queryPlanGenerator;\r\n");
 foreach (var f in this.unassignedFields.Where(fld => !fld.OptimizeString())) { 
            this.Write("\r\n        pool.Get(out sharedDefaultColumnFor_");
            this.Write(this.ToStringHelper.ToStringWithCulture(f.Name));
            this.Write(");\r\n        Array.Clear(sharedDefaultColumnFor_");
            this.Write(this.ToStringHelper.ToStringWithCulture(f.Name));
            this.Write(".col, 0, sharedDefaultColumnFor_");
            this.Write(this.ToStringHelper.ToStringWithCulture(f.Name));
            this.Write(".col.Length);\r\n");
 } 
            this.Write("\r\n    }\r\n\r\n    public override void ProduceQueryPlan(PlanNode previous)\r\n    {\r\n " +
                    "       Observer.ProduceQueryPlan(queryPlanGenerator(previous, this));\r\n    }\r\n\r\n" +
                    "    protected override void DisposeState()\r\n    {\r\n");
 foreach (var f in this.unassignedFields.Where(fld => !fld.OptimizeString())) { 
            this.Write("        this.sharedDefaultColumnFor_");
            this.Write(this.ToStringHelper.ToStringWithCulture(f.Name));
            this.Write(".Return();\r\n");
 } 
            this.Write("    }\r\n\r\n    public override unsafe void OnNext(StreamMessage<");
            this.Write(this.ToStringHelper.ToStringWithCulture(TKey));
            this.Write(", ");
            this.Write(this.ToStringHelper.ToStringWithCulture(TPayload));
            this.Write("> _inBatch)\r\n    {\r\n        var sourceBatch = _inBatch as ");
            this.Write(this.ToStringHelper.ToStringWithCulture(BatchGeneratedFrom_TKey_TPayload));
            this.Write(this.ToStringHelper.ToStringWithCulture(TKeyTPayloadGenericParameters));
            this.Write(";\r\n\r\n        StreamMessage<");
            this.Write(this.ToStringHelper.ToStringWithCulture(TKey));
            this.Write(", ");
            this.Write(this.ToStringHelper.ToStringWithCulture(TResult));
            this.Write("> _genBatch; // Need this type to call Get with so the right subtype will be retu" +
                    "rned\r\n        pool.Get(out _genBatch);\r\n\r\n        ");
            this.Write(this.ToStringHelper.ToStringWithCulture(Transformer.GetBatchClassName(keyType, resultType)));
            this.Write(this.ToStringHelper.ToStringWithCulture(TKeyTResultGenericParameters));
            this.Write(" resultBatch = _genBatch as ");
            this.Write(this.ToStringHelper.ToStringWithCulture(Transformer.GetBatchClassName(keyType, resultType)));
            this.Write(this.ToStringHelper.ToStringWithCulture(TKeyTResultGenericParameters));
            this.Write(@";
        var count = sourceBatch.Count;

        resultBatch.vsync = sourceBatch.vsync;
        resultBatch.vother = sourceBatch.vother;
        resultBatch.key = sourceBatch.key;
        resultBatch.hash = sourceBatch.hash;
        resultBatch.bitvector = sourceBatch.bitvector;

");
 if (resultType.CanContainNull()) { 
            this.Write("        pool.GetBV(out resultBatch._nullnessvector);\r\n");
 } 
            this.Write(@"
        // Get memory pools for the result columns.
        // When no transformation was done to the query, then needed for all fields in the result type.
        // When the query was transformed, then this is needed only for computed fields, since
        // any field that is just assigned a field from the source type will get its value by
        // swinging a pointer for the corresponding column.

");
 foreach (var f in (this.ProjectionReturningResultInstance != null ? this.destinationFields : this.computedFields.Keys)) { 
            this.Write("        pool.Get(out resultBatch.");
            this.Write(this.ToStringHelper.ToStringWithCulture(f.Name));
            this.Write(");\r\n");
 } 
            this.Write("\r\n");
 foreach (var f in this.unassignedFields) { 
 if (!f.OptimizeString()) { 
            this.Write("        this.sharedDefaultColumnFor_");
            this.Write(this.ToStringHelper.ToStringWithCulture(f.Name));
            this.Write(".IncrementRefCount(1);\r\n");
 } 
            this.Write("        resultBatch.");
            this.Write(this.ToStringHelper.ToStringWithCulture(f.Name));
            this.Write(" = this.sharedDefaultColumnFor_");
            this.Write(this.ToStringHelper.ToStringWithCulture(f.Name));
            this.Write(";\r\n");
 } 
            this.Write("\r\n        // Create locals that point directly to the arrays within the columns i" +
                    "n the destination batch.\r\n\r\n");
 foreach (var f in this.computedFields.Keys) { 
 if (f.OptimizeString()) { 
            this.Write("        var dest_");
            this.Write(this.ToStringHelper.ToStringWithCulture(f.Name));
            this.Write(" = resultBatch.");
            this.Write(this.ToStringHelper.ToStringWithCulture(f.Name));
            this.Write(";\r\n");
 } else { 
            this.Write("        var dest_");
            this.Write(this.ToStringHelper.ToStringWithCulture(f.Name));
            this.Write(" = resultBatch.");
            this.Write(this.ToStringHelper.ToStringWithCulture(f.Name));
            this.Write(".col;\r\n");
 } 
 } 
            this.Write("\r\n        // Create locals that point directly to the arrays within the columns i" +
                    "n the source batch.\r\n");
 if (this.ProjectionReturningResultInstance != null || 0 < this.computedFields.Count()) { 
 foreach (var f in this.fields) { 
 if (f.canBeFixed) { 
            this.Write("        fixed (");
            this.Write(this.ToStringHelper.ToStringWithCulture(f.TypeName));
            this.Write("* ");
            this.Write(this.ToStringHelper.ToStringWithCulture(f.Name));
            this.Write("_col = sourceBatch.");
            this.Write(this.ToStringHelper.ToStringWithCulture(f.Name));
            this.Write(".col)\r\n        {\r\n");
 } else { 
            this.Write("        var ");
            this.Write(this.ToStringHelper.ToStringWithCulture(f.Name));
            this.Write("_col = sourceBatch.");
            this.Write(this.ToStringHelper.ToStringWithCulture(f.Name));
            this.Write(".col;\r\n");
 } 
 } 
            this.Write("\r\n        fixed (long* bv = sourceBatch.bitvector.col)\r\n        {\r\n            fo" +
                    "r (int i = 0; i < count; i++)\r\n            {\r\n                if ((bv[i >> 6] & " +
                    "(1L << (i & 0x3f))) == 0)\r\n                {\r\n");
 if (this.StartEdgeParameterName != null) { 
            this.Write("                    var ");
            this.Write(this.ToStringHelper.ToStringWithCulture(this.StartEdgeParameterName));
            this.Write(" = sourceBatch.vsync.col[i] < sourceBatch.vother.col[i] ? sourceBatch.vsync.col[i" +
                    "] : sourceBatch.vother.col[i];\r\n");
 } 
 if (this.needSourceInstance) { 
            this.Write("                    var ");
            this.Write(this.ToStringHelper.ToStringWithCulture(PARAMETER));
            this.Write(" = sourceBatch[i];\r\n");
 } 
 if (this.ProjectionReturningResultInstance != null) { 
            this.Write("                    resultBatch[i] = ");
            this.Write(this.ToStringHelper.ToStringWithCulture(this.ProjectionReturningResultInstance));
            this.Write(";\r\n");
 } else { 
 foreach (var kv in this.computedFields) {
                      var f = kv.Key;
                      var v = kv.Value.ExpressionToCSharp();
                    
 if (f.OptimizeString()) { 
            this.Write("                    dest_");
            this.Write(this.ToStringHelper.ToStringWithCulture(f.Name));
            this.Write(".AddString(");
            this.Write(this.ToStringHelper.ToStringWithCulture(v));
            this.Write(");\r\n");
 } else { 
            this.Write("                    dest_");
            this.Write(this.ToStringHelper.ToStringWithCulture(f.Name));
            this.Write("[i] = ");
            this.Write(this.ToStringHelper.ToStringWithCulture(v));
            this.Write(";\r\n");
 } 
 } 
 } 
            this.Write("                }\r\n            }\r\n        }\r\n\r\n");
 foreach (var f in this.fields.Where(fld => fld.canBeFixed)) { 
            this.Write("        }\r\n");
 } 
 } 
            this.Write("\r\n        #region MultiString Vector Operations (if any)\r\n");
 foreach (var mso in this.multiStringOperations) { 
            this.Write("            ");
            this.Write(this.ToStringHelper.ToStringWithCulture(mso));
            this.Write("\r\n");
 } 
            this.Write(@"        #endregion

        #region Swinging Fields

        // When the query is transformed, then any fields in the result batch that
        // are just assigned from a field in the source batch are computed by just
        // swinging the pointer for the corresponding column.

");
 foreach (var tuple in this.swingingFields) {
          var destField = tuple.Item1.Name;
          var sourceField = tuple.Item2.Name; 
            this.Write("\r\n        // Special case for a field in the output being assigned the key field\r" +
                    "\n        // when both are strings and the output field is a MultiString. The key" +
                    "\r\n        // field is a CB<string> so we need to call MultiString.FromColumnBatc" +
                    "h\r\n");
 if (tuple.Item1.OptimizeString() && sourceField.Equals("key")) { 
            this.Write("        resultBatch.");
            this.Write(this.ToStringHelper.ToStringWithCulture(destField));
            this.Write(" = MultiString.FromColumnBatch(sourceBatch.");
            this.Write(this.ToStringHelper.ToStringWithCulture(sourceField));
            this.Write(", sourceBatch.bitvector, pool.charArrayPool, pool.intPool, pool.shortPool, pool.b" +
                    "itvectorPool);\r\n");
 } else { 
            this.Write("        resultBatch.");
            this.Write(this.ToStringHelper.ToStringWithCulture(destField));
            this.Write(" = sourceBatch.");
            this.Write(this.ToStringHelper.ToStringWithCulture(sourceField));
            this.Write(";\r\n");
 } 
 } 
            this.Write(@"
        // We avoid incrementing the ref counts on the ouput batch because for the
        // most part we are throwing away our reference to the columns from the source batch
        // so the reference count doesn't change. But the same field might get swung to more
        // than one column in the output batch. In that case, we need to increment the ref count.
");
 foreach (var kv in this.swungFieldsCount) {
          var sourceField = kv.Key;
          var count = kv.Value;
          if (sourceField.Name.Equals("key") && !sourceField.OptimizeString()) {
            // special case: we "pointer swing" the key field once just because the key
            // of the output batch is the key of the source batch
            // (Note that this really applies only to SelectKey since Select is not able
            // to reference the key field in the selector.)
            count++;
          }
          if (count == 1) continue; 
            this.Write("        sourceBatch.");
            this.Write(this.ToStringHelper.ToStringWithCulture(sourceField.Name));
            this.Write(".IncrementRefCount(");
            this.Write(this.ToStringHelper.ToStringWithCulture(count - 1));
            this.Write(");\r\n");
 } 
            this.Write(@"        #endregion

        resultBatch.Count = count;
        resultBatch.Seal();

        // Return source columns as necessary.
        // When the query is transformed, this is the ""non-swinging"" fields.
        // Otherwise it is all source fields
");
 foreach (var sourceField in this.nonSwingingFields) { 
 if (sourceField.OptimizeString()) { 
            this.Write("        sourceBatch.");
            this.Write(this.ToStringHelper.ToStringWithCulture(sourceField.Name));
            this.Write(".Dispose();\r\n");
 } else { 
            this.Write("        sourceBatch.");
            this.Write(this.ToStringHelper.ToStringWithCulture(sourceField.Name));
            this.Write(".Return();\r\n");
 } 
 } 
 if (payloadType.CanContainNull()) { 
            this.Write("        sourceBatch._nullnessvector.ReturnClear();\r\n");
 } 
            this.Write("\r\n        sourceBatch.Return();\r\n        this.Observer.OnNext(resultBatch);\r\n    " +
                    "}\r\n\r\n    public override int CurrentlyBufferedOutputCount => 0;\r\n\r\n    public ov" +
                    "erride int CurrentlyBufferedInputCount => 0;\r\n}\r\n");
            return this.GenerationEnvironment.ToString();
        }
    }
}
