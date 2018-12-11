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
    using System.Collections.Generic;
    using System;
    
    /// <summary>
    /// Class to produce the template output
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "15.0.0.0")]
    internal partial class SnapshotWindowStartEdgeTemplate : AggregateTemplate
    {
        /// <summary>
        /// Create the template output
        /// </summary>
        public override string TransformText()
        {
            this.Write("\r\n");
            this.Write(@"// *********************************************************************
// Copyright (c) Microsoft Corporation.  All rights reserved.
// Licensed under the MIT License
// *********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

using Microsoft.StreamProcessing;
using Microsoft.StreamProcessing.Internal;
using Microsoft.StreamProcessing.Internal.Collections;
using Microsoft.StreamProcessing.Aggregates;

");


  List<string> genericParamList = new List<string>();
  int oldCount = 0;
  var TKey = keyType.GetCSharpSourceSyntax(ref genericParamList);
  var keyGenericParameters = new List<string>(genericParamList.Skip(oldCount));

  oldCount = genericParamList.Count;
  var TInput = inputType.GetCSharpSourceSyntax(ref genericParamList);
  var inputGenericParameters = new List<string>(genericParamList.Skip(oldCount));

  oldCount = genericParamList.Count;
  var TState = stateType.GetCSharpSourceSyntax(ref genericParamList);
  var stateGenericParameters = new List<string>(genericParamList.Skip(oldCount));

  oldCount = genericParamList.Count;
  var TOutput = outputType.GetCSharpSourceSyntax(ref genericParamList);
  var outputGenericParameters = new List<string>(genericParamList.Skip(oldCount));

  var genericParameters = genericParamList.BracketedCommaSeparatedString();
  var TKeyTInputGenericParameters = keyGenericParameters.Concat(inputGenericParameters).BracketedCommaSeparatedString();
  var TKeyTOutputGenericParameters = keyGenericParameters.Concat(outputGenericParameters).BracketedCommaSeparatedString();

  var BatchGeneratedFrom_TKey_TInput = Transformer.GetBatchClassName(keyType, inputType);

  var genericParameters2 = string.Format("<{0}, {1}>", TKey, TOutput);
  if (!keyType.KeyTypeNeedsGeneratedMemoryPool() && outputType.MemoryPoolHasGetMethodFor())
      genericParameters2 = string.Empty;
  else if (!outputType.CanRepresentAsColumnar())
      genericParameters2 = string.Empty;

  Func<string, string> assignToOutput = rhs =>
    this.outputType.IsAnonymousType()
    ?
    rhs
    :
    (
    this.outputFields.Count() == 1
    ?
    string.Format("this.batch.{0}.col[c] = {1};", this.outputFields.First().Name, rhs)
    :
    "temporaryOutput = " + rhs + ";\r\n" + String.Join("\r\n", this.outputFields.Select(f => "dest_" + f.Name + "[c] = temporaryOutput." + f.OriginalName + ";")))
    ;

  var getOutputBatch = string.Format("this.pool.Get(out genericOutputbatch); this.batch = ({0}{1})genericOutputbatch;",
          Transformer.GetBatchClassName(keyType, outputType),
          TKeyTOutputGenericParameters);


            this.Write("[assembly: IgnoresAccessChecksTo(\"Microsoft.StreamProcessing\")]\r\n\r\n// genericPara" +
                    "ms2 = \"");
            this.Write(this.ToStringHelper.ToStringWithCulture(genericParameters2));
            this.Write("\"\r\n\r\n[DataContract]\r\nstruct StateAndActive\r\n{\r\n    [DataMember]\r\n    public ");
            this.Write(this.ToStringHelper.ToStringWithCulture(TState));
            this.Write(" state;\r\n    [DataMember]\r\n    public ulong active;\r\n}\r\n\r\n[DataContract]\r\nstruct " +
                    "HeldStateStruct\r\n{\r\n    [DataMember]\r\n    public long timestamp;\r\n    [DataMembe" +
                    "r]\r\n    public ");
            this.Write(this.ToStringHelper.ToStringWithCulture(TState));
            this.Write(" state;\r\n}\r\n\r\n[DataContract]\r\nsealed class HeldState\r\n{\r\n    [DataMember]\r\n    pu" +
                    "blic long timestamp;\r\n    [DataMember]\r\n    public StateAndActive state;\r\n}\r\n\r\n/" +
                    "/ TKey: ");
            this.Write(this.ToStringHelper.ToStringWithCulture(TKey));
            this.Write("\r\n// TInput: ");
            this.Write(this.ToStringHelper.ToStringWithCulture(TInput));
            this.Write("\r\n// TState: ");
            this.Write(this.ToStringHelper.ToStringWithCulture(TState));
            this.Write("\r\n// TOutput: ");
            this.Write(this.ToStringHelper.ToStringWithCulture(TOutput));
            this.Write("\r\n/// <summary>\r\n/// Operator only has to deal with start edges\r\n/// </summary>\r\n" +
                    "[DataContract]\r\ninternal sealed class ");
            this.Write(this.ToStringHelper.ToStringWithCulture(className));
            this.Write(this.ToStringHelper.ToStringWithCulture(genericParameters));
            this.Write(" : UnaryPipe<");
            this.Write(this.ToStringHelper.ToStringWithCulture(TKey));
            this.Write(", ");
            this.Write(this.ToStringHelper.ToStringWithCulture(TInput));
            this.Write(", ");
            this.Write(this.ToStringHelper.ToStringWithCulture(TOutput));
            this.Write(">\r\n{\r\n    private readonly Func<PlanNode, IQueryObject, PlanNode> queryPlanGenera" +
                    "tor;\r\n    private readonly IAggregate<");
            this.Write(this.ToStringHelper.ToStringWithCulture(TInput));
            this.Write(", ");
            this.Write(this.ToStringHelper.ToStringWithCulture(TState));
            this.Write(", ");
            this.Write(this.ToStringHelper.ToStringWithCulture(TOutput));
            this.Write("> aggregate;\r\n    ");
            this.Write(this.ToStringHelper.ToStringWithCulture(Transformer.GetMemoryPoolClassName(this.keyType, this.outputType)));
            this.Write(this.ToStringHelper.ToStringWithCulture(genericParameters2));
            this.Write(" pool;\r\n\r\n    private StreamMessage<");
            this.Write(this.ToStringHelper.ToStringWithCulture(TKey));
            this.Write(", ");
            this.Write(this.ToStringHelper.ToStringWithCulture(TOutput));
            this.Write("> genericOutputbatch;\r\n    [DataMember]\r\n    private ");
            this.Write(this.ToStringHelper.ToStringWithCulture(Transformer.GetBatchClassName(keyType, outputType)));
            this.Write(this.ToStringHelper.ToStringWithCulture(TKeyTOutputGenericParameters));
            this.Write(" batch;\r\n\r\n    ");
 if (this.useCompiledInitialState) { 
            this.Write("\r\n    private readonly Func<");
            this.Write(this.ToStringHelper.ToStringWithCulture(TState));
            this.Write("> initialState;\r\n    ");
 } 
            this.Write("    ");
 if (this.useCompiledAccumulate) { 
            this.Write("    private readonly Func<");
            this.Write(this.ToStringHelper.ToStringWithCulture(TState));
            this.Write(", long, ");
            this.Write(this.ToStringHelper.ToStringWithCulture(TInput));
            this.Write(", ");
            this.Write(this.ToStringHelper.ToStringWithCulture(TState));
            this.Write("> accumulate;\r\n    ");
 } 
            this.Write("    ");
 if (this.useCompiledDeaccumulate) { 
            this.Write("\r\n    private readonly Func<");
            this.Write(this.ToStringHelper.ToStringWithCulture(TState));
            this.Write(", ");
            this.Write(this.ToStringHelper.ToStringWithCulture(TOutput));
            this.Write("> computeResult;\r\n    ");
 } 
            this.Write("\r\n    private readonly IEqualityComparerExpression<");
            this.Write(this.ToStringHelper.ToStringWithCulture(TKey));
            this.Write("> keyComparer;\r\n\r\n    ");
 if (!this.isUngrouped) { 
            this.Write("\r\n    [DataMember]\r\n    private FastDictionary3<");
            this.Write(this.ToStringHelper.ToStringWithCulture(TKey));
            this.Write(", HeldStateStruct> heldAggregates;\r\n    ");
 } else { 
            this.Write("\r\n    [DataMember]\r\n    private HeldStateStruct currentState;\r\n    [DataMember]\r\n" +
                    "    private bool currentStateExists;\r\n    [DataMember]\r\n    private bool isDirty" +
                    ";\r\n    ");
 } 
            this.Write("\r\n    [DataMember]\r\n    private long lastSyncTime = long.MinValue;\r\n\r\n    ");
            this.Write(this.ToStringHelper.ToStringWithCulture(staticCtor));
            this.Write("\r\n\r\n    public ");
            this.Write(this.ToStringHelper.ToStringWithCulture(className));
            this.Write("() { }\r\n\r\n    public ");
            this.Write(this.ToStringHelper.ToStringWithCulture(className));
            this.Write("(\r\n        Streamable<");
            this.Write(this.ToStringHelper.ToStringWithCulture(TKey));
            this.Write(", ");
            this.Write(this.ToStringHelper.ToStringWithCulture(TOutput));
            this.Write("> stream,\r\n        IStreamObserver<");
            this.Write(this.ToStringHelper.ToStringWithCulture(TKey));
            this.Write(", ");
            this.Write(this.ToStringHelper.ToStringWithCulture(TOutput));
            this.Write("> observer,\r\n        Func<PlanNode, IQueryObject, PlanNode> queryPlanGenerator,\r\n" +
                    "        IAggregate<");
            this.Write(this.ToStringHelper.ToStringWithCulture(TInput));
            this.Write(", ");
            this.Write(this.ToStringHelper.ToStringWithCulture(TState));
            this.Write(", ");
            this.Write(this.ToStringHelper.ToStringWithCulture(TOutput));
            this.Write("> aggregate)\r\n        : base(stream, observer)\r\n    {\r\n        this.aggregate = a" +
                    "ggregate;\r\n        this.queryPlanGenerator = queryPlanGenerator;\r\n\r\n        ");
 if (this.useCompiledInitialState) { 
            this.Write("\r\n        initialState = aggregate.InitialState().Compile();\r\n        ");
 } 
            this.Write("        ");
 if (this.useCompiledAccumulate) { 
            this.Write("\r\n        accumulate = aggregate.Accumulate().Compile();\r\n        ");
 } 
            this.Write("        ");
 if (this.useCompiledDeaccumulate) { 
            this.Write("\r\n        computeResult = aggregate.ComputeResult().Compile();\r\n        ");
 } 
            this.Write("\r\n        this.keyComparer = stream.Properties.KeyEqualityComparer;\r\n\r\n        th" +
                    "is.pool = MemoryManager.GetMemoryPool<");
            this.Write(this.ToStringHelper.ToStringWithCulture(TKey));
            this.Write(", ");
            this.Write(this.ToStringHelper.ToStringWithCulture(TOutput));
            this.Write(">() as ");
            this.Write(this.ToStringHelper.ToStringWithCulture(Transformer.GetMemoryPoolClassName(this.keyType, this.outputType)));
            this.Write(this.ToStringHelper.ToStringWithCulture(genericParameters2));
            this.Write(";\r\n        ");
            this.Write(this.ToStringHelper.ToStringWithCulture(getOutputBatch));
            this.Write("\r\n        this.batch.Allocate();\r\n\r\n        ");
 if (!this.isUngrouped) { 
            this.Write("        var generator = this.keyComparer.CreateFastDictionary3Generator<");
            this.Write(this.ToStringHelper.ToStringWithCulture(TKey));
            this.Write(", HeldStateStruct>(1, this.keyComparer.GetEqualsExpr().Compile(), this.keyCompare" +
                    "r.GetGetHashCodeExpr().Compile(), stream.Properties.QueryContainer);\r\n        th" +
                    "is.heldAggregates = generator.Invoke();\r\n        ");
 } else { 
            this.Write("\r\n        isDirty = false;\r\n        ");
 } 
            this.Write(@"    }

    public override void ProduceQueryPlan(PlanNode previous)
    {
        Observer.ProduceQueryPlan(queryPlanGenerator(previous, this));
    }

    protected override void FlushContents()
    {
        if (this.batch == null || this.batch.Count == 0) return;
        this.batch.Seal();
        this.Observer.OnNext(this.batch);
        ");
            this.Write(this.ToStringHelper.ToStringWithCulture(getOutputBatch));
            this.Write("\r\n        this.batch.Allocate();\r\n    }\r\n\r\n    protected override void DisposeSta" +
                    "te() => this.batch.Free();\r\n\r\n    public override int CurrentlyBufferedOutputCou" +
                    "nt => this.batch.Count;\r\n\r\n    public override int CurrentlyBufferedInputCount =" +
                    "> ");
            this.Write(this.ToStringHelper.ToStringWithCulture(this.isUngrouped ? "0" : "this.heldAggregates.Count"));
            this.Write(";\r\n\r\n    public override unsafe void OnNext(StreamMessage<");
            this.Write(this.ToStringHelper.ToStringWithCulture(TKey));
            this.Write(", ");
            this.Write(this.ToStringHelper.ToStringWithCulture(TInput));
            this.Write("> inputBatch)\r\n    {\r\n        ");
            this.Write(this.ToStringHelper.ToStringWithCulture(BatchGeneratedFrom_TKey_TInput));
            this.Write(this.ToStringHelper.ToStringWithCulture(TKeyTInputGenericParameters));
            this.Write(" generatedBatch = inputBatch as ");
            this.Write(this.ToStringHelper.ToStringWithCulture(BatchGeneratedFrom_TKey_TInput));
            this.Write(this.ToStringHelper.ToStringWithCulture(TKeyTInputGenericParameters));
            this.Write(";\r\n\r\n        var count = generatedBatch.Count;\r\n\r\n        ");
 if (this.outputFields.Count() > 1) { 
            this.Write("\r\n        ");
            this.Write(this.ToStringHelper.ToStringWithCulture(TOutput));
            this.Write(" temporaryOutput;\r\n        ");
 } 
            this.Write("\r\n        // Create locals that point directly to the arrays within the columns i" +
                    "n the input batch.\r\n");
 foreach (var f in this.inputFields) { 
            this.Write("\r\n");
 if (f.canBeFixed) { 
            this.Write("\r\n        fixed (");
            this.Write(this.ToStringHelper.ToStringWithCulture(f.TypeName));
            this.Write("* ");
            this.Write(this.ToStringHelper.ToStringWithCulture(f.Name));
            this.Write("_col = generatedBatch.");
            this.Write(this.ToStringHelper.ToStringWithCulture(f.Name));
            this.Write(".col)\r\n        {\r\n");
 } else { 
            this.Write("\r\n        var ");
            this.Write(this.ToStringHelper.ToStringWithCulture(f.Name));
            this.Write("_col = generatedBatch.");
            this.Write(this.ToStringHelper.ToStringWithCulture(f.Name));
            this.Write(".col;\r\n\r\n");
 } 
 } 
            this.Write("\r\n        // Create locals that point directly to the arrays within the columns i" +
                    "n the output batch.\r\n");
 foreach (var f in this.outputFields) { 
            this.Write("\r\n");
 if (f.canBeFixed) { 
            this.Write("\r\n        fixed (");
            this.Write(this.ToStringHelper.ToStringWithCulture(f.TypeName));
            this.Write("* dest_");
            this.Write(this.ToStringHelper.ToStringWithCulture(f.Name));
            this.Write(" = this.batch.");
            this.Write(this.ToStringHelper.ToStringWithCulture(f.Name));
            this.Write(".col)\r\n        {\r\n");
 } else { 
            this.Write("\r\n        var dest_");
            this.Write(this.ToStringHelper.ToStringWithCulture(f.Name));
            this.Write(" = this.batch.");
            this.Write(this.ToStringHelper.ToStringWithCulture(f.Name));
            this.Write(".col;\r\n\r\n");
 } 
 } 
            this.Write(@"        var col_key = generatedBatch.key.col;

        fixed (long* col_vsync = generatedBatch.vsync.col)
        fixed (long* col_vother = generatedBatch.vother.col)
        fixed (int* col_hash = generatedBatch.hash.col)
        fixed (long* col_bv = generatedBatch.bitvector.col)
        for (int i = 0; i < count; i++)
        {
            if ((col_bv[i >> 6] & (1L << (i & 0x3f))) != 0)
            {
                if (col_vother[i] == long.MinValue)
                {
                    // We have found a row that corresponds to punctuation
                    OnPunctuation(col_vsync[i]);

                    int c = this.batch.Count;
                    this.batch.vsync.col[c] = col_vsync[i];
                    this.batch.vother.col[c] = long.MinValue;
                    this.batch.key.col[c] = default;
                    this.batch[c] = default;
                    this.batch.hash.col[c] = 0;
                    this.batch.bitvector.col[c >> 6] |= (1L << (c & 0x3f));
                    this.batch.Count++;
                    if (this.batch.Count == Config.DataBatchSize) FlushContents();
                }
                continue;
            }

            var syncTime = col_vsync[i];

            // Handle time moving forward
            if (syncTime > this.lastSyncTime)
            {
                ");
 if (this.isUngrouped) { 
            this.Write(@"
                if (currentStateExists && isDirty)   // there exists earlier state
                {
                    int c = this.batch.Count;
                    this.batch.vsync.col[c] = currentState.timestamp;
                    this.batch.vother.col[c] = StreamEvent.InfinitySyncTime;
                    ");
            this.Write(this.ToStringHelper.ToStringWithCulture(assignToOutput(computeResult("currentState.state"))));
            this.Write("\r\n                    this.batch.hash.col[c] = 0;\r\n                    this.batch" +
                    ".Count++;\r\n                    if (this.batch.Count == Config.DataBatchSize) Flu" +
                    "shContents();\r\n                }\r\n                isDirty = false;\r\n            " +
                    "    ");
 } else { 
            this.Write("\r\n                    int iter1 = FastDictionary3<");
            this.Write(this.ToStringHelper.ToStringWithCulture(TKey));
            this.Write(@", HeldStateStruct>.IteratorStart;
                    while (this.heldAggregates.IterateDirty(ref iter1))
                    {
                        var iter1entry = this.heldAggregates.entries[iter1];

                        int c = this.batch.Count;
                        this.batch.vsync.col[c] = iter1entry.value.timestamp;
                        this.batch.vother.col[c] = StreamEvent.InfinitySyncTime;
                        ");
            this.Write(this.ToStringHelper.ToStringWithCulture(assignToOutput(computeResult("iter1entry.value.state"))));
            this.Write(@"
                        this.batch.key.col[c] = iter1entry.key;
                        this.batch.hash.col[c] = iter1entry.hash;
                        this.batch.Count++;
                        if (this.batch.Count == Config.DataBatchSize) FlushContents();
                    }

                    // Time has moved forward, clean the held aggregates
                    this.heldAggregates.Clean();
                ");
 } 
            this.Write("\r\n                // Since sync time changed, set this.lastSyncTime\r\n            " +
                    "    this.lastSyncTime = syncTime;\r\n            }\r\n\r\n            ");
 if (this.isUngrouped) { 
            this.Write("\r\n            if (!currentStateExists)\r\n            {\r\n                currentSta" +
                    "te.state = ");
            this.Write(this.ToStringHelper.ToStringWithCulture(initialState));
            this.Write(@";
                currentState.timestamp = syncTime;
                currentStateExists = true;
                isDirty = true;
            }
            else
            {
                if (!isDirty)
                {
                    // Output end edge
                    int c = this.batch.Count;
                    this.batch.vsync.col[c] = syncTime;
                    this.batch.vother.col[c] = currentState.timestamp;
                    ");
            this.Write(this.ToStringHelper.ToStringWithCulture(assignToOutput(computeResult("currentState.state"))));
            this.Write(@"

                    this.batch.hash.col[c] = 0;
                    this.batch.Count++;
                    if (this.batch.Count == Config.DataBatchSize) FlushContents();
                    currentState.timestamp = syncTime;
                    isDirty = true;
                }
            }
            currentState.state = ");
            this.Write(this.ToStringHelper.ToStringWithCulture(accumulate("currentState.state", "col_vsync[i]")));
            this.Write(";\r\n            ");
 } else { 
            this.Write(@"            // Retrieve the key from the dictionary
            var currentKey = col_key[i];
            var currentHash = col_hash[i];

            int index;

            bool heldAggsLookup = false;
            {
                int num = currentHash & 0x7fffffff;
                index = num % this.heldAggregates.Size;

                do
                {
                    if ((this.heldAggregates.bitvector[index >> 3] & (0x1 << (index & 0x7))) == 0)
                    {
                        heldAggsLookup = false;
                        break;
                    }

                    if ((currentHash == this.heldAggregates.entries[index].hash) && (");
            this.Write(this.ToStringHelper.ToStringWithCulture(inlinedKeyComparerEquals("currentKey", "this.heldAggregates.entries[index].key")));
            this.Write(@"))
                    {
                        heldAggsLookup = true;
                        break;
                    }

                    index++;
                    if (index == this.heldAggregates.Size)
                        index = 0;
                } while (true);
            }

            if (!heldAggsLookup)
            // if (!this.heldAggregates.Lookup(currentKey, currentHash, out index))
            {
                // New group. Create new state
                HeldStateStruct currentState;
                currentState.state = ");
            this.Write(this.ToStringHelper.ToStringWithCulture(initialState));
            this.Write(@";
                currentState.timestamp = syncTime;
                // No output because initial state is empty

                this.heldAggregates.Insert(ref index, currentKey, currentState, currentHash);
            }
            else
            {
                // read new currentState from _heldAgg index
                var currentState = this.heldAggregates.entries[index].value;

                if (this.heldAggregates.IsClean(ref index))
                {
                    // Output end edge
                    int c = this.batch.Count;
                    this.batch.vsync.col[c] = syncTime;
                    this.batch.vother.col[c] = currentState.timestamp;
                    ");
            this.Write(this.ToStringHelper.ToStringWithCulture(assignToOutput(computeResult("currentState.state"))));
            this.Write(@"

                    this.batch.key.col[c] = currentKey;
                    this.batch.hash.col[c] = currentHash;
                    this.batch.Count++;
                    if (this.batch.Count == Config.DataBatchSize) FlushContents();
                    currentState.timestamp = syncTime;
                    this.heldAggregates.SetDirty(ref index);
                }
            }
            this.heldAggregates.entries[index].value.state = ");
            this.Write(this.ToStringHelper.ToStringWithCulture(accumulate("this.heldAggregates.entries[index].value.state", "col_vsync[i]")));
            this.Write(";\r\n            ");
 } 
            this.Write("        }\r\n\r\n        ");
 foreach (var f in this.inputFields.Where(fld => fld.canBeFixed)) { 
            this.Write("\r\n        }\r\n        ");
 } 
            this.Write("        ");
 foreach (var f in this.outputFields.Where(fld => fld.canBeFixed)) { 
            this.Write("\r\n        }\r\n        ");
 } 
            this.Write("\r\n        generatedBatch.Release();\r\n        generatedBatch.Return();\r\n    }\r\n\r\n\r" +
                    "\n\r\n    public void OnPunctuation(long syncTime)\r\n    {\r\n\r\n        ");
 if (this.outputFields.Count() > 1) { 
            this.Write("\r\n        ");
            this.Write(this.ToStringHelper.ToStringWithCulture(TOutput));
            this.Write(" temporaryOutput;\r\n        ");
 foreach (var f in this.outputFields) { 
            this.Write("\r\n        var dest_");
            this.Write(this.ToStringHelper.ToStringWithCulture(f.Name));
            this.Write(" = this.batch.");
            this.Write(this.ToStringHelper.ToStringWithCulture(f.Name));
            this.Write(".col;\r\n        ");
 } 
            this.Write("        ");
 } 
            this.Write("\r\n        // Handle time moving forward\r\n        if (syncTime > this.lastSyncTime" +
                    ")\r\n        {\r\n            ");
 if (this.isUngrouped) { 
            this.Write(@"
            if (currentStateExists && isDirty) // need to send start edge if state is dirty
            {
                int c = this.batch.Count;
                this.batch.vsync.col[c] = currentState.timestamp;
                this.batch.vother.col[c] = StreamEvent.InfinitySyncTime;
                ");
            this.Write(this.ToStringHelper.ToStringWithCulture(assignToOutput(computeResult("currentState.state"))));
            this.Write("\r\n\r\n                this.batch.hash.col[c] = 0;\r\n                this.batch.Count" +
                    "++;\r\n                if (this.batch.Count == Config.DataBatchSize) FlushContents" +
                    "();\r\n            }\r\n            isDirty = false;\r\n            ");
 } else { 
            this.Write("\r\n            int iter1 = FastDictionary3<");
            this.Write(this.ToStringHelper.ToStringWithCulture(TKey));
            this.Write(@", HeldStateStruct>.IteratorStart;
            while (this.heldAggregates.IterateDirty(ref iter1))
            {
                var iter1entry = this.heldAggregates.entries[iter1];

                    int c = this.batch.Count;
                    this.batch.vsync.col[c] = iter1entry.value.timestamp;
                    this.batch.vother.col[c] = StreamEvent.InfinitySyncTime;
                    ");
            this.Write(this.ToStringHelper.ToStringWithCulture(assignToOutput(computeResult("iter1entry.value.state"))));
            this.Write(@"

                    this.batch.key.col[c] = iter1entry.key;
                    this.batch.hash.col[c] = iter1entry.hash;
                    this.batch.Count++;
                    if (this.batch.Count == Config.DataBatchSize) FlushContents();
                }
            // Time has moved forward, clean the held aggregates
            this.heldAggregates.Clean();
            ");
 } 
            this.Write("\r\n            // Since sync time changed, set this.lastSyncTime\r\n            this" +
                    ".lastSyncTime = syncTime;\r\n        }\r\n    }\r\n}//\r\n");
            return this.GenerationEnvironment.ToString();
        }
    }
}
