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
    using System.Text;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Reflection;
    using System;
    
    /// <summary>
    /// Class to produce the template output
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "15.0.0.0")]
    internal partial class UnionTemplate : CommonBaseTemplate
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

// TKey: ");
            this.Write(this.ToStringHelper.ToStringWithCulture(TKey));
            this.Write("\r\n// TPayload: ");
            this.Write(this.ToStringHelper.ToStringWithCulture(TPayload));
            this.Write("\r\n\r\n[DataContract]\r\ninternal sealed class ");
            this.Write(this.ToStringHelper.ToStringWithCulture(className));
            this.Write(this.ToStringHelper.ToStringWithCulture(genericParameters));
            this.Write(" : BinaryPipe<");
            this.Write(this.ToStringHelper.ToStringWithCulture(TKey));
            this.Write(", ");
            this.Write(this.ToStringHelper.ToStringWithCulture(TPayload));
            this.Write(", ");
            this.Write(this.ToStringHelper.ToStringWithCulture(TPayload));
            this.Write(", ");
            this.Write(this.ToStringHelper.ToStringWithCulture(TPayload));
            this.Write(">\r\n{\r\n    private readonly MemoryPool<");
            this.Write(this.ToStringHelper.ToStringWithCulture(TKey));
            this.Write(", ");
            this.Write(this.ToStringHelper.ToStringWithCulture(TPayload));
            this.Write("> pool;\r\n\r\n    private StreamMessage<");
            this.Write(this.ToStringHelper.ToStringWithCulture(TKey));
            this.Write(", ");
            this.Write(this.ToStringHelper.ToStringWithCulture(TPayload));
            this.Write("> genericOutputBatch;\r\n    [DataMember]\r\n    private ");
            this.Write(this.ToStringHelper.ToStringWithCulture(GeneratedBatchName));
            this.Write(@" output;
    [DataMember]
    private long nextLeftTime = long.MinValue;
    [DataMember]
    private long nextRightTime = long.MinValue;

    private readonly Func<PlanNode, PlanNode, IBinaryObserver, BinaryPlanNode> queryPlanGenerator;

    [Obsolete(""Used only by serialization. Do not call directly."")]
    public ");
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
            this.Write("> observer,\r\n        Func<PlanNode, PlanNode, IBinaryObserver, BinaryPlanNode> qu" +
                    "eryPlanGenerator)\r\n        : base(stream, observer)\r\n    {\r\n        this.queryPl" +
                    "anGenerator = queryPlanGenerator;\r\n        this.pool = MemoryManager.GetMemoryPo" +
                    "ol<");
            this.Write(this.ToStringHelper.ToStringWithCulture(TKey));
            this.Write(", ");
            this.Write(this.ToStringHelper.ToStringWithCulture(TPayload));
            this.Write(@">(true /*stream.Properties.IsColumnar*/);
        GetOutputBatch();
    }

    public override int CurrentlyBufferedOutputCount => output.Count;

    private void GetOutputBatch()
    {
        this.pool.Get(out this.genericOutputBatch);
        this.genericOutputBatch.Allocate();
        this.output = (");
            this.Write(this.ToStringHelper.ToStringWithCulture(GeneratedBatchName));
            this.Write(")this.genericOutputBatch;\r\n");
 foreach (var f in this.fields.Where(fld => fld.OptimizeString())) {  
            this.Write("\r\n        this.output.");
            this.Write(this.ToStringHelper.ToStringWithCulture(f.Name));
            this.Write(".Initialize();\r\n");
 } 
            this.Write("}\r\n\r\n    [MethodImpl(MethodImplOptions.AggressiveInlining)]\r\n    protected overri" +
                    "de void DisposeState() => this.output.Free();\r\n\r\n    [MethodImpl(MethodImplOptio" +
                    "ns.AggressiveInlining)]\r\n    protected override void ProcessBothBatches(StreamMe" +
                    "ssage<");
            this.Write(this.ToStringHelper.ToStringWithCulture(TKey));
            this.Write(", ");
            this.Write(this.ToStringHelper.ToStringWithCulture(TPayload));
            this.Write("> leftBatch, StreamMessage<");
            this.Write(this.ToStringHelper.ToStringWithCulture(TKey));
            this.Write(", ");
            this.Write(this.ToStringHelper.ToStringWithCulture(TPayload));
            this.Write("> rightBatch, out bool leftBatchDone, out bool rightBatchDone, out bool leftBatch" +
                    "Free, out bool rightBatchFree)\r\n    {\r\n        leftBatchFree = rightBatchFree = " +
                    "true;\r\n\r\n        long lastLeftTime = -1;\r\n        long lastRightTime = -1;\r\n\r\n  " +
                    "      bool first = (leftBatch.iter == 0);\r\n        if (!GoToVisibleRow(leftBatch" +
                    "))\r\n        {\r\n            leftBatchDone = true;\r\n            rightBatchDone = f" +
                    "alse;\r\n            return;\r\n        }\r\n\r\n        this.nextLeftTime = leftBatch.v" +
                    "sync.col[leftBatch.iter];\r\n        if (first) lastLeftTime = leftBatch.vsync.col" +
                    "[leftBatch.Count - 1];\r\n\r\n        first = (rightBatch.iter == 0);\r\n        if (!" +
                    "GoToVisibleRow(rightBatch))\r\n        {\r\n            leftBatchDone = false;\r\n    " +
                    "        rightBatchDone = true;\r\n\r\n            return;\r\n        }\r\n\r\n        this" +
                    ".nextRightTime = rightBatch.vsync.col[rightBatch.iter];\r\n        if (first) last" +
                    "RightTime = rightBatch.vsync.col[rightBatch.Count - 1];\r\n\r\n        if ((lastLeft" +
                    "Time != -1) && (lastRightTime != -1))\r\n        {\r\n            leftBatchDone = ri" +
                    "ghtBatchDone = false;\r\n            if (lastLeftTime <= this.nextRightTime)\r\n    " +
                    "        {\r\n                OutputBatch(leftBatch);\r\n                leftBatchDon" +
                    "e = true;\r\n                leftBatchFree = false;\r\n            }\r\n\r\n            " +
                    "if (Config.DeterministicWithinTimestamp ? (lastRightTime < this.nextLeftTime) : " +
                    "(lastRightTime <= this.nextLeftTime))\r\n            {\r\n                OutputBatc" +
                    "h(rightBatch);\r\n                rightBatchDone = true;\r\n                rightBat" +
                    "chFree = false;\r\n            }\r\n\r\n            if (leftBatchDone || rightBatchDon" +
                    "e) return;\r\n        }\r\n\r\n        while (true)\r\n        {\r\n            if (this.n" +
                    "extLeftTime <= this.nextRightTime)\r\n            {\r\n                OutputCurrent" +
                    "Tuple(leftBatch);\r\n\r\n                leftBatch.iter++;\r\n\r\n                if (!G" +
                    "oToVisibleRow(leftBatch))\r\n                {\r\n                    leftBatchDone " +
                    "= true;\r\n                    rightBatchDone = false;\r\n                    return" +
                    ";\r\n                }\r\n\r\n                this.nextLeftTime = leftBatch.vsync.col[" +
                    "leftBatch.iter];\r\n            }\r\n            else\r\n            {\r\n              " +
                    "  OutputCurrentTuple(rightBatch);\r\n\r\n                rightBatch.iter++;\r\n\r\n     " +
                    "           if (!GoToVisibleRow(rightBatch))\r\n                {\r\n                " +
                    "    leftBatchDone = false;\r\n                    rightBatchDone = true;\r\n        " +
                    "            return;\r\n                }\r\n\r\n                this.nextRightTime = r" +
                    "ightBatch.vsync.col[rightBatch.iter];\r\n            }\r\n        }\r\n    }\r\n\r\n    [M" +
                    "ethodImpl(MethodImplOptions.AggressiveInlining)]\r\n    protected override void Pr" +
                    "ocessLeftBatch(StreamMessage<");
            this.Write(this.ToStringHelper.ToStringWithCulture(TKey));
            this.Write(", ");
            this.Write(this.ToStringHelper.ToStringWithCulture(TPayload));
            this.Write(@"> batch, out bool isBatchDone, out bool isBatchFree)
    {
        isBatchFree = true;
        if (batch.iter == 0)
        {
            if (batch.vsync.col[batch.Count - 1] <= this.nextRightTime)
            {
                OutputBatch(batch);
                isBatchDone = true;
                isBatchFree = false;
                return;
            }
        }

        while (true)
        {
            if (!GoToVisibleRow(batch))
            {
                isBatchDone = true;
                return;
            }

            this.nextLeftTime = batch.vsync.col[batch.iter];

            if (this.nextLeftTime > this.nextRightTime)
            {
                isBatchDone = false;
                return;
            }

            OutputCurrentTuple(batch);

            batch.iter++;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected override void ProcessRightBatch(StreamMessage<");
            this.Write(this.ToStringHelper.ToStringWithCulture(TKey));
            this.Write(", ");
            this.Write(this.ToStringHelper.ToStringWithCulture(TPayload));
            this.Write(@"> batch, out bool isBatchDone, out bool isBatchFree)
    {
        isBatchFree = true;
        if (batch.iter == 0)
        {
            if (Config.DeterministicWithinTimestamp ? (batch.vsync.col[batch.Count - 1] < this.nextLeftTime) : (batch.vsync.col[batch.Count - 1] <= this.nextLeftTime))
            {
                OutputBatch(batch);
                isBatchDone = true;
                isBatchFree = false;
                return;
            }
        }

        while (true)
        {
            if (!GoToVisibleRow(batch))
            {
                isBatchDone = true;
                return;
            }

            this.nextRightTime = batch.vsync.col[batch.iter];

            if (this.nextRightTime >= this.nextLeftTime)
            {
                isBatchDone = false;
                return;
            }

            OutputCurrentTuple(batch);

            batch.iter++;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private bool GoToVisibleRow(StreamMessage<");
            this.Write(this.ToStringHelper.ToStringWithCulture(TKey));
            this.Write(", ");
            this.Write(this.ToStringHelper.ToStringWithCulture(TPayload));
            this.Write(@"> batch)
    {
        while (batch.iter < batch.Count && (batch.bitvector.col[batch.iter >> 6] & (1L << (batch.iter & 0x3f))) != 0 && batch.vother.col[batch.iter] >= 0)
            batch.iter++;

        return batch.iter != batch.Count;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void OutputCurrentTuple(StreamMessage<");
            this.Write(this.ToStringHelper.ToStringWithCulture(TKey));
            this.Write(", ");
            this.Write(this.ToStringHelper.ToStringWithCulture(TPayload));
            this.Write(@"> batch)
    {
        if (batch.vother.col[batch.iter] == StreamEvent.PunctuationOtherTime)
        {
            if (batch.vsync.col[batch.iter] <= lastCTI) return;

            lastCTI = batch.vsync.col[batch.iter];
        }

        var inputBatch = (");
            this.Write(this.ToStringHelper.ToStringWithCulture(GeneratedBatchName));
            this.Write(@") batch;
        int index = this.output.Count++;
        var batchIndex = batch.iter;
        this.output.vsync.col[index] = batch.vsync.col[batchIndex];
        this.output.vother.col[index] = batch.vother.col[batchIndex];
        this.output.key.col[index] = batch.key.col[batchIndex];
        this.output[index] = batch[batchIndex];

        this.output.hash.col[index] = batch.hash.col[batch.iter];
        if ((batch.bitvector.col[batch.iter >> 6] & (1L << (batch.iter & 0x3f))) != 0)
            this.output.bitvector.col[index >> 6] |= (1L << (index & 0x3f));

        if (this.output.Count == Config.DataBatchSize) FlushContents();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void OutputBatch(StreamMessage<");
            this.Write(this.ToStringHelper.ToStringWithCulture(TKey));
            this.Write(", ");
            this.Write(this.ToStringHelper.ToStringWithCulture(TPayload));
            this.Write(@"> batch)
    {
        long updatedCTI = lastCTI;
        for (int i = 0; i < batch.Count; i++)
        {
            // Find first punctuation
            if (batch.vother.col[i] == StreamEvent.PunctuationOtherTime)
            {
                if (batch.vsync.col[i] <= updatedCTI)
                {
                    // Remove the redundant punctuation by converting to a deleted data event
                    batch.vother.col[i] = 0;
                    batch.bitvector.col[i >> 6] |= (1L << (i & 0x3f));
                }
                else
                    updatedCTI = batch.vsync.col[i];
            }
        }
        lastCTI = updatedCTI;

        FlushContents();
        this.Observer.OnNext(batch);
    }

    protected override void ProduceBinaryQueryPlan(PlanNode left, PlanNode right)
    {
        this.Observer.ProduceQueryPlan(queryPlanGenerator(left, right, this));
    }

    protected override void FlushContents()
    {
        if (this.output.Count == 0) return;
        this.output.Seal();
        this.Observer.OnNext(this.output);
        GetOutputBatch();
    }
}
");
            return this.GenerationEnvironment.ToString();
        }
    }
}
