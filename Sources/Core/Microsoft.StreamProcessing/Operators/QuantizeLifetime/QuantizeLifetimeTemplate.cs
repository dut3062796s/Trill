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
    internal partial class QuantizeLifetimeTemplate : CommonUnaryTemplate
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
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Threading;
using Microsoft.StreamProcessing;
using Microsoft.StreamProcessing.Aggregates;
using Microsoft.StreamProcessing.Internal;
using Microsoft.StreamProcessing.Internal.Collections;

[DataContract]
internal sealed class ");
            this.Write(this.ToStringHelper.ToStringWithCulture(className));
            this.Write(this.ToStringHelper.ToStringWithCulture(TKeyTPayloadGenericParameters));
            this.Write(" : UnaryPipe<");
            this.Write(this.ToStringHelper.ToStringWithCulture(TKey));
            this.Write(", ");
            this.Write(this.ToStringHelper.ToStringWithCulture(TPayload));
            this.Write(", ");
            this.Write(this.ToStringHelper.ToStringWithCulture(TPayload));
            this.Write(">\r\n{\r\n    private readonly MemoryPool<");
            this.Write(this.ToStringHelper.ToStringWithCulture(TKey));
            this.Write(", ");
            this.Write(this.ToStringHelper.ToStringWithCulture(TPayload));
            this.Write(@"> pool;
    private readonly Func<PlanNode, IQueryObject, PlanNode> queryPlanGenerator;

    [SchemaSerialization]
    private readonly long width;
    [SchemaSerialization]
    private readonly long skip;
    [SchemaSerialization]
    private readonly long offset;

    private StreamMessage<");
            this.Write(this.ToStringHelper.ToStringWithCulture(TKey));
            this.Write(", ");
            this.Write(this.ToStringHelper.ToStringWithCulture(TPayload));
            this.Write("> genericOutputBatch;\r\n    [DataMember]\r\n    private ");
            this.Write(this.ToStringHelper.ToStringWithCulture(BatchGeneratedFrom_TKey_TPayload));
            this.Write(this.ToStringHelper.ToStringWithCulture(TKeyTPayloadGenericParameters));
            this.Write(@" output;

    [DataMember]
    private long lastSyncTime = long.MinValue;
    [DataMember]
    private EndPointHeap endPointHeap = new EndPointHeap();
    [DataMember]
    private FastMap<ActiveEvent> intervalMap = new FastMap<ActiveEvent>();

    ");
            this.Write(this.ToStringHelper.ToStringWithCulture(staticCtor));
            this.Write("\r\n\r\n    [Obsolete(\"Used only by serialization. Do not call directly.\")]\r\n    publ" +
                    "ic ");
            this.Write(this.ToStringHelper.ToStringWithCulture(className));
            this.Write("() { }\r\n\r\n    public ");
            this.Write(this.ToStringHelper.ToStringWithCulture(className));
            this.Write("(\r\n        IStreamable<");
            this.Write(this.ToStringHelper.ToStringWithCulture(TKey));
            this.Write(", ");
            this.Write(this.ToStringHelper.ToStringWithCulture(TPayload));
            this.Write("> stream,\r\n        IStreamObserver<");
            this.Write(this.ToStringHelper.ToStringWithCulture(TKey));
            this.Write(", ");
            this.Write(this.ToStringHelper.ToStringWithCulture(TPayload));
            this.Write("> observer, long width, long skip, long offset,\r\n        Func<PlanNode, IQueryObj" +
                    "ect, PlanNode> queryPlanGenerator)\r\n        : base(stream, observer)\r\n    {\r\n   " +
                    "     pool = MemoryManager.GetMemoryPool<");
            this.Write(this.ToStringHelper.ToStringWithCulture(TKey));
            this.Write(", ");
            this.Write(this.ToStringHelper.ToStringWithCulture(TPayload));
            this.Write(@">(true /*stream.Properties.IsColumnar*/);
        this.queryPlanGenerator = queryPlanGenerator;
        GetOutputBatch();
        this.width = width;
        this.skip = skip;
        this.offset = offset;
    }

    private void GetOutputBatch()
    {
        pool.Get(out genericOutputBatch);
        genericOutputBatch.Allocate();
        output = (");
            this.Write(this.ToStringHelper.ToStringWithCulture(BatchGeneratedFrom_TKey_TPayload));
            this.Write(this.ToStringHelper.ToStringWithCulture(TKeyTPayloadGenericParameters));
            this.Write(")genericOutputBatch;\r\n");
 foreach (var f in this.fields.Where(fld => fld.OptimizeString())) {  
            this.Write("        output.");
            this.Write(this.ToStringHelper.ToStringWithCulture(f.Name));
            this.Write(".Initialize();\r\n");
 } 
            this.Write(@"   }

    public override void ProduceQueryPlan(PlanNode previous)
    {
        Observer.ProduceQueryPlan(queryPlanGenerator(previous, this));
    }

    private void ReachTime(long timestamp)
    {
        long endPointTime;
        int index;
        while (endPointHeap.TryGetNextInclusive(timestamp, out endPointTime, out index))
        {
            int ind = output.Count++;
            var interval = intervalMap.Values[index];
            output.vsync.col[ind] = endPointTime;
            output.vother.col[ind] = interval.Other;
            output.key.col[ind] = interval.Key;
");
     foreach (var f in this.fields) { 
       if (f.OptimizeString()) { 
            this.Write("\r\n            output.");
            this.Write(this.ToStringHelper.ToStringWithCulture(f.Name));
            this.Write(".AddString(interval.");
            this.Write(this.ToStringHelper.ToStringWithCulture(f.Name));
            this.Write(");\r\n");
       } else { 
            this.Write("            output.");
            this.Write(this.ToStringHelper.ToStringWithCulture(f.Name));
            this.Write(".col[ind] = interval.");
            this.Write(this.ToStringHelper.ToStringWithCulture(f.Name));
            this.Write(";\r\n");
       } 
     } 
            this.Write(@"            output.hash.col[ind] = interval.Hash;

            if (output.Count == Config.DataBatchSize) FlushContents();

            intervalMap.Remove(index);
        }
        lastSyncTime = timestamp;
    }

    public override unsafe void OnNext(StreamMessage<");
            this.Write(this.ToStringHelper.ToStringWithCulture(TKey));
            this.Write(", ");
            this.Write(this.ToStringHelper.ToStringWithCulture(TPayload));
            this.Write("> genericBatch)\r\n    {\r\n        var batch = genericBatch as ");
            this.Write(this.ToStringHelper.ToStringWithCulture(BatchGeneratedFrom_TKey_TPayload));
            this.Write(this.ToStringHelper.ToStringWithCulture(TKeyTPayloadGenericParameters));
            this.Write(";\r\n        var count = batch.Count;\r\n\r\n        fixed (long* bv = batch.bitvector." +
                    "col)\r\n        fixed (long* vsync = batch.vsync.col)\r\n        fixed (long* vother" +
                    " = batch.vother.col)\r\n        {\r\n            for (int i = 0; i < count; i++)\r\n  " +
                    "          {\r\n                if ((bv[i >> 6] & (1L << (i & 0x3f))) == 0)\r\n      " +
                    "          {\r\n                    if (batch.vsync.col[i] > lastSyncTime) ReachTim" +
                    "e(batch.vsync.col[i]);\r\n\r\n                    if (batch.vother.col[i] == StreamE" +
                    "vent.InfinitySyncTime) // Start edge\r\n                    {\r\n                   " +
                    "     int ind = output.Count++;\r\n                        output.vsync.col[ind] = " +
                    "vsync[i] - ((vsync[i] - offset) % skip);\r\n                        output.vother." +
                    "col[ind] = StreamEvent.InfinitySyncTime;\r\n                        output.key.col" +
                    "[ind] = batch.key.col[i];\r\n                        output[ind] = batch[i];\r\n    " +
                    "                    output.hash.col[ind] = batch.hash.col[i];\r\n\r\n               " +
                    "         if (output.Count == Config.DataBatchSize) FlushContents();\r\n           " +
                    "         }\r\n                    else if (batch.vother.col[i] > batch.vsync.col[i" +
                    "]) // Interval\r\n                    {\r\n                        int ind = output." +
                    "Count++;\r\n                        output.vsync.col[ind] = vsync[i] - ((vsync[i] " +
                    "- offset) % skip);\r\n                        var temp = Math.Max(vother[i] + skip" +
                    " - 1, vsync[i] + width);\r\n                        output.vother.col[ind] = temp " +
                    "- ((temp - (offset + width)) % skip);\r\n                        output.key.col[in" +
                    "d] = batch.key.col[i];\r\n                        output[ind] = batch[i];\r\n       " +
                    "                 output.hash.col[ind] = batch.hash.col[i];\r\n\r\n                  " +
                    "      if (output.Count == Config.DataBatchSize) FlushContents();\r\n              " +
                    "      }\r\n                    else\r\n                    {\r\n                      " +
                    "  var temp = Math.Max(vsync[i] + skip - 1, vother[i] + width);\r\n                " +
                    "        int index = intervalMap.Insert(batch.hash.col[i]);\r\n                    " +
                    "    intervalMap.Values[index].Populate(batch.key.col[i], batch, i, batch.hash.co" +
                    "l[i], vother[i] - ((vother[i] - offset) % skip));\r\n                        endPo" +
                    "intHeap.Insert(temp - ((temp - (offset + width)) % skip), index);\r\n             " +
                    "       }\r\n                }\r\n                else if (vother[i] == long.MinValue" +
                    ") // Punctuation\r\n                {\r\n                    if (vsync[i] > lastSync" +
                    "Time) ReachTime(vsync[i]);\r\n\r\n                    int ind = output.Count++;\r\n   " +
                    "                 output.vsync.col[ind] = vsync[i] - ((vsync[i] - offset) % skip)" +
                    ";\r\n                    output.vother.col[ind] = long.MinValue;\r\n                " +
                    "    output.key.col[ind] = default;\r\n                    output[ind] = default;\r\n" +
                    "                    output.hash.col[ind] = batch.hash.col[i];\r\n                 " +
                    "   output.bitvector.col[ind >> 6] |= (1L << (ind & 0x3f));\r\n                    " +
                    "if (output.Count == Config.DataBatchSize) FlushContents();\r\n                }\r\n " +
                    "           }\r\n        }\r\n        batch.Free();\r\n    }\r\n\r\n    protected override " +
                    "void FlushContents()\r\n    {\r\n        if (output.Count == 0) return;\r\n        out" +
                    "put.Seal();\r\n        this.Observer.OnNext(output);\r\n        GetOutputBatch();\r\n " +
                    "   }\r\n\r\n    protected override void DisposeState() => output.Free();\r\n\r\n    publ" +
                    "ic override int CurrentlyBufferedOutputCount => output.Count;\r\n\r\n    public over" +
                    "ride int CurrentlyBufferedInputCount => intervalMap.Count;\r\n\r\n    [DataContract]" +
                    "\r\n    private struct ActiveEvent\r\n    {\r\n");
 foreach (var f in this.fields) { 
            this.Write("        [DataMember]\r\n        public ");
            this.Write(this.ToStringHelper.ToStringWithCulture(f.Type.GetCSharpSourceSyntax()));
            this.Write(" ");
            this.Write(this.ToStringHelper.ToStringWithCulture(f.Name));
            this.Write(";\r\n");
 } 
            this.Write("        [DataMember]\r\n        public ");
            this.Write(this.ToStringHelper.ToStringWithCulture(TKey));
            this.Write(" Key;\r\n        [DataMember]\r\n        public int Hash;\r\n        [DataMember]\r\n    " +
                    "    public long Other;\r\n\r\n        public void Populate(");
            this.Write(this.ToStringHelper.ToStringWithCulture(TKey));
            this.Write(" key, ");
            this.Write(this.ToStringHelper.ToStringWithCulture(BatchGeneratedFrom_TKey_TPayload));
            this.Write(this.ToStringHelper.ToStringWithCulture(TKeyTPayloadGenericParameters));
            this.Write(" batch, int index, int hash, long other)\r\n        {\r\n            this.Key = key;\r" +
                    "\n            //this.Payload = payload;\r\n");
 foreach (var f in this.fields) { 
            this.Write("            this.");
            this.Write(this.ToStringHelper.ToStringWithCulture(f.Name));
            this.Write(" = ");
            this.Write(this.ToStringHelper.ToStringWithCulture(f.AccessExpressionForRowValue("batch", "index")));
            this.Write(";\r\n");
 } 
            this.Write("            this.Hash = hash;\r\n            this.Other = other;\r\n        }\r\n\r\n    " +
                    "    public override string ToString()\r\n        {\r\n            return \"Key=\'\" + K" +
                    "ey + \"\', Payload=\'\"; // + Payload;\r\n        }\r\n    }\r\n\r\n}\r\n");
            return this.GenerationEnvironment.ToString();
        }
    }
}
