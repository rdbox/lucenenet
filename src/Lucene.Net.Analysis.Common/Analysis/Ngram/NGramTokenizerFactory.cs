﻿using Lucene.Net.Analysis.Util;
using Lucene.Net.Util;
using System.Collections.Generic;
using System.IO;

namespace Lucene.Net.Analysis.Ngram
{
    /*
     * Licensed to the Apache Software Foundation (ASF) under one or more
     * contributor license agreements.  See the NOTICE file distributed with
     * this work for additional information regarding copyright ownership.
     * The ASF licenses this file to You under the Apache License, Version 2.0
     * (the "License"); you may not use this file except in compliance with
     * the License.  You may obtain a copy of the License at
     *
     *     http://www.apache.org/licenses/LICENSE-2.0
     *
     * Unless required by applicable law or agreed to in writing, software
     * distributed under the License is distributed on an "AS IS" BASIS,
     * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
     * See the License for the specific language governing permissions and
     * limitations under the License.
     */

    /// <summary>
    /// Factory for <seealso cref="NGramTokenizer"/>.
    /// <pre class="prettyprint">
    /// &lt;fieldType name="text_ngrm" class="solr.TextField" positionIncrementGap="100"&gt;
    ///   &lt;analyzer&gt;
    ///     &lt;tokenizer class="solr.NGramTokenizerFactory" minGramSize="1" maxGramSize="2"/&gt;
    ///   &lt;/analyzer&gt;
    /// &lt;/fieldType&gt;</pre>
    /// </summary>
    public class NGramTokenizerFactory : TokenizerFactory
    {
        private readonly int maxGramSize;
        private readonly int minGramSize;

        /// <summary>
        /// Creates a new NGramTokenizerFactory </summary>
        public NGramTokenizerFactory(IDictionary<string, string> args)
            : base(args)
        {
            minGramSize = GetInt(args, "minGramSize", NGramTokenizer.DEFAULT_MIN_NGRAM_SIZE);
            maxGramSize = GetInt(args, "maxGramSize", NGramTokenizer.DEFAULT_MAX_NGRAM_SIZE);
            if (args.Count > 0)
            {
                throw new System.ArgumentException("Unknown parameters: " + args);
            }
        }

        /// <summary>
        /// Creates the <seealso cref="TokenStream"/> of n-grams from the given <seealso cref="TextReader"/> and <seealso cref="AttributeSource.AttributeFactory"/>. </summary>
        public override Tokenizer Create(AttributeSource.AttributeFactory factory, TextReader input)
        {
            if (luceneMatchVersion.OnOrAfter(LuceneVersion.LUCENE_44))
            {
                return new NGramTokenizer(luceneMatchVersion, factory, input, minGramSize, maxGramSize);
            }
            else
            {
                return new Lucene43NGramTokenizer(factory, input, minGramSize, maxGramSize);
            }
        }
    }
}